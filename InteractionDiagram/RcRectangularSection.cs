using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RcDesign.Material;
using RcDesign.Section;


namespace RcDesign.InteractionDiagram
{
    public static class Extensions
    {
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }

    public class RcRectangularSection
    {
        public RectangularSection Section { get; private set; }
        public Concrete Concrete { get; set; }
        public Steel Steel { get; private set; }
        public BalancedCondition balancedCondition { get; private set; }
        public ServiceabilityCondition serviceabilityCondition { get; private set; }
        public List<ReinforcingBar> ReinforcingBars { get; private set; }

        public double cMax { get; private set; }

        public RcRectangularSection(RectangularSection Section, Concrete Concrete, Steel Steel, List<ReinforcingBar> ReinforcingBars)
        {
            this.Section = Section;
            this.Concrete = Concrete;
            this.Steel = Steel;
            this.ReinforcingBars = ReinforcingBars;

            balancedCondition = new BalancedCondition(Concrete, Steel, Section.b, Section.d);
            serviceabilityCondition = new ServiceabilityCondition(Concrete, Steel, Section.b, Section.d);
            cMax = ReinforcingBars.Max(bar => bar.di);
            //cMax = 0.90 * Section.h;
        }

        public double Pmax()
        {
            return 0.80 * Concrete.fc * Section.Ac;
        }

        public double NominalPmin()
        {
            return -ReinforcingBars.Sum(rb => rb.As) * Steel.fyd;
        }

        public double NominalMmin()
        {
            return
               Steel.fyd * ReinforcingBars.Sum(rb => rb.As * (rb.di - Section.h / 2.0));
        }

        public double NominalPmax()
        {
            return
               Concrete.beta * Concrete.fcd * (Section.b * Section.h - ReinforcingBars.Sum(rb => rb.As)) +
               ReinforcingBars.Sum(rb => rb.As) * Steel.fyd;
                
        }

        public double NominalMmax()
        {
            return
            (Steel.fyd - 0.85 * Concrete.fcd) * ReinforcingBars.Sum(rb => rb.As * (Section.h / 2.0 - rb.di));
        }

        private double PEgilmeBasincElemanSinir()
        {
            return 0.10 * Concrete.fc * Section.Ac;
        }

        /// <summary>
        /// Donatı çubuğunda birim şekil değiştirme
        /// 
        /// </summary>
        /// <param name="c">Tarafsız eksen derinliği</param>
        /// <param name="di">Donatının kesitin üst noktasından uzaklığı</param>
        /// <returns></returns>
        public double ReinforcingStrain(double c, double di)
        {
            return Concrete.Ecu * (c - di) / c;
        }

        public double ReinforcingStress(double c, double di)
        {
            if (ReinforcingStrain(c, di) >= Steel.Eyd)
                return Steel.fyd;
            else if (ReinforcingStrain(c, di) <= -Steel.Eyd)
                return -Steel.fyd;
            else
                return ReinforcingStrain(c, di) * Steel.E;
        }

        public double ReinforcingForce(double c, double di, double ReinforcingArea)
        {
            return (ReinforcingStress(c, di) - (c / Concrete.k1 < di ? 0.0 : 0.85 * Concrete.fcd)) * ReinforcingArea;
        }

        public double ConcreteForce(double c)
        {
            return
                Concrete.beta * Concrete.fcd * Concrete.k1 * c * Section.b;
        }

        public double AxialCapacity(double c)
        {
            return
                ConcreteForce(c) + ReinforcingBars.Sum(rb => ReinforcingForce(c, rb.di, rb.As));
        }

        public double MomentCapacity(double c)
        {
            return
               ConcreteForce(c) * (Section.h / 2.0 - Concrete.k1 * c / 2.0) +
               ReinforcingBars.Sum(rb => ReinforcingForce(c, rb.di, rb.As) * (Section.h / 2.0 - rb.di));
        }

        public double MaxRebarStrain(List<double> ReinforcingStrains)
        {
            List<double> diList = ReinforcingBars.Select(x => x.di).ToList();
            int i = diList.IndexOf(diList.Max());
            return Math.Abs(ReinforcingStrains[i]);
        }

        public double MaxRebarDi()
        {
            return ReinforcingBars.Select(x => x.di).Max();
        }

        public double MinRebarDi()
        {
            return ReinforcingBars.Select(x => x.di).Min();
        }
    }
}
