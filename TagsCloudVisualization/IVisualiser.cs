using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    interface IVisualiser
    {
        Image DrawTags(IEnumerable<Tag> tags);
    }
}
