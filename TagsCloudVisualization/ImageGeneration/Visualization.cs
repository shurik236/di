using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.TagGeneration;

namespace TagsCloudVisualization.ImageGeneration
{
    public class Visualization : IVisualiser
    {
        private readonly ILayouterFactory layouterGenerator;

        public Visualization(ILayouterFactory layouterGenerator)
        {
            this.layouterGenerator = layouterGenerator;
        }

        public Image DrawTags(IEnumerable<Tag> tags, VisualizationConfig config)
        {
            var img = new Bitmap(config.Width, config.Height);
            var layouter = layouterGenerator.Create(config);
            
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
