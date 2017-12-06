using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public interface IIntersectionContainer : IEnumerable<Rectangle>
    {
        bool IntersectsWith(Rectangle rect);

        void Append(Rectangle rect);
    }
}
