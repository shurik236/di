using System.Collections.Generic;

namespace TagsCloudVisualization
{
    internal interface ITextProcessor
    {
        IEnumerable<string> ProcessText(string text);
    }
}