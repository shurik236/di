using System.Drawing;
using TagsCloudVisualization.TagGeneration;

namespace TagsCloudVisualization.ImageGeneration
{
    internal class ImageGenerator
    {
        private readonly ITagGenerator tagGenerator;
        private readonly ITextExtractor wordExtractor;
        private readonly Visualization visualization;
        
        public ImageGenerator(ITagGenerator tagGenerator, ITextExtractor wordExtractor, Visualization visualization)
        {
            this.tagGenerator = tagGenerator;
            this.wordExtractor = wordExtractor;
            this.visualization = visualization;
        }

        public Image GenerateImage(string text, TagGeneratorConfig tagConfig, VisualizationConfig visualizationConfig)
        {
            var filteredText = wordExtractor.ExtractWords(text);
            return visualization.DrawTags(tagGenerator.GenerateTags(filteredText, tagConfig), visualizationConfig);
        }
    }
}
