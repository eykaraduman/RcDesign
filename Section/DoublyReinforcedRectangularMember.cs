using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RcDesign.Material;

namespace RcDesign.Section
{
    public class DoublyReinforcedRectangularMember
    {
        /// <summary>
        /// Çekme donatı alanı
        /// </summary>
        public double Ast { get; private set; }

        /// <summary>
        /// Basınç donatı alanı
        /// </summary>
        public double Asc { get; private set; }

        public DoublyReinforcedRectangularMember(Concrete concrete, Steel steel, RectangularSection section, double Md, double Nd)
        {
            ServiceabilityCondition sCond = new ServiceabilityCondition(concrete, steel, section.b, section.d);

            double Mds = 0.85 * concrete.fcd * sCond.a * section.b * (section.d - sCond.a / 2.0) - (section.d - section.h / 2.0) * Nd;

            double fs = Math.Min(steel.E * concrete.Ecu * ((sCond.c - section.pCompFace) / sCond.c), steel.fyd);
            Asc = (Md - Mds) / (fs * (section.d - section.pCompFace));
            Ast = (0.85 * concrete.fcd * sCond.a * section.b - Nd + Asc * fs) / steel.fyd;
            
        }

        public void CalculateUSBR()
        {

        }
    }
}
