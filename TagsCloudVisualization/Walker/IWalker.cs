using System.Drawing;

namespace TagsCloudVisualization.Walker
{
    public interface IWalker
    {
        Result<Point> GetNextPoint();
        void Reset();
    }
}
