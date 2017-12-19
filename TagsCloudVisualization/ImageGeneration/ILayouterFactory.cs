namespace TagsCloudVisualization.ImageGeneration
{
    public interface ILayouterFactory
    {
        ITagLayouter Create(VisualizationConfig config);
    }
}
