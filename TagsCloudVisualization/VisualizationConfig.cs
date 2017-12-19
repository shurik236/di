namespace TagsCloudVisualization
{
    public class VisualizationConfig
    {
        public int Height { get; set; } = 600;
        public int Width { get; set; } = 800;
        public int TagLimit { get; set; } = 120;
        public double CloudSparcity { get; set; } = 0.1;
        public double CloudStep { get; set; } = 0.1;

    }
}