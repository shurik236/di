using System.Collections.Generic;

namespace TagsCloudVisualization.TagGeneration
{
    internal interface ITagGenerator
    {
        IEnumerable<Tag> GenerateTags(IEnumerable<string> words, TagGeneratorConfig config);
    }
}