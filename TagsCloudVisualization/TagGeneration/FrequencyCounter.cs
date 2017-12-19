using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.TagGeneration
{
    internal class FrequencyCounter : IWeightAssigner
    {
        public IEnumerable<Tuple<string, int>> AssignWeights(IEnumerable<string> words)
        {
            return words.GroupBy(w => w).Select(gr => Tuple.Create(gr.Key, gr.Count()));
        }
    }
}
