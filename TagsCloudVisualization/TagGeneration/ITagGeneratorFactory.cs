namespace TagsCloudVisualization.TagGeneration
{
    internal interface ITagGeneratorFactory
    {
        ITagGenerator Create(IWeightAssigner weightAssigner, TagGeneratorConfig config);
    }
}
