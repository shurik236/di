using System;
using System.Collections.Generic;
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

        private static Result<Tag> GenerateSingleTag(string word, int frequency, TagGeneratorConfig config)
        {
            return new Tag(word, config.FontSelector(word, frequency),
                config.BrushSelector(word, frequency), frequency);
        }

        public Result<IEnumerable<Tag>> GenerateTags(IEnumerable<string> words, TagGeneratorConfig config)
        {
            var assignedWeights = weightAssigner.AssignWeights(words);
            var tags = assignedWeights
                .Select(t => GenerateSingleTag(t.Item1, t.Item2, config).OnFail(Console.WriteLine))
                .Select(result => result.Value)
                .ToList();

            return Result.Ok(tags.OrderBy(t => config.OrderingFunc(t)).AsEnumerable());
        }

    }
}
