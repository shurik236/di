using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.SupportModules
{
    public interface IIntersectionContainer : IList<Rectangle>
    {
        bool IntersectsWith(Rectangle rect);
    }
}
