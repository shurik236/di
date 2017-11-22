using System;
using System.Linq;
using System.Windows.Forms;
using FractalPainting.App.Actions;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;
using Ninject.Extensions.Factory;

namespace FractalPainting.App
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var dependencyContainer = new StandardKernel();

                dependencyContainer.Bind<IUiAction>().To<DragonFractalAction>();
                dependencyContainer.Bind<IUiAction>().To<ImageSettingsAction>();
                dependencyContainer.Bind<IUiAction>().To<KochFractalAction>();
                dependencyContainer.Bind<IUiAction>().To<PaletteSettingsAction>();
                dependencyContainer.Bind<IUiAction>().To<SaveImageAction>();

                dependencyContainer.Bind<IImageHolder, PictureBoxImageHolder>().To<PictureBoxImageHolder>()
                    .InSingletonScope();
                dependencyContainer.Bind<Palette>().ToSelf().InSingletonScope();

                dependencyContainer.Bind<IDragonPainterFactory>().ToFactory();

                dependencyContainer.Bind<IObjectSerializer>().To<XmlObjectSerializer>()
                    .WhenInjectedInto<SettingsManager>();
                dependencyContainer.Bind<IBlobStorage>().To<FileBlobStorage>().WhenInjectedInto<SettingsManager>();

                var mainForm = dependencyContainer.Get<MainForm>();

                Application.Run(mainForm);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}