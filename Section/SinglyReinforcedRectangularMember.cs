using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RcDesign.Material;

namespace RcDesign.Section
{
    public class SinglyReinforcedRectangularMember
    {
        /// <summary>
        /// Boyutsuz basınç bloğu derinliği
        /// </summary>
        public double k { get; private set; }

        /// <summary>
        /// Basınç bloğu derinliği
        /// </summary>
        public double a { get; private set; }

        /// <summary>
        /// Tarafsız eksen derinliği
        /// </summary>
        public double c { get; private set; }

        /// <summary>
        /// Donatı oranı
        /// </summary>
        public double r { get; private set; }

        /// <summary>
        /// Donatı alanı
        /// </summary>
        public double As { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="concrete">Beton</param>
        /// <param name="steel">Çelik</param>
        /// <param name="section">Dikdörtgen kesit</param>
        /// <param name="Md">Tasarım moment [t.m]</param>
        /// <param name="Nd">Normal kuvvet. Basınç +. [t]</param>
        public SinglyReinforcedRectangularMember(Concrete concrete, Steel steel, RectangularSection section, double Md, double Nd)
        {
            k = 1 - Math.Sqrt(1 - 2.0 * (Math.Abs(Md) + Nd * (section.d - section.h / 2)) / (concrete.beta * concrete.fcd * section.b * Math.Pow(section.d, 2.0)));
            a = k * section.d;
            c = a / concrete.k1;
            As = (concrete.beta * concrete.fcd * section.b * section.d - Nd) / steel.fyd;
            r = As / (section.b * section.d);
        }
    }
}
