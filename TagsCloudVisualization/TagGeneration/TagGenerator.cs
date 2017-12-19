using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization.TagGeneration
{
    internal class TagGenerator : ITagGenerator
    {
        private readonly IWeightAssigner weightAssigner;

        public TagGenerator(IWeightAssigner weightAssigner)
        {
            this.weightAssigner = weightAssigner;
        }

        private static Tag GenerateSingleTag(string word, int frequency, TagGeneratorConfig config)
        {
            return new Tag(word, (Font) config.FontSelector.DynamicInvoke(word, frequency),
                (Brush) config.BrushSelector.DynamicInvoke(word, frequency), frequency);
        }

        public IEnumerable<Tag> GenerateTags(IEnumerable<string> words, TagGeneratorConfig config)
        {
            var tags = weightAssigner.AssignWeights(words)
                .Select(t => GenerateSingleTag(t.Item1, t.Item2, config));

            return tags.OrderBy(t => (IComparable) config.OrderingFunc.DynamicInvoke(t));
        }

    }
}
