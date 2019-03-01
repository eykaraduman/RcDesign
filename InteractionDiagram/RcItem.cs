using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RcDesign.InteractionDiagram
{
    public class RcItem
    {
        public double Strain { get; private set; }
        public double Stress { get; private set; }
        public double Force { get; private set; }
        public double Moment { get; private set; }

        public RcItem()
        {

        }
    }
}
