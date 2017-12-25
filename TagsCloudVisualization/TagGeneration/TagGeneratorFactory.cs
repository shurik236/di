namespace TagsCloudVisualization.TagGeneration
{
    class TagGeneratorFactory : ITagGeneratorFactory
    {
        public ITagGenerator Create(IWeightAssigner weightAssigner, TagGeneratorConfig config)
        {
            return new TagGenerator(weightAssigner, config);
        }
    }
}
