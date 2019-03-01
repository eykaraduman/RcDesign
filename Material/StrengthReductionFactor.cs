using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RcDesign.Material
{
    public class StrengthReductionFactor
    {
        // axial compression: 0.80
        public double phiA { get; private set; }

        // tension controlled failure: 0.90
        public double phiB { get; private set; }
        // compression controlled failure: 0.65
        public double phiC { get; private set; }

        public StrengthReductionFactor(double phiA, double phiB, double phiC)
        {
            this.phiA = phiA;
            this.phiB = phiB;
            this.phiC = phiC;
        }

        public double CalculateActualPhi(DesignCodes code, double MaxStrain)
        {
            switch (code)
            {
                case DesignCodes.TS500:
                    return 1.0;
                case DesignCodes.ACI318:
                    if (MaxStrain <= 0.002)
                    {
                        return phiC;
                    }
                    else
                    {
                        if (MaxStrain >= 0.005)
                            return phiB;
                        else
                            return phiC + (phiB - phiC) * (MaxStrain - 0.002) / 0.003;
                    }
                default:
                    return 1.00;
            }
        }
    }
}
