using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TagsCloudVisualization.ImageGeneration;
using TagsCloudVisualization.SupportModules;
using TagsCloudVisualization.TagGeneration;
using TagsCloudVisualization.Walker;

namespace TagsCloudVisualization
{
    internal class TagsCloudVisualizerModule : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<VisualizationConfig>().ImplementedBy<VisualizationConfig>());
            container.Register(Component.For<TagGeneratorConfig>().ImplementedBy<TagGeneratorConfig>());
            container.Register(Component.For<IWeightAssigner>().ImplementedBy<FrequencyCounter>());
            container.Register(Component.For<IIntersectionContainerFactory>().ImplementedBy<CloudFactory>());
            container.Register(Component.For<IWalkerFactory>().ImplementedBy<SpiralWalkerFactory>());
            container.Register(Component.For<ILayouterFactory>().ImplementedBy<CloudLayouterFactory>());
            container.Register(Component.For<IWalker>().ImplementedBy<SpiralWalker>().LifestyleTransient());
            container.Register(Component.For<IIntersectionContainer>().ImplementedBy<Cloud>().LifestyleTransient());
            container.Register(Component.For<ITagGenerator>().ImplementedBy<TagGenerator>().LifestyleTransient());
            container.Register(Component.For<ITagLayouter>().ImplementedBy<CloudLayouter>().LifestyleTransient());
            container.Register(Component.For<Visualization>().ImplementedBy<Visualization>().LifestyleTransient());
            container.Register(Component.For<ITextExtractor>().ImplementedBy<WordExtractor>());
            container.Register(Component.For<ImageGenerator>().ImplementedBy<ImageGenerator>().LifestyleTransient());
        }
    }
}
