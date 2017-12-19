using System.Collections.Generic;

namespace TagsCloudVisualization.TagGeneration
{
    internal interface ITextExtractor
    {
        IEnumerable<string> ExtractWords(string text);
    }
}