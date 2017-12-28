using System.Collections.Generic;

namespace TagsCloudVisualization.TagGeneration
{
    internal interface ITextExtractor
    {
        Result<IEnumerable<string>> ExtractWords(string text);
    }
}