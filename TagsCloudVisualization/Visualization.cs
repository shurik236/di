using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class Visualization : IVisualiser
    {
        private ITagLayouter layouter;
        private int height;
        private int width;
        private int tagLimit;

        public Visualization(VisualizationConfig config, ITagLayouter layouter)
        {
            this.layouter = layouter;
            height = config.Height;
            width = config.Width;
            tagLimit = config.TagLimit;
        }

        public Image DrawTags(IEnumerable<Tag> tags)
        {
            var img = new Bitmap(width, height);
            var g = Graphics.FromImage(img);
            g.FillRectangle(new SolidBrush(Color.Black), 0, 0, width, height); 
            foreach (var tag in tags.Take(tagLimit))
            {
                var rect = layouter.PutNextRectangle(tag.GetSize());
                g.DrawString(tag.Value, tag.Font, tag.Brush, rect);
            }

            return img;
        }
    }
}
