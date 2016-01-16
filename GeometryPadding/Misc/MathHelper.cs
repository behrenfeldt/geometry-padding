using System;

namespace GeometryPadding.Misc
{
    using GeometryPadding.Figures;

    static class MathHelper
    {
        private const double Eps = 1e-6;

        public static bool DoubleIsZero(double d)
        {
            return Math.Abs(d) < Eps;
        }

        public static bool DoubleIsZeroOrSmaller(double d)
        {
            return d < Eps;
        }

        public static bool DoublesAreEqual(double a, double b)
        {
            return DoubleIsZero(a - b);
        }

        public static double Cross(Point O, Point A, Point B)
        {
            return (A.X - O.X) * (B.Y - O.Y) - (A.Y - O.Y) * (B.X - O.X);
        }
    }
}
