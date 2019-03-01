using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitsNet.Extensions.NumberToLength;

namespace RcDesign.Section
{
    public class RectangularSection
    {
        /// <summary>
        /// Kesit genişliği (m)
        /// </summary>
        public double b { get; private set; }

        /// <summary>
        /// Kesit yüksekliği (m)
        /// </summary>
        public double h { get; private set; }

        /// <summary>
        /// Faydalı yükseklik(m)
        /// </summary>
        public double d { get; private set; }
        public double pTensFace { get; private set; }
        public double pCompFace { get; private set; }
        public double xp { get; private set; }
        public double Ac { get; private set; }
        public double emin { get; private set; }

        public RectangularSection(double b, double h, double pTensFace, double pCompFace)
        {
            this.b = b;
            this.h = h;
            this.pTensFace = pTensFace;
            this.pCompFace = pCompFace;
            d = h - pTensFace;
            xp = h / 2.0;
            Ac = b * h;
            emin = (15 + 0.03 * h.Meters().Millimeters).Millimeters().Meters;
        }
    }
}
