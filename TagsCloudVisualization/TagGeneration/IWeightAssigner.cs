using System;
using System.Collections.Generic;

namespace TagsCloudVisualization.TagGeneration
{
    internal interface IWeightAssigner
    {
        IEnumerable<Tuple<string, int>> AssignWeights(IEnumerable<string> words);
    }
}
