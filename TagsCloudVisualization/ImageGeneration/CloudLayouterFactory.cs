using TagsCloudVisualization.SupportModules;
using TagsCloudVisualization.Walker;

namespace TagsCloudVisualization.ImageGeneration
{
    internal class CloudLayouterFactory : ILayouterFactory
    {
        private readonly IIntersectionContainerFactory containerGenerator;
        private readonly IWalkerFactory pointWalkerGenerator;

        public CloudLayouterFactory(IIntersectionContainerFactory containerGenerator, IWalkerFactory pointWalkerGenerator)
        {
            this.containerGenerator = containerGenerator;
            this.pointWalkerGenerator = pointWalkerGenerator;
        }

        public ITagLayouter Create(VisualizationConfig config)
        {
            return new CloudLayouter(containerGenerator.Create(), pointWalkerGenerator.Create(config));
        }
    }
}
