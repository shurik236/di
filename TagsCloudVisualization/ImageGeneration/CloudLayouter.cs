using System;
using System.Drawing;
using TagsCloudVisualization.SupportModules;
using TagsCloudVisualization.Walker;

namespace TagsCloudVisualization.ImageGeneration
{
    public class CloudLayouter : ITagLayouter
    {
        public IIntersectionContainer Cloud { get; }
        private readonly IWalker pointWalker;

        public CloudLayouter(IIntersectionContainer cloud, IWalker pointWalker)
        {
            Cloud = cloud;
            this.pointWalker = pointWalker;
        }
       
        public Result<Rectangle> PutNextRectangle(Size rectangleSize)
        {
            Rectangle newRect;
            do
            {
                var nextPoint = pointWalker.GetNextPoint().OnFail(Console.WriteLine);

                newRect = new Rectangle(nextPoint.Value, rectangleSize);
                newRect.Offset((newRect.Left - newRect.Right) / 2, (newRect.Top - newRect.Bottom) / 2);
            } while (Cloud.IntersectsWith(newRect));

            Cloud.Add(newRect);
            pointWalker.Reset();
            return Result.Ok(newRect);
        }
    }
}
