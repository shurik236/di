using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ITagLayouter
    {
        Result<Rectangle> PutNextRectangle(Size rectangleSize);
    }
}