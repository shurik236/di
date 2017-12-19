namespace TagsCloudVisualization.SupportModules
{
    internal class CloudFactory : IIntersectionContainerFactory
    {
        public IIntersectionContainer Create() => new Cloud();
    }
}
