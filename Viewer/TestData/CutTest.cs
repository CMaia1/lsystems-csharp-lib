﻿// reference "System.Core.dll"
// reference "LSystems.dll"

namespace Viewer.TestData
{
    public class aaCutTest : LSystems.Turtle.SystemDefinition, LSystems.IRewriteRules
    {
        public class A { }
        public class B { }
        public class X
        {
            public double D { get; private set; }
            public X(double d) { this.D = d; }
        }
        public class U { }

        private double Step = 10;
        private double Angle = 75;

        [LSystems.Production]
        public object Grow(A a)
        {
            return Produce(
                F(Step),
                StartBranch, TurnLeft(Angle), new X(10), new B(), EndBranch,
                StartBranch, TurnRight(Angle), new X(10), new B(), EndBranch,
                new A());
        }

        [LSystems.Production]
        public object Grow(B b)
        {
            return Produce(F(Step), new B());
        }

        [LSystems.Production]
        public object Grow(X x)
        {
            if (x.D > 0) { return new X(x.D - 1); }
            else { return Produce(new U(), Cut); }
        }

        [LSystems.Production]
        public object Grow(U u)
        {
            return F(0.3 * Step);
        }

        public object Axiom
        {
            get { return Produce(LineThickness(3), TurnLeft(90), new A()); }
        }

        public int Depth
        {
            get { return 2; }
        }

        public LSystems.RewriteDirection RewriteDirection
        {
            get { return LSystems.RewriteDirection.LeftToRight; }
        }
    }
}
