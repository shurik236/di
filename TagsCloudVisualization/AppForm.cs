using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TagsCloudVisualization.ImageGeneration;

namespace TagsCloudVisualization
{
    internal class AppForm : Form
    {
        private readonly ImageGenerator imgGenerator;
        private readonly VisualizationConfig visualizationConfig;
        private readonly TagGeneratorConfig tagConfig;

        public AppForm(ImageGenerator imgGenerator, VisualizationConfig visualizationConfig, TagGeneratorConfig tagConfig)
        {
            this.imgGenerator = imgGenerator;
            this.visualizationConfig = visualizationConfig;
            this.tagConfig = tagConfig;
            ClientSize = new Size(300, 200);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            var menuStrip = new MenuStrip
            {
                Location = new Point(0, 0),
                Name = "MenuStrip"
            };

            var fileMenu = new ToolStripMenuItem
            {
                Name = "fileMenu",
                Text = "File"
            };

            var configMenu = new ToolStripMenuItem
            {
                Name = "configure",
                Text = "Configure"
            };

            fileMenu.DropDownItems.Add(new ToolStripMenuItem("Open", null, Open_OnClick));
            fileMenu.DropDownItems.Add(new ToolStripMenuItem("Save", null, Save_OnClick));
            configMenu.DropDownItems.Add(new ToolStripMenuItem("Visualization", null, ConfigureVisualization_OnClick));
            menuStrip.Items.Add(fileMenu);
            menuStrip.Items.Add(configMenu);

            MainMenuStrip = menuStrip;
            Controls.Add(menuStrip);
        }

        private void ConfigureVisualization_OnClick(object sender, EventArgs e)
        {
            SettingsForm.For(visualizationConfig).ShowDialog();
        }

        private void Save_OnClick(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog {Filter = "bmp files(*.bmp) | *.bmp"};
            if (dialog.ShowDialog() != DialogResult.OK) return;
            var saveResult = Result.OfAction(() => BackgroundImage.Save(dialog.FileName));
            if (!saveResult.IsSuccess)
                Console.WriteLine(saveResult.Error);
        }

        private void Open_OnClick(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog {Filter = "txt files(*.txt) | *.txt"};
            if (dialog.ShowDialog() != DialogResult.OK) return;

            string text;

            using (var fs = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read))
            using (var bs = new BufferedStream(fs))
            using (var sr = new StreamReader(bs))
            {
                text = sr.ReadToEnd();
            }

            var img = imgGenerator.GenerateImage(text, tagConfig, visualizationConfig);
            if (!img.IsSuccess)
            {
                Console.WriteLine(img.Error);
                return;
            }
            BackgroundImage = img.Value;
            ClientSize = BackgroundImage.Size;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Text = "Tag cloud";
        }
    }
}
