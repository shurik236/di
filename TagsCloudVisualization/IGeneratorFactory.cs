namespace TagsCloudVisualization
{
    interface IGeneratorFactory
    {
        ITagGenerator Create(GeneratorConfig config);
    }
}
