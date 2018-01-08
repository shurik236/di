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
            return wordExtractor.ExtractWords(text)
                .Then(words => tagGenerator.GenerateTags(words, tagConfig))
                .Then(tags => DrawCloud(tags, visualizationConfig));
        }

        private Result<Image> DrawCloud(IEnumerable<Tag> tags, VisualizationConfig config)
        {
            return Result.Of(() => CreateImageTemplate(config), "Failed to draw cloud.")
                .Then(image => DrawBackground(image, new SolidBrush(Color.Black)))
                .Then(image => DrawAllTags(image, tags.Take(config.TagLimit), layouterFactory.Create(config)));
        }

        private static Image CreateImageTemplate(VisualizationConfig config)
        {
            //I'm a STUB!
            return new Bitmap(config.Width, config.Height);
        }

        private static Image DrawBackground(Image img, Brush brush)
        {
            Graphics.FromImage(img).FillRectangle(brush, 0, 0, img.Width, img.Height);
            return img;
        }

        private static Result<None> DrawSingleTag(Image img, Tag tag, ITagLayouter layouter)
        {
            return Result.Of(tag.GetSize)
                .Then(layouter.PutNextRectangle)
                .Then(rect => Graphics.FromImage(img).DrawString(tag.Value, tag.Font, tag.Brush, rect));
        }

        private static Result<Image> DrawAllTags(Image img, IEnumerable<Tag> tags, ITagLayouter layouter)
        {
            return tags.Aggregate(Result.Ok(),
                    (result, nextTag) => result.Then(_ => DrawSingleTag(img, nextTag, layouter)))
                .Then(_ => img);
        }
    }
}
