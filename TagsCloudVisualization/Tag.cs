using System.Drawing;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public class Tag
    {
        public Font Font { get; }
        public string Value { get; }
        public Brush Brush { get; }
        public int Frequency { get; }

        public Tag(string value, Font font, Brush brush, int frequency)
        {
            this.Value = value;
            this.Brush = brush;
            this.Font = font;
            this.Frequency = frequency;

        }

        public Size GetSize()
        {
            var size = TextRenderer.MeasureText(Value, Font);
            return new Size(size.Width+5, size.Height);
        }
    }
}
