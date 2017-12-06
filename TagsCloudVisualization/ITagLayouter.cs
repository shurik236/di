using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ITagLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
        IIntersectionContainer GetTagCloud();
    }
}