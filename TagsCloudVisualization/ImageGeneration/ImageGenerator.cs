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

        public Result<Image> GenerateImage(string text, TagGeneratorConfig tagConfig, VisualizationConfig visualizationConfig)
        {
            var filteredText = wordExtractor.ExtractWords(text);
            return !filteredText.IsSuccess ? Result.Fail<Image>("Error while creating image. " + filteredText.Error) :
                DrawTags(tagGenerator.GenerateTags(filteredText.Value, tagConfig).Value, visualizationConfig);
        }

        private Result<Image> DrawTags(IEnumerable<Tag> tags, VisualizationConfig config)
        {
            var img = new Bitmap(config.Width, config.Height);
            var layouter = layouterFactory.Create(config);

            var g = Graphics.FromImage(img);
            g.FillRectangle(new SolidBrush(Color.Black), 0, 0, config.Width, config.Height);
            foreach (var tag in tags.Take(config.TagLimit))
            {
                var rect = layouter.PutNextRectangle(tag.GetSize());
                if (!rect.IsSuccess)
                    return Result.Fail<Image>("Error while creating image. " + rect.Error);
                g.DrawString(tag.Value, tag.Font, tag.Brush, rect.Value);
            }
            return img;
        }
    }
}
