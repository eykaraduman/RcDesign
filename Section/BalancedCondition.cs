using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RcDesign.Material;

namespace RcDesign.Section
{
    public class BalancedCondition
    {
        /// <summary>
        /// Tarafsız eksen derinliği
        /// </summary>
        public double c { get; private set; }

        /// <summary>
        /// Basınç bloğu derinliği
        /// </summary>
        public double a { get; private set; }

        /// <summary>
        /// Maksimum basınç bloğu derinliği
        /// </summary>
        public double amax { get; private set; }

        /// <summary>
        /// Maksimum basınç bloğu derinliği
        /// </summary>
        public double cmax { get; private set; }

        /// <summary>
        /// Boyutsuz basınç bloğu derinliği
        /// </summary>
        public double k { get; private set; }

        /// <summary>
        /// Maksimum boyutsuz basınç bloğu derinliği
        /// </summary>
        public double kmax { get; private set; }

        /// <summary>
        /// Donatı oranı
        /// </summary>
        public double r { get; private set; }

        /// <summary>
        /// Moment kolu katsayısı: z=j*d
        /// </summary>
        public double j { get; private set; }

        /// <summary>
        /// Moment
        /// </summary>
        public double M { get; private set; }


        /// <summary>
        /// b*d^2/M
        /// </summary>
        public double K { get; private set; }

        public BalancedCondition(Concrete concrete, Steel steel, double b, double d)
        {
            c = concrete.Ecu * d / (concrete.Ecu + steel.Eyd);            
            a = c * concrete.k1;
            k = a / d;
            r = concrete.beta * concrete.fcd * concrete.k1 * c / (steel.fyd * d);
            j = 1.0 - concrete.k1 * c / (2.0 * d);
            K = 1.0 / (j * steel.fyd * r);
            M = r * b * d * steel.fyd * (d - concrete.k1 * c / 2.0);

            amax = 0.85 * a;
            cmax = amax / concrete.k1;
            kmax = amax / d;
        }
    }
}
