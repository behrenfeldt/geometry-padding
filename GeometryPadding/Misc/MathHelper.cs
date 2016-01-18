using System;

namespace GeometryPadding.Misc
{
    using GeometryPadding.Figures;

    static class MathHelper
    {
        private const double Eps = 1e-6;

        public static double DegToRad(double deg)
        {
            return deg * Math.PI / 180.0f;
        }

        public static double RadToDeg(double rad)
        {
            return rad * 180.0f / Math.PI;
        }

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

        public static double AngleBetweenLines(Point a, Point b, Point c)
        {
            var p1X = a.X - b.X;
            var p1Y = a.Y - b.Y;
            var p2X = c.X - b.X;
            var p2Y = c.Y - b.Y;

            var dotProduct = (p1X * p2X) + (p1Y * p2Y);
            var denominator = Math.Sqrt((p1X * p1X) + (p1Y * p1Y)) * Math.Sqrt((p2X * p2X) + (p2Y * p2Y));
            var product = !DoubleIsZero(denominator) ? dotProduct / denominator : 0.0;

            return Math.Acos(product);
        }
    }
}
