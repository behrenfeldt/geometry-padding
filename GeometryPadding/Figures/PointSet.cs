using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryPadding.Figures
{
    public class PointSet : Figure
    {
        public PointSet() { }

        public PointSet(params Point[] points)
        {
            this.Points = points;
        }

        public PointSet(IList<Point> points)
        {
            this.Points = points ?? new List<Point>();
        }

        public IList<Point> Points { get; set; } = new List<Point>();

        public override string ToString()
        {
            return $"({string.Join(", ", this.Points)})";
        }
    }
}
