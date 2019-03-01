using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitsNet.Extensions.NumberToPressure;


namespace RcDesign.Material
{
    public class Concrete
    {
        /// <summary>
        /// Karakteristik basınç dayanımı (t/m2)
        /// </summary>
        public double fc { get; private set; }

        /// <summary>
        /// Karakteristik çekme dayanımı (t/m2)
        /// </summary>
        public double fct { get; private set; }


        /// <summary>
        /// Tasarım basınç dayanımı (t/m2)
        /// </summary>
        public double fcd { get; private set; }

        /// <summary>
        /// Tasarım çekme dayanımı (t/m2)
        /// </summary>
        public double fctd { get; private set; }

        /// <summary>
        /// Elastisite modülü (t/m2)
        /// </summary>
        public double E { get; private set; }

        /// <summary>
        /// Betondaki maksimum birim kısalma
        /// </summary>
        public double Ecu { get; set; }

        /// <summary>
        /// Beton basınç bloğu yükseklik faktörü: a=k1*c
        /// </summary>
        public double k1 { get; private set; }

        /// <summary>
        /// Beton basınç bloğu yükseklik faktörü
        /// </summary>
        public double beta { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Code">Ülke kodu</param>
        /// <param name="fck">Karakteristik beton basınç dayanımı (MPa)</param>
        public Concrete(DesignCodes Code, double fck)
        {
            if (Code == DesignCodes.TS500)
            {
                Ecu = 0.003;
                k1 = fck <= 25 ? 0.85 : Math.Max(0.85 - 0.006 * (fc - 25.00), 0.64);
                beta = 0.85;
                E = 3250 * Math.Sqrt(fck) + 14000;
                fct = 0.35 * Math.Sqrt(fck);
                fc = fck.Megapascals().TonnesForcePerSquareMeter;
                fct = fct.Megapascals().TonnesForcePerSquareMeter;
                E = E.Megapascals().TonnesForcePerSquareMeter;
                fcd = fc / 1.5;
                fctd = fct / 1.5;
            }
            else if (Code == DesignCodes.ACI318)
            {
                Ecu = 0.003;
                k1 = fck <= 28 ? 0.85 : Math.Max(0.85 - 0.007 * (fc - 28.0), 0.65);
                beta = 0.85;
                E = 4700 * Math.Sqrt(fck);
                fct = 0.25 * Math.Sqrt(fck);
                fc = fck.Megapascals().TonnesForcePerSquareMeter;
                fct = fct.Megapascals().TonnesForcePerSquareMeter;
                E = E.Megapascals().TonnesForcePerSquareMeter;
                fcd = fc;
                fctd = fct;
            }
        }
    }
}
