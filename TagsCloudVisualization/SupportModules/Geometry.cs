using System;
using System.Drawing;

namespace TagsCloudVisualization.SupportModules
{
    public class Geometry
    {
        public static Point PolarToCartesian(double r, double phi, Point center)
        {
            var x = Math.Round(r * Math.Cos(phi)) + center.X;
            var y = Math.Round(r * Math.Sin(phi)) + center.Y;

            return new Point((int)x, (int)y);
        }
    }
}
