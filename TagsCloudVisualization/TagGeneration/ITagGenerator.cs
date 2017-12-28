using System.Collections.Generic;

namespace TagsCloudVisualization.TagGeneration
{
    internal interface ITagGenerator
    {
        Result<IEnumerable<Tag>> GenerateTags(IEnumerable<string> words, TagGeneratorConfig config);
    }
}