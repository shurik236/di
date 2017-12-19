using System.Drawing;
using TagsCloudVisualization.SupportModules;

namespace TagsCloudVisualization.Walker
{
    public class SpiralWalker : IWalker
    {
        public Point Center { get; set; }
        private readonly double angularStep;
        private readonly double initialRadius;
        private readonly double sparcity;
        private double angle;

        public SpiralWalker(VisualizationConfig config)
        {
            Center = new Point(config.Width/2, config.Height/2);
            angularStep = config.CloudStep;
            initialRadius = 0.0;
            sparcity = config.CloudSparcity;
        }

        public Point GetNextPoint()
        {
            angle += angularStep;
            var radius = initialRadius + angle * sparcity;
            return Geometry.PolarToCartesian(radius, angle, Center);
        }

        public void Reset()
        {
            angle = 0;
        }
    }
}
