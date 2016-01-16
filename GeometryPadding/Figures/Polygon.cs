using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryPadding.Figures
{
    public class Polygon : Curve
    {
        public enum Preposition { Inside, Outside, On }

        public Polygon() { }

        public Polygon(params Point[] points)
        {
            this.Points = points;
        }

        public Polygon(IList<Point> points)
        {
            this.Points = points ?? new List<Point>();
        }

        public void ValidatePolygon()
        {
            if (this.Points.First() != this.Points.Last())
            {
                throw new Exception("The endpoints of the polygon must be connected. ");
            }
        }

        public string ToWkt()
        {
            return $"(({string.Join(", ", this.Points)}))";
        }
    }
}
