namespace GeometryPadding.Tests
{
    using Figures;

    using GeometryPadding.Misc;

    using NUnit.Framework;

    [TestFixture]
    class TestCurve
    {
        [Test]
        public void CanGetLineLength()
        {
            var line = new Curve(new Point(3, 4), new Point(7, 7));
            var len = line.Length;
            var cachedLen = line.Length;
            Assert.AreEqual(len, 5.0);
            Assert.AreEqual(cachedLen, 5.0);
        }

        [Test]
        public void CanGetLineIntersections()
        {
            var line1 = new Curve(new Point(0, 0), new Point(4, 4));
            var line2 = new Curve(new Point(4, 0), new Point(0, 4));
            var intersection = (Point)line1.FindIntersection(line2);
            Assert.That(intersection.X, Is.EqualTo(2));
            Assert.That(intersection.Y, Is.EqualTo(2));
        }

        [Test]
        public void CanGetLineIntersectionsInf()
        {
            var line1 = new Curve(new Point(0, 0), new Point(1, 1));
            var line2 = new Curve(new Point(4, 0), new Point(3, 1));
            var intersection = (Point)line1.FindIntersectionInf(line2);
            Assert.That(intersection.X, Is.EqualTo(2));
            Assert.That(intersection.Y, Is.EqualTo(2));
        }

        [Test]
        public void CanCreatePolygonFromLine1()
        {
            var line = new Curve(new Point(0, 0), new Point(10, 0));
            var polygon = line.CreatePolygonFromCurve(0.2, 0.8, 1.0);
            Assert.That(polygon.Points.Count, Is.EqualTo(5));
            Assert.That(polygon.Points[0].X, Is.EqualTo(2));
            Assert.That(polygon.Points[0].Y, Is.EqualTo(1));
            Assert.That(polygon.Points[1].X, Is.EqualTo(8));
            Assert.That(polygon.Points[1].Y, Is.EqualTo(1));
            Assert.That(polygon.Points[2].X, Is.EqualTo(8));
            Assert.That(polygon.Points[2].Y, Is.EqualTo(-1));
            Assert.That(polygon.Points[3].X, Is.EqualTo(2));
            Assert.That(polygon.Points[3].Y, Is.EqualTo(-1));
            Assert.That(polygon.Points[4].X, Is.EqualTo(2));
            Assert.That(polygon.Points[4].Y, Is.EqualTo(1));
        }

        [Test]
        public void CanCreatePolygonFromLine2()
        {
            var line = new Curve(new Point(0, 0), new Point(0, 10), new Point(10, 10));
            var polygon = line.CreatePolygonFromCurve(0, 1, 5.0);
            Assert.That(polygon.Points.Count, Is.EqualTo(7));
            Assert.That(polygon.Points[0].X, Is.EqualTo(-5));
            Assert.That(polygon.Points[0].Y, Is.EqualTo(0));
            Assert.That(polygon.Points[1].X, Is.EqualTo(-5));
            Assert.That(polygon.Points[1].Y, Is.EqualTo(15));
            Assert.That(polygon.Points[2].X, Is.EqualTo(10));
            Assert.That(polygon.Points[2].Y, Is.EqualTo(15));
            Assert.That(polygon.Points[3].X, Is.EqualTo(10));
            Assert.That(polygon.Points[3].Y, Is.EqualTo(5));
            Assert.That(polygon.Points[4].X, Is.EqualTo(5));
            Assert.That(polygon.Points[4].Y, Is.EqualTo(5));
            Assert.That(polygon.Points[5].X, Is.EqualTo(5));
            Assert.That(polygon.Points[5].Y, Is.EqualTo(0));
            Assert.That(polygon.Points[6].X, Is.EqualTo(-5));
            Assert.That(polygon.Points[6].Y, Is.EqualTo(0));
            var s = polygon.ToWkt();
            Assert.AreEqual(s, "((-5 0, -5 15, 10 15, 10 5, 5 5, 5 0, -5 0))");
        }

        [Test]
        public void CanCreatePolygonRemoveOverlappingOffsettedPoints()
        {
            var line = new Curve(
                new Point(140269.53480959567, 6570034.62979288),
                new Point(140265.811, 6569999.782),
                new Point(140264.6760465, 6569990.5984698),
                new Point(140264.6760465, 6569990.5984698),
                new Point(140260.972, 6569960.627),
                new Point(140260.972, 6569960.627),
                new Point(140260.454, 6569956.429),
                new Point(140258.1810254, 6569940.8646021),
                new Point(140258.1810254, 6569940.8646021),
                new Point(140255.205, 6569920.486),
                new Point(140255.205, 6569920.486),
                new Point(140252.873, 6569904.517),
                new Point(140248.809, 6569871.397),
                new Point(140246.11606674892, 6569853.680439134)
                );

            line.CreatePolygonFromCurve(0.0, 1.0, 1.0);
        }
    }
}
