using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization.SupportModules
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

        public IEnumerator<Rectangle> GetEnumerator()
        {
            return ((IEnumerable<Rectangle>)rectangles).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Rectangle>)rectangles).GetEnumerator();
        }

        public void Add(Rectangle item) => rectangles.Add(item);

        public void Clear() => rectangles.Clear();

        public bool Contains(Rectangle item) => rectangles.Contains(item);

        public void CopyTo(Rectangle[] array, int arrayIndex) => rectangles.CopyTo(array, arrayIndex);

        public bool Remove(Rectangle item) => rectangles.Remove(item);

        public int Count => rectangles.Count();
        public bool IsReadOnly => false;
        public int IndexOf(Rectangle item) => rectangles.IndexOf(item);

        public void Insert(int index, Rectangle item) => rectangles.Insert(index, item);

        public void RemoveAt(int index) => rectangles.RemoveAt(index);

        public Rectangle this[int index]
        {
            get { return rectangles[index]; }
            set { rectangles[index] = value; }
        }
    }
}
