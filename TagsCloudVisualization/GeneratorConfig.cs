using System;
using System.Drawing;
using static System.Math;

namespace TagsCloudVisualization
{
    class GeneratorConfig
    {
        public Func<string, int, Brush> BrushSelector = (word, frequency) =>
        {
            var r = new Random();
            return new SolidBrush(Color.FromArgb(r.Next(255), r.Next(255), r.Next(255)));
        };

        public Func<string, int, Font> FontSelector = (word, frequency) =>
            new Font(FontFamily.GenericSansSerif, (int) Min(2 * Log(frequency) + 12, 32));

        public Func<Tag, IComparable> OrderingFunc = tag => -tag.Frequency;
    }
}
