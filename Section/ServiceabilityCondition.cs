using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RcDesign.Material;

namespace RcDesign.Section
{
    public class ServiceabilityCondition
    {
        /// <summary>
        /// Tarafsız eksen derinliği
        /// </summary>
        public double c { get; private set; }

        /// <summary>
        /// Tarafsız eksen derinliği
        /// </summary>
        public double a { get; private set; }

        /// <summary>
        /// Moment kolu katsayısı
        /// </summary>
        public double j { get; private set; }

        /// <summary>
        /// Moment
        /// </summary>
        public double M { get; private set; }

        /// <summary>
        /// Donatı oranı
        /// </summary>
        public double r { get; private set; }

        /// <summary>
        /// b*d^2/M : m^2/t
        /// </summary>
        public double K { get; private set; }

        public ServiceabilityCondition(Concrete concrete, Steel steel, double b, double d)
        {
            r = 0.235 * concrete.fcd / steel.fyd;
            c = r * d * steel.fyd / (concrete.fcd * concrete.beta * concrete.k1);
            j = 1 - c * concrete.k1 / (2.0 * d);
            M = r * b * d * steel.fyd * j * d;
            a = concrete.k1 * c;

            K = b * d * d / M;
        }
    }
}
