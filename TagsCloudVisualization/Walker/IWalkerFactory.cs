namespace TagsCloudVisualization.Walker
{
    internal interface IWalkerFactory
    {
        IWalker Create(VisualizationConfig config);
    }
}
