using System;

namespace GeometryPadding.Strategies
{
    using System.Collections.Generic;

    using GeometryPadding.Figures;

    public static class PointStrategies
    {
        public static double EuclidianDistance(Point p1, Point p2)
        {
            return Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y));
        }

        public static IList<double> PointAlongLine2D(Point p1, Point p2, double distance)
        {
            var vec = new Point(p2.X - p1.X, p2.Y - p1.Y);
            vec /= Math.Sqrt(vec.X * vec.X + vec.Y * vec.Y);
            vec *= distance;

            return new List<double> { p1.X + vec.X, p1.Y + vec.Y };
        }
    }
}
