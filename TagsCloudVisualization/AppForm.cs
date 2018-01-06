using System;
using System.Collections.Generic;
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
            Result.Of(SetSavePath, "Failed to save image.")
                .Then(BackgroundImage.Save)
                .OnFail(error => MessageBox.Show(error));
        }

        private static string SetSavePath()
        {
            var dialog = new SaveFileDialog { Filter = "bmp files(*.bmp) | *.bmp" };
            dialog.ShowDialog();
            return dialog.FileName;
        }

        private void Open_OnClick(object sender, EventArgs e)
        {
            Result.Of(SetPathToText, "Working with txt only.")
                .Then(GetText)
                .Then(text => imgGenerator.GenerateImage(text, tagConfig, visualizationConfig))
                .Then(SetBackgroundImage)
                .OnFail(error => MessageBox.Show(error));
        }

        private static string GetText(string path)
        {
            string text;
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (var bs = new BufferedStream(fs))
            using (var sr = new StreamReader(bs))
            {
                text = sr.ReadToEnd();
            }
            return text;
        }

        private static string SetPathToText()
        {
            var dialog = new OpenFileDialog { Filter = "txt files(*.txt) | *.txt" };
            dialog.ShowDialog();
            return dialog.FileName;
        }

        private void SetBackgroundImage(Image img)
        {
            BackgroundImage = img;
            ClientSize = BackgroundImage.Size;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Text = "Tag cloud";
        }

    }
}
