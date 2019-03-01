using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileHelpers;

namespace RcDesign.InteractionDiagram
{
    [DelimitedRecord("|")]
    public class InteractionDiagramItem
    {
        public double Mnominal { get; set; }
        public double Pnominal { get; set; }
        [FieldHidden]
        public double Mactual { get; set; }
        [FieldHidden]
        public double Pactual { get; set; }
        public double Phi { get; set; }
        public double a { get; set; }

        public InteractionDiagramItem()
        {

        }

        public InteractionDiagramItem(double M, double P, double a)
        {
            this.Mnominal = M;
            this.Pnominal = P;
            this.a = a;
        }

        public InteractionDiagramItem(double M, double P, double Phi, double a)
        {
            Mnominal = M;
            Pnominal = P;
            Mactual = M * Phi;
            Pactual = P * Phi;
            this.Phi = Phi;
            this.a = a;
        }

        public static void WriteToFile(List<InteractionDiagramItem> items, string filePath)
        {
            var engine = new FileHelperEngine<InteractionDiagramItem>();
            engine.HeaderText = engine.GetFileHeader();
            engine.WriteFile(filePath, items);
        }
    }
}
