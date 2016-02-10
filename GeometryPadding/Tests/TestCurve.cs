namespace GeometryPadding.Tests
{
    using System.Globalization;
    using System.Linq;

    using Figures;

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

        [TestCase("LINESTRING (140246.116 6569853.68, 140248.809 6569871.397, 140252.873 6569904.517, 140255.205 6569920.486, 140258.1810254 6569940.8646021, 140255.205 6569920.486, 140260.972 6569960.627, 140260.454 6569956.429, 140258.1810254 6569940.8646021, 140264.6760465 6569990.5984698, 140260.972 6569960.627, 140269.606 6570035.296, 140265.811 6569999.782, 140264.6760465 6569990.5984698, 140273.5043298 6570063.6626036, 140273.035 6570060.287, 140269.606 6570035.296, 140277.635 6570093.372, 140273.5043298 6570063.6626036, 140279.3762111 6570106.518872, 140277.635 6570093.372, 140291.7398674 6570198.7107463, 140287.183 6570167.424, 140281.701 6570124.072, 140279.3762111 6570106.518872, 140292.945 6570206.985, 140291.7398674 6570198.7107463, 140299.057323 6570250.8546082, 140295.728 6570228.556, 140292.945 6570206.985, 140301.195 6570265.172, 140299.057323 6570250.8546082, 140305.137 6570301.17, 140302.793 6570281.712, 140301.195 6570265.172, 140315.9393169 6570413.4529412, 140308.762 6570331.274, 140305.137 6570301.17, 140316.594 6570420.954, 140316.543 6570420.365, 140315.9393169 6570413.4529412, 140322.071 6570484.589, 140316.594 6570420.954, 140324.611669 6570511.4992158, 140322.071 6570484.589, 140326.282 6570529.191, 140324.611669 6570511.4992158, 140326.282 6570529.191, 140328.005 6570554.52, 140329.89 6570575.586, 140329.429 6570589.223, 140327.04 6570601.727, 140323.69 6570610.147, 140316.739 6570621.717, 140310.317 6570630.344, 140302.111 6570637.688, 140291.123 6570645.233, 140277.777 6570652.094, 140255.879 6570663.667, 140230.192 6570675.917, 140223.769 6570678.888, 140223.769 6570678.888, 140167.053 6570705.129, 140128.282 6570722.973, 140100.589 6570735.764, 140085.399 6570742.078, 140067.519 6570747.759, 140048.534 6570751.539, 140028.917 6570753.736, 140013.463 6570755.073, 140013.463 6570755.073, 139965.699 6570758.3, 139895.034 6570762.905, 139895.034 6570762.905, 139876.638 6570764.278, 139847.494 6570765.71, 139661.529 6570786.378, 139701.538 6570781.514, 139745.404 6570776.397, 139760.469 6570774.734, 139784.005 6570772.578, 139808.728 6570769.831, 139847.494 6570765.71, 139661.529 6570786.378, 139640.847 6570788.365, 139620.152 6570790.07, 139599.965 6570791.411, 139590.415 6570792.243, 139590.415 6570792.243, 139578.368 6570792.11, 139547.263 6570792.292, 139510.732 6570791.008, 139473.366 6570787.843, 139432.246 6570782.17, 139396.969 6570777.965, 139361.693 6570773.967, 139345.267 6570771.136, 139334.351 6570767.68, 139334.351 6570767.68, 139324.934 6570763.292, 139317.428 6570757.562, 139309.106 6570749.702, 139302.851 6570741.137, 139297.224 6570729.647, 139292.016 6570717.742, 139289.52 6570707.091, 139288.485 6570697.069, 139288.911 6570685.586, 139291.637 6570668.239, 139296.453 6570631.478, 139302.355 6570590.181, 139302.355 6570590.181, 139303.189 6570571.584, 139303.893 6570558.906, 139303.559 6570546.274, 139303.559 6570546.274, 139302.928 6570531.008, 139300.837 6570510.141, 139298.068 6570491.095, 139294.399 6570470.86, 139288.011 6570439.598, 139277.609 6570393.495, 139273.867 6570373.734, 139273.867 6570373.734, 139271.452 6570360.996, 139269.628 6570346.537, 139267.763 6570328.755, 139267.763 6570328.755, 139267.384 6570325.132, 139266.969 6570312.923, 139266.969 6570312.923, 139266.345 6570294.564, 139266.345 6570294.564, 139266.197275 6570290.21657, 139266.197275 6570290.21657, 139265.9 6570281.468, 139265.9 6570281.468, 139265.3610573 6570270.9568222, 139265.3610573 6570270.9568222, 139263.949 6570243.417, 139263.4862569 6570233.3426156, 139263.4862569 6570233.3426156, 139262.997 6570222.691, 139262.997 6570222.691, 139262.492 6570212.293, 139262.492 6570212.293, 139260.463 6570170.505, 139259.427 6570145.442, 139258.3384056 6570118.6316363, 139258.3384056 6570118.6316363, 139257.893 6570107.662, 139257.893 6570107.662, 139257.5043313 6570097.8521137, 139257.5043313 6570097.8521137, 139256.362 6570069.02, 139255.9594779 6570060.2486301, 139255.9594779 6570060.2486301, 139255.448 6570049.103, 139255.448 6570049.103, 139255.2010136 6570039.2430976, 139255.2010136 6570039.2430976, 139254.387 6570006.747, 139251.712 6569970.199, 139251.3747395 6569959.8861552, 139251.3747395 6569959.8861552, 139251.003 6569948.519, 139251.003 6569948.519, 139250.2220236 6569936.7792436, 139250.2220236 6569936.7792436, 139247.613 6569897.56)", 0d, 1d, 5d), Ignore("Behöver rättas")]
        public void CanCreatePolygonFromLine3(string wkt, double from, double to, double padding)
        {
            var start = wkt.IndexOf("(");
            var end = wkt.LastIndexOf(")");
            var points = wkt
                .Substring(start + 1, end - start - 1)
                .Split(',')
                .Select(s => s.Trim().Split(' '))
                .Select(s => new Point(double.Parse(s[0], CultureInfo.InvariantCulture), double.Parse(s[1], CultureInfo.InvariantCulture)))
                .ToList();
            var line = new Curve(points);
            var polygon = line.CreatePolygonFromCurve(from, to, padding);
            Assert.That(polygon, Is.Not.Null);
            Assert.DoesNotThrow(() => polygon.ValidatePolygon());
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
