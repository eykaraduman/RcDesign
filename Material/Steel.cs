using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitsNet.Extensions.NumberToPressure;

namespace RcDesign.Material
{
    public class Steel
    {
        /// <summary>
        /// Akma dayanımı (t/m2)
        /// S420a için 420 Mpa
        /// </summary>
        public double fy { get; private set; }

        /// <summary>
        /// Tasarım Akma dayanımı (t/m2)
        /// </summary>
        public double fyd { get; private set; }

        /// <summary>
        /// Elastisite modülü (t/m2)
        /// </summary>
        public double E { get; private set; }


        /// <summary>
        /// Akma uzaması
        /// </summary>
        public double Ey { get; private set; }

        /// <summary>
        /// Tasarım akma uzaması
        /// </summary>
        public double Eyd { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fyk">Karakteristik akma dayanımı</param>
        public Steel(DesignCodes Code, double fyk)
        {
            if (Code == DesignCodes.TS500)
            {
                E = 200000.Megapascals().TonnesForcePerSquareMeter;
                fy = fyk.Megapascals().TonnesForcePerSquareMeter;
                fyd = fy / 1.15;
                Ey = fy / E;
                Eyd = fyd / E;
            }
            else if (Code == DesignCodes.ACI318)
            {
                E = 200000.Megapascals().TonnesForcePerSquareMeter;
                fy = fyk.Megapascals().TonnesForcePerSquareMeter;
                fyd = fy;
                Ey = fy / E;
                Eyd = fyd / E;
            }
        }
    }
}
