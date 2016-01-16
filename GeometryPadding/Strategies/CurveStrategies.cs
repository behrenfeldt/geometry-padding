using System;

namespace GeometryPadding.Strategies
{
    using System.Linq;

    using Figures;

    using Misc;

    public static class CurveStrategies
    {
        public static double LengthOfCurve(Curve l)
        {
            var length = 0.0;
            for (var i = 0; i < l.Points.Count - 1; i++)
            {
                length += l.Points[i].EuclidianDistanceTo(l.Points[i + 1]);
            }

            return length;
        }

        public static Figure IntersectionOfCurvesInf(Point p1, Point p2, Point p3, Point p4)
        {
            var aVerticle = false;
            var bVerticle = false;

            if (MathHelper.DoubleIsZeroOrSmaller(Math.Abs(p2.X - p1.X)))
            {
                aVerticle = true;
            }

            if (MathHelper.DoubleIsZeroOrSmaller(Math.Abs(p4.X - p3.X)))
            {
                bVerticle = true;
            }

            if (aVerticle && bVerticle)
            {
                return null;
            }

            if (aVerticle)
            {
                var k2 = (p4.Y - p3.Y) / (p4.X - p3.X);
                var m2 = p3.Y - (k2 * p3.X);
                var x = p1.X;
                var y = k2 * x + m2;
                return new Point(x, y);
            }

            if (bVerticle)
            {
                var k1 = (p2.Y - p1.Y) / (p2.X - p1.X);
                var m1 = p1.Y - k1 * p1.X;
                var x = p3.X;
                var y = k1 * x + m1;
                return new Point(x, y);
            }
            else
            {
                var k1 = (p2.Y - p1.Y) / (p2.X - p1.X);
                var k2 = (p4.Y - p3.Y) / (p4.X - p3.X);
                if (MathHelper.DoublesAreEqual(k1, k2))
                {
                    return null;
                }

                var m1 = p1.Y - k1 * p1.X;
                var m2 = p3.Y - k2 * p3.X;
                var x = (m1 - m2) / (k2 - k1);
                var y = k1 * x + m1;
                return new Point(x, y);
            }
        }

        public static Figure IntersectionOfCurves(Point p1, Point p2, Point p3, Point p4)
        {
            double x;
            double y;
            double xx;
            double yy;

            var incA = 0;
            var incB = 0;
            var rev = 0;

            var leftA = p3.X < p4.X ? p3.X : p4.X;
            var topA = p3.Y > p4.Y ? p3.Y : p4.Y;
            var rightA = p3.X > p4.X ? p3.X : p4.X;
            var bottomA = p3.Y < p4.Y ? p3.Y : p4.Y;

            if (p3.Y > p4.Y)
            {
                ++rev;
            }

            if (p3.X > p4.X)
            {
                ++rev;
            }

            if (rev % 2 == 0)
            {
                incA = 1;
            }

            rev = 0;

            var leftB = p1.X < p2.X ? p1.X : p2.X;
            var topB = p1.Y > p2.Y ? p1.Y : p2.Y;
            var rightB = p1.X > p2.X ? p1.X : p2.X;
            var bottomB = p1.Y < p2.Y ? p1.Y : p2.Y;

            if (p1.Y > p2.Y)
            {
                ++rev;
            }

            if (p1.X > p2.X)
            {
                ++rev;
            }

            if (rev % 2 == 0)
            {
                incB = 1;
            }

            var leftC = leftA > leftB ? leftA : leftB;
            var topC = topA < topB ? topA : topB;
            var rightC = rightA < rightB ? rightA : rightB;
            var bottomC = bottomA > bottomB ? bottomA : bottomB;

            if (bottomC > topC || leftC > rightC)
            {
                return null;
            }

            var kA = 0.0;
            var kB = 0.0;
            var mA = 0.0;
            var mB = 0.0;

            var isPointA = false;
            var isPointB = false;
            var kInfA = false;
            var kInfB = false;

            var dx = Math.Abs(rightA - leftA);
            var dy = topA - bottomA;
            if (incA == 1 && dy < 0)
            {
                dy = -dy;
            }

            if (incA == 0 && dy > 0)
            {
                dy = -dy;
            }

            if (MathHelper.DoubleIsZero(dx))
            {
                if (MathHelper.DoubleIsZero(dy))
                {
                    isPointA = true;
                }
                else
                {
                    kInfA = true;
                }
            }
            else
            {
                kA = dy / dx;
                mA = p3.Y - kA * p3.X;
            }

            dx = Math.Abs(rightB - leftB);
            dy = topB - bottomB;
            if (incB == 1 && dy < 0)
            {
                dy = -dy;
            }

            if (incB == 0 && dy > 0)
            {
                dy = -dy;
            }

            if (MathHelper.DoubleIsZero(dx))
            {
                if (MathHelper.DoubleIsZero(dy))
                {
                    isPointB = true;
                }
                else
                {
                    kInfB = true;
                }
            }
            else
            {
                kB = dy / dx;
                mB = p1.Y - kB * p1.X;
            }

            // A point, B line
            if (isPointA && isPointB)
            {
                if (!MathHelper.DoublesAreEqual(leftC, rightC) || !MathHelper.DoublesAreEqual(topC, bottomC))
                {
                    return null;
                }
                x = leftC;
                y = topC;
                return new Point(x, y);
            }

            // A point, B line
            if (isPointA)
            {
                if (kInfB)
                {
                    x = leftA;
                    y = topA;
                    return new Point(x, y);
                }

                if (!MathHelper.DoubleIsZero(kB * leftA + mB - topA))
                {
                    return null;
                }
                x = leftA;
                y = topA;
                return new Point(x, y);
            }

            // A line, B point
            if (isPointB)
            {
                if (kInfA)
                {
                    x = leftB;
                    y = topB;
                    return new Point(x, y);
                }

                if (!MathHelper.DoubleIsZero(kA * leftB + mA - topB))
                {
                    return null;
                }
                x = leftB;
                y = topB;
                return new Point(x, y);
            }

            // A, line, B, line
            if (kInfA && kInfB)
            {
                x = leftC;
                y = bottomC;
                xx = rightC;
                yy = topC;
                if (MathHelper.DoublesAreEqual(x, xx) && MathHelper.DoublesAreEqual(y, yy))
                {
                    return new Point(x, y);
                }

                return new Curve(new Point(x, y), new Point(xx, yy));
            }

            if (kInfA == false && kInfB == false && MathHelper.DoublesAreEqual(kA, kB))
            {
                if (!MathHelper.DoublesAreEqual(mA, mB))
                {
                    return null;
                }
                if (kA < 0)
                {
                    x = leftC;
                    y = topC;
                    xx = rightC;
                    yy = bottomC;
                }
                else
                {
                    x = leftC;
                    y = bottomC;
                    xx = rightC;
                    yy = topC;
                }

                if (MathHelper.DoublesAreEqual(x, xx) && MathHelper.DoublesAreEqual(y, yy))
                {
                    return new Point(x, y);
                }

                if (xx < leftA || xx > rightA || yy < bottomA || yy > topA)
                {
                    return null;
                }

                if (xx < leftB || xx > rightB || yy < bottomB || yy > topB)
                {
                    return null;
                }

                return new Curve(new Point(x, y), new Point(xx, yy));
            }

            if (kInfA)
            {
                x = leftA;
                y = kB * x + mB;
                if (y <= topA && y >= bottomA)
                {
                    return new Point(x, y);
                }

                return null;
            }

            if (kInfB)
            {
                x = leftB;
                y = kA * x + mA;
                if (y <= topB && y >= bottomB)
                {
                    return new Point(x, y);
                }

                return null;
            }

            x = (mB - mA) / (kA - kB);
            y = kA * x + mA;

            if (x < leftA || x > rightA || y < bottomA || y > topA)
            {
                return null;
            }

            if (x < leftB || x > rightB || y < bottomB || y > topB)
            {
                return null;
            }

            if (MathHelper.DoubleIsZero((kA * x) + mA - y) && MathHelper.DoubleIsZero((kB * x) + mB - y))
            {
                return new Point(x, y);
            }

            return null;
        }

        public static Curve CreateParallelLine(Point p1, Point p2, double offset)
        {
            var result = new Curve();
            var l = PointStrategies.EuclidianDistance(p1, p2);
            var pnt1X = p1.X + offset * ((p2.Y - p1.Y) / l);
            var pnt2X = p2.X + offset * ((p2.Y - p1.Y) / l);
            var pnt1Y = p1.Y + offset * ((p1.X - p2.X) / l);
            var pnt2Y = p2.Y + offset * ((p1.X - p2.X) / l);
            result.Points.Add(new Point(pnt1X, pnt1Y));
            result.Points.Add(new Point(pnt2X, pnt2Y));
            return result;
        }

        private static void GetNewEndCoordinates(
            Curve curve,
            double toDistance,
            int i,
            double accLen,
            Curve modifiedCurve)
        {
            var newEnd = PointStrategies.PointAlongLine2D(curve.Points[i], curve.Points[i + 1], toDistance - accLen);
            modifiedCurve.Points.Add(new Point(newEnd[0], newEnd[1]));
        }

        private static void GetNewStartCoordinates(
            Curve curve,
            double fromDistance,
            int i,
            double accLen,
            Curve modifiedCurve)
        {
            var newStart = PointStrategies.PointAlongLine2D(curve.Points[i], curve.Points[i + 1], fromDistance - accLen);
            modifiedCurve.Points.Add(new Point(newStart[0], newStart[1]));
        }

        public static Curve CutCurve(Curve curve, double fromDistance, double toDistance)
        {
            var prevCurve = curve.RemoveOverlappingPoints2D();

            var accLen = 0d;
            var modifiedCurve = new Curve();
            var startFound = false;

            for (var i = 0; i < prevCurve.Points.Count - 1; i++)
            {
                var len = PointStrategies.EuclidianDistance(prevCurve.Points[i], prevCurve.Points[i + 1]);

                if (startFound == false && accLen + len >= fromDistance)
                {
                    GetNewStartCoordinates(prevCurve, fromDistance, i, accLen, modifiedCurve);
                    startFound = true;

                    if (accLen + len >= toDistance)
                    {
                        GetNewEndCoordinates(prevCurve, toDistance, i, accLen, modifiedCurve);
                        return modifiedCurve;
                    }

                    if (!MathHelper.DoublesAreEqual(accLen + len, fromDistance))
                    {
                        modifiedCurve.Points.Add(prevCurve.Points[i + 1]);
                    }
                }
                else if (accLen + len >= toDistance)
                {
                    if (!MathHelper.DoublesAreEqual(accLen + len, toDistance))
                    {
                        modifiedCurve.Points.Add(prevCurve.Points[i]);
                    }
                    GetNewEndCoordinates(prevCurve, toDistance, i, accLen, modifiedCurve);
                    return modifiedCurve;
                }
                else if (startFound)
                {
                    modifiedCurve.Points.Add(prevCurve.Points[i]);
                }

                accLen += len;
            }

            modifiedCurve.Points.Add(prevCurve.Points.Last());
            return modifiedCurve;
        }
    }
}
