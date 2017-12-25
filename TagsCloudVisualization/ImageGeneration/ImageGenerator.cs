using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.TagGeneration;

namespace TagsCloudVisualization.ImageGeneration
{
    internal class ImageGenerator
    {
        private readonly ITagGenerator tagGenerator;
        private readonly ITextExtractor wordExtractor;
        private readonly ILayouterFactory layouterFactory;
        
        public ImageGenerator(ITagGenerator tagGenerator, ITextExtractor wordExtractor, ILayouterFactory layouterFactory)
        {
            this.tagGenerator = tagGenerator;
            this.wordExtractor = wordExtractor;
            this.layouterFactory = layouterFactory;
        }

        public Image GenerateImage(string text, TagGeneratorConfig tagConfig, VisualizationConfig visualizationConfig)
        {
            var filteredText = wordExtractor.ExtractWords(text);
            return DrawTags(tagGenerator.GenerateTags(filteredText, tagConfig), visualizationConfig);
        }

        private Image DrawTags(IEnumerable<Tag> tags, VisualizationConfig config)
        {
            var img = new Bitmap(config.Width, config.Height);
            var layouter = layouterFactory.Create(config);

            var g = Graphics.FromImage(img);
            g.FillRectangle(new SolidBrush(Color.Black), 0, 0, config.Width, config.Height);
            foreach (var tag in tags.Take(config.TagLimit))
            {
                var rect = layouter.PutNextRectangle(tag.GetSize());
                g.DrawString(tag.Value, tag.Font, tag.Brush, rect);
            }
            return img;
        }
    }
}
