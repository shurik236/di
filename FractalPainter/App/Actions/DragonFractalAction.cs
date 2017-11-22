using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;

namespace FractalPainting.App.Actions
{
	public class DragonFractalAction : IUiAction
	{
	    private IDragonPainterFactory factory;
	    private Func<Random, DragonSettingsGenerator> createDragonSettingsGenerator;

	    public DragonFractalAction(IDragonPainterFactory factory,
            Func<Random, DragonSettingsGenerator> createDragonSettingsGenerator)
	    {
	        this.factory = factory;
	        this.createDragonSettingsGenerator = createDragonSettingsGenerator;
	    }

		public string Category => "Фракталы";
		public string Name => "Дракон";
		public string Description => "Дракон Хартера-Хейтуэя";

		public void Perform()
		{
		    var dragonSettings = CreateRandomSettings();
			// редактируем настройки:
			SettingsForm.For(dragonSettings).ShowDialog();
			// создаём painter с такими настройками
			factory.Create(dragonSettings).Paint();
		}

	    private DragonSettings CreateRandomSettings()
	    {
	        return createDragonSettingsGenerator(new Random()).Generate();
	    }
	}
}