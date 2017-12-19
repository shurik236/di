using System;
using System.Windows.Forms;
using Castle.Windsor;
using TagsCloudVisualization.ImageGeneration;

namespace TagsCloudVisualization
{
    internal class Program
    {
        [STAThread]
        public static void Main()
        {
            var container = new WindsorContainer();
            container.Install(new TagsCloudVisualizerModule());
            var appForm = new AppForm(container.Resolve<ImageGenerator>(), container.Resolve<VisualizationConfig>(), container.Resolve<TagGeneratorConfig>());
            Application.Run(appForm);
        }
    }
}
