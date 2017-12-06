using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    class TagGenerator : ITagGenerator
    {
        private Func<string, int, Brush> brushSelector;
        private Func<string, int, Font> fontSelector;
        private Func<Tag, IComparable> orderingFunc;

        public TagGenerator(GeneratorConfig config)
        {
            brushSelector = config.BrushSelector;
            fontSelector = config.FontSelector;
            orderingFunc = config.OrderingFunc;
        }

        private IEnumerable<Tuple<string, int>> CountFerquency(IEnumerable<string> words)
        {
            return words.GroupBy(w => w).Select(gr => Tuple.Create(gr.Key, gr.Count()));
        }

        private Tag GenerateSingleTag(string word, int frequency)
        {
            return new Tag(word, (Font) fontSelector.DynamicInvoke(word, frequency),
                (Brush) brushSelector.DynamicInvoke(word, frequency), frequency);
        }

        public IEnumerable<Tag> GenerateTags(IEnumerable<string> words)
        {
            var tags = CountFerquency(words)
                .Select(t => GenerateSingleTag(t.Item1, t.Item2));

            return tags.OrderBy(t => (IComparable) orderingFunc.DynamicInvoke(t));
        }

    }
}
