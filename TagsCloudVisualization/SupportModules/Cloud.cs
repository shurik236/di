using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class Cloud : IIntersectionContainer

    {
        private readonly List<Rectangle> rectangles;
        public Cloud()
        {
            rectangles = new List<Rectangle>();
        }

        public bool IntersectsWith(Rectangle rect)
        {
            return rectangles.Any(r => r.IntersectsWith(rect));
        }

        public void Append(Rectangle rect)
        {
            rectangles.Add(rect);
        }

        public IEnumerator<Rectangle> GetEnumerator()
        {
            return ((IEnumerable<Rectangle>)rectangles).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Rectangle>)rectangles).GetEnumerator();
        }
    }
}
