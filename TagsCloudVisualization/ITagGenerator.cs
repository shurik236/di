using System.Collections.Generic;

namespace TagsCloudVisualization
{
    internal interface ITagGenerator
    {
        IEnumerable<Tag> GenerateTags(IEnumerable<string> words);
    }
}