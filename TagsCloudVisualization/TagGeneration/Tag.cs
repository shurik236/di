using System.Drawing;
using System.Windows.Forms;

namespace TagsCloudVisualization.TagGeneration
{
    public class Tag
    {
        public Font Font { get; }
        public string Value { get; }
        public Brush Brush { get; }
        public int Weight { get; }

        public Tag(string value, Font font, Brush brush, int weight)
        {
            this.Value = value;
            this.Brush = brush;
            this.Font = font;
            this.Weight = weight;
        }

        public Size GetSize()
        {
            var size = TextRenderer.MeasureText(Value, Font);
            return new Size((int)(size.Width*1.05), size.Height);
        }
    }
}
