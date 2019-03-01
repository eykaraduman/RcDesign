using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace RcDesign.InteractionDiagram
{
    public class ReinforcingBar: INotifyPropertyChanged,ICloneable
    {      
        public double As { get; set; }

        public double di { get; set; }

        public ReinforcingBar()
        {

        }

        public ReinforcingBar(ReinforcingBar rb)
        {
            As = rb.As;
            di = rb.di;
        }

        public ReinforcingBar(double As, double di)
        {
            this.As = As;
            this.di = di;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public object Clone()
        {
            return new ReinforcingBar(this);
        }
    }
}
