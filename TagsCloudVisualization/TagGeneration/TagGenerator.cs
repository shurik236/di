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
            var tags = new List<Tag>();
            foreach (var t in assignedWeights)
            {
                var result = GenerateSingleTag(t.Item1, t.Item2, config);
                if (!result.IsSuccess)
                    return Result.Fail<IEnumerable<Tag>>("Tag generation failed. " + result.Error);
                tags.Add(result.Value);
            }

            return Result.Ok(tags.OrderBy(t => config.OrderingFunc(t)).AsEnumerable());
        }

    }
}
