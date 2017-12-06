using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using TagsCloudVisualization.DocumentReader;
using TagsCloudVisualization.Walker;

namespace TagsCloudVisualization
{
    class Program
    {
        private static WindsorContainer GenerateContainer()
        {
            var container = new WindsorContainer();
            container.AddFacility<TypedFactoryFacility>();
            container.Register(Component.For<VisualizationConfig>().ImplementedBy<VisualizationConfig>());
            container.Register(Component.For<GeneratorConfig>().ImplementedBy<GeneratorConfig>());

            container.Register(Component.For<IWalker>()
                .ImplementedBy<SpiralWalker>()
                .DependsOn(
                    Dependency.OnValue("center", new Point(400, 300)),
                    Dependency.OnValue("step", 0.1),
                    Dependency.OnValue("initialRadius", 0.0),
                    Dependency.OnValue("sparcity", 0.1)));

            container.Register(Component.For<IIntersectionContainer>().ImplementedBy<Cloud>());
            container.Register(Component.For<ITagGenerator>().ImplementedBy<TagGenerator>());
            container.Register(Component.For<ITagLayouter>().ImplementedBy<CloudLayouter>());
            container.Register(Component.For<IGeneratorFactory>().AsFactory());
            container.Register(Component.For<Visualization>().ImplementedBy<Visualization>());
            container.Register(Component.For<IDocumentReader>().ImplementedBy<TextFileReader>());
            container.Register(Component.For<ITextProcessor>().ImplementedBy<TextProcessor>());

            return container;
        }
        public static void Main()
        {
            var container = GenerateContainer();
            
            var reader = container.Resolve<IDocumentReader>();
            var tagGenerator = container.Resolve<ITagGenerator>();
            var path = Path.Combine(
                Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory),
                "..",
                "..",
                "guide.txt"
            );
            var text = reader.ReadTextFromFile(path);
            var filteredWords = container.Resolve<ITextProcessor>().ProcessText(text);
            var tags = tagGenerator.GenerateTags(filteredWords);
            var visualizer = container.Resolve<Visualization>();

            var appForm = new AppForm(container.Resolve<VisualizationConfig>());
            appForm.BackgroundImage = visualizer.DrawTags(tags);
            Application.Run(appForm);
        }
    }
}
