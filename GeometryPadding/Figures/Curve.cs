using System;
using System.Collections.Generic;
using System.Linq;

namespace GeometryPadding.Figures
{
    using GeometryPadding.Misc;
    using GeometryPadding.Strategies;

    public class Curve : PointSet
    {
        public Curve() { }

        public Curve(params Point[] points)
        {
            this.Points = points;
        }

        public Curve(IList<Point> points)
        {
            this.Points = points ?? new List<Point>();
        }

        public double Length => CurveStrategies.LengthOfCurve(this);

        public Figure FindIntersection(Curve l)
        {
            if (this.Points.Count != 2 || l.Points.Count != 2)
            {
                throw new Exception("The curves must contain exactly two points each.");
            }
            return CurveStrategies.IntersectionOfCurves(this.Points[0], this.Points[1], l.Points[0], l.Points[1]);
        }

        public Figure FindIntersectionInf(Curve l)
        {
            if (this.Points.Count != 2 || l.Points.Count != 2)
            {
                throw new Exception("The curves must contain exactly two points each.");
            }
            return CurveStrategies.IntersectionOfCurvesInf(this.Points[0], this.Points[1], l.Points[0], l.Points[1]);
        }

        public override string ToString()
        {
            return $"({string.Join(", ", this.Points)})";
        }

        public Polygon CreatePolygonFromCurve(double from, double to, double padding)
        {
            var curve = new Curve();
            foreach (var point in this.Points)
            {
                curve.Points.Add(new Point(point));
            }

            curve = curve.RemoveOverlappingPoints2D();
            var len = curve.Length;
            curve = CurveStrategies.CutCurve(curve, from * len, to * len);
            curve = curve.RemoveOverlappingPoints2D();

            var curveLeft = curve.LateralOffsetCurve(Enums.LateralPosition.Left, padding).RemoveOverlappingPoints2D();
            var curveRight = curve.LateralOffsetCurve(Enums.LateralPosition.Right, padding).RemoveOverlappingPoints2D();

            var poly = new Polygon();
            foreach (var point in curveLeft.Points)
            {
                poly.Points.Add(point);
            }
            foreach (var point in curveRight.Points.Reverse())
            {
                poly.Points.Add(point);
            }
            poly.Points.Add(curveLeft.Points.First());
            return poly;

        }

        public Curve RemoveOverlappingPoints2D()
        {
            var newPoints = new Curve();
            for (var i = 0; i < this.Points.Count - 1; i++)
            {
                if (MathHelper.DoublesAreEqual(this.Points[i].X, this.Points[i + 1].X)
                    && MathHelper.DoublesAreEqual(this.Points[i].Y, this.Points[i + 1].Y))
                {
                    continue;
                }

                newPoints.Points.Add(this.Points[i]);
            }

            if (this.Points.Count > 0)
            {
                newPoints.Points.Add(this.Points.Last());
            }

            return newPoints;
        }

        private Curve LateralOffsetCurve(
                Enums.LateralPosition? lateralPosition, double offset)
        {
            Curve curve;
            if (this.UpdateOffset(lateralPosition, ref offset, out curve))
            {
                return curve;
            }

            var shiftedPoints = new Curve();
            var cnt = this.Points.Count();
            for (var i = 0; i < cnt - 1; i++)
            {
                var shiftedPointsTmp = CurveStrategies.CreateParallelLine(this.Points[i], this.Points[i + 1], offset);
                shiftedPoints.Points.Add(shiftedPointsTmp.Points[0]);
                shiftedPoints.Points.Add(shiftedPointsTmp.Points[1]);
            }

            var cntShifted = shiftedPoints.Points.Count();
            for (var i = 0; i < cntShifted - 3; i += 2)
            {
                var innerIntersection = CurveStrategies.IntersectionOfCurves(
                    shiftedPoints.Points[i],
                    shiftedPoints.Points[i + 1],
                    shiftedPoints.Points[i + 2],
                    shiftedPoints.Points[i + 3]);

                if (innerIntersection != null && innerIntersection.GetType() == typeof(Point))
                {
                    // inner intersection
                    shiftedPoints.Points[i + 1].X = ((Point)innerIntersection).X;
                    shiftedPoints.Points[i + 1].Y = ((Point)innerIntersection).Y;
                    shiftedPoints.Points[i + 2].X = ((Point)innerIntersection).X;
                    shiftedPoints.Points[i + 2].Y = ((Point)innerIntersection).Y;
                }
                else
                {
                    // outer intersection
                    var outerIntersection = (Point)CurveStrategies.IntersectionOfCurvesInf(
                        shiftedPoints.Points[i],
                        shiftedPoints.Points[i + 1],
                        shiftedPoints.Points[i + 2],
                        shiftedPoints.Points[i + 3]);

                    var angle = MathHelper.RadToDeg(MathHelper.AngleBetweenLines(
                        shiftedPoints.Points[i + 1],
                        outerIntersection,
                        shiftedPoints.Points[i + 2]));

                    if (angle >= 45f && outerIntersection != null && outerIntersection.GetType() == typeof(Point))
                    {
                        shiftedPoints.Points[i + 1].X = ((Point)outerIntersection).X;
                        shiftedPoints.Points[i + 1].Y = ((Point)outerIntersection).Y;
                        shiftedPoints.Points[i + 2].X = ((Point)outerIntersection).X;
                        shiftedPoints.Points[i + 2].Y = ((Point)outerIntersection).Y;
                    }
                }
            }

            return shiftedPoints;
        }

        private bool UpdateOffset(Enums.LateralPosition? lateralPosition, ref double offset, out Curve curve)
        {
            switch (lateralPosition)
            {
                case null:
                    {
                        curve = this;
                        return true;
                    }

                case Enums.LateralPosition.Left:
                    offset = -offset;
                    break;

                case Enums.LateralPosition.Middle:
                    offset = 0;
                    break;

                case Enums.LateralPosition.Right:
                    offset = +offset;
                    break;
            }
            curve = null;
            return false;
        }
    }
}
