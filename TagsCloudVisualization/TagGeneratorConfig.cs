using System;
using System.Drawing;
using TagsCloudVisualization.TagGeneration;
using static System.Math;

namespace TagsCloudVisualization
{
    internal class TagGeneratorConfig
    {
        private static readonly Random Random = new Random();
        public Func<string, int, Brush> BrushSelector { get; set; } = (word, frequency) 
            => new SolidBrush(Color.FromArgb(Random.Next(255), Random.Next(255), Random.Next(255)));

        public Func<string, int, Font> FontSelector { get; set; } = (word, weight) =>
            new Font(FontFamily.GenericSansSerif, (int) Min(2 * Log(weight) + 12, 32));

        public Func<Tag, IComparable> OrderingFunc { get; set; } = tag => -tag.Weight;
    }
}
