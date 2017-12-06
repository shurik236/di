using static System.Math;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class Geometry
    {
        public static Point PolarToCartesian(double r, double phi, Point center)
        {
            var x = Round(r * Cos(phi)) + center.X;
            var y = Round(r * Sin(phi)) + center.Y;

            return new Point((int)x, (int)y);
        }
    }
}
