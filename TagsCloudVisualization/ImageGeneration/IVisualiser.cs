using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.TagGeneration;

namespace TagsCloudVisualization.ImageGeneration
{
    internal interface IVisualiser
    {
        Image DrawTags(IEnumerable<Tag> tags, VisualizationConfig config);
    }
}
