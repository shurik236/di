using System;
using System.Drawing;

namespace TagsCloudVisualization.SupportModules
{
    public class Geometry
    {
        public static Result<Point> PolarToCartesian(double r, double phi, Point center)
        {
            if (r < 0) return Result.Fail<Point>("Radius can't be negative");
            var x = Math.Round(r * Math.Cos(phi)) + center.X;
            var y = Math.Round(r * Math.Sin(phi)) + center.Y;

            return Result.Ok(new Point((int)x, (int)y));
        }
    }
}
