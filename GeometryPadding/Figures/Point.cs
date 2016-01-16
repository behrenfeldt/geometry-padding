using System;

namespace GeometryPadding.Figures
{
    using GeometryPadding.Strategies;

    public class Point : Figure, IComparable
    {
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public Point(Point p)
        {
            this.x = p.X;
            this.y = p.Y;
        }

        #region Point Properties 

        public double X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }

        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                this.y = value;
            }
        }

        #endregion

        #region Point Private members
        private double x;

        private double y;
        #endregion

        #region Point Operators
        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }

        public static bool operator !=(Point p1, Point p2)
        {
            return !(p1 == p2);
        }

        public static Point operator *(Point p1, double c)
        {
            return new Point(p1.X * c, p1.Y * c);
        }

        public static Point operator /(Point p1, double c)
        {
            return new Point(p1.X / c, p1.Y / c);
        }

        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static bool operator ==(Point p1, Point p2)
        {
            if (ReferenceEquals(p1, p2))
            {
                return true;
            }

            if (((object)p1 == null) || ((object)p2 == null))
            {
                return false;
            }

            return p1.Equals(p2);
        }

        #endregion

        #region Point Equality members
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return obj.GetType() == this.GetType() && this.Equals((Point)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.x.GetHashCode();
                hashCode = (hashCode * 397) ^ this.y.GetHashCode();
                return hashCode;
            }
        }

        public int CompareTo(object obj)
        {
            var o = (Point)obj;
            var xCmp = this.X.CompareTo(o.X);
            return xCmp != 0 ? xCmp : this.Y.CompareTo(o.Y);
        }

        private bool Equals(Point other)
        {
            return this.x.Equals(other.x) && this.y.Equals(other.y);
        }
        #endregion

        public double EuclidianDistanceTo(Point p)
        {
            return PointStrategies.EuclidianDistance(this, p);
        }

        public override string ToString()
        {
            return $"{this.X} {this.Y}";
        }
    }
}
