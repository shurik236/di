using System.Drawing;

namespace TagsCloudVisualization.Walker
{
    public class SpiralWalker : IWalker
    {
        public Point Center { get; }
        private readonly double angularStep;
        private readonly double initialRadius;
        private readonly double sparcity;
        private double angle;

        public SpiralWalker(Point center, double step, double initialRadius, double sparcity)
        {
            Center = center;
            angularStep = step;
            this.initialRadius = initialRadius;
            this.sparcity = sparcity;
        }

        public Point GetNextPoint()
        {
            angle += angularStep;
            var radius = initialRadius + angle * sparcity;
            return Geometry.PolarToCartesian(radius, angle, Center);
        }

    }
}
