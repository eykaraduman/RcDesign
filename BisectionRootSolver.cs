using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RcDesign
{
    public delegate double RealFunction(double x);
    public enum SolverStatus { DivideByZero, RootNotFound, RootFound }

    public static class Globals
    {
        public const double PI = Math.PI;
        public const double g = 9.81;
        public const double Tolerance = 1.0e-4;
        public const int MaxIterations = 1000;
    }

    public class BisectionSolver
    {
        public double Tolerance { get; set; }
        public int MaxIterations { get; set; }
        public int IterationsNeeded { get; set; }
        public double EstimatedError { get; private set; }
        public RealFunction TargetFunction { get; set; }
        public SolverStatus Status { get; set; }
        public bool ThrowExceptionOnFailure { get; set; }

        public BisectionSolver(RealFunction TargetFunction)
        {
            this.TargetFunction = TargetFunction;
            MaxIterations = 100;
            Tolerance = 1e-2;
            ThrowExceptionOnFailure = false;
        }

        public double Solve(double xa, double xb)
        {
            RealFunction f = TargetFunction;
            int i = 0;
            double x1 = xa;
            double x2 = xb;
            double fb = f(xb);
            double xmOld = 0.0;
            while (i <= MaxIterations)
            {
                i++;
                double xm = (x1 + x2) / 2;
                if (fb * f(xm) > 0)
                    x2 = xm;
                else
                    x1 = xm;

                if (Math.Abs(x2 - x1) <= Tolerance)
                {
                    Status = SolverStatus.RootFound;
                    IterationsNeeded = i;
                    EstimatedError = Math.Abs(100 * (xm - xmOld) / xm);
                    return x2 - (x2 - x1) * f(x2) / (f(x2) - f(x1));
                }

                xmOld = xm;
            }

            Status = SolverStatus.RootNotFound;
            if (ThrowExceptionOnFailure)
                throw new RootNotFoundException("Kök bulunamadı!");
            if(IterationsNeeded >= MaxIterations)
                throw new RootNotFoundException("Maksimum iterasyon sayısı aşıldı!");

            return Double.NaN;
        }
    }

    public class RootNotFoundException : Exception
    {
        public RootNotFoundException()
            : base()
        {
        }

        public RootNotFoundException(string message)
            : base(message)
        {
        }
        public RootNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
        protected RootNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {

        }

    }

}
