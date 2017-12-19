namespace TagsCloudVisualization.Walker
{
    internal class SpiralWalkerFactory : IWalkerFactory
    {
        public IWalker Create(VisualizationConfig config) => new SpiralWalker(config);
    }
}
