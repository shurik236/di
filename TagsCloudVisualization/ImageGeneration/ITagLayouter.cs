using System.Drawing;

namespace TagsCloudVisualization.ImageGeneration
{
    public interface ITagLayouter
    {
        Result<Rectangle> PutNextRectangle(Size rectangleSize);
    }
}