using System.Drawing;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TagsCloudVisualization.ImageGeneration;
using TagsCloudVisualization.SupportModules;
using TagsCloudVisualization.Walker;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    internal class CloudLayouterShould
    {
        [SetUp]
        public void SetUp()
        {
            shapeContainer = new Mock<IIntersectionContainer>();
            pointWalker = new Mock<IWalker>();
            pointWalker.SetupSequence(x => x.GetNextPoint())
                .Returns(new Point(2, 2))
                .Returns(new Point(2, 4))
                .Returns(new Point(4, 2))
                .Returns(new Point(4, 4));
            cloudLayouter = new CloudLayouter(shapeContainer.Object, pointWalker.Object);
        }

        private CloudLayouter cloudLayouter;
        private Mock<IWalker> pointWalker;
        private Mock<IIntersectionContainer> shapeContainer;

        [Test]
        public void PutFirstRectangleOnFirstPoint()
        {
            var rect = cloudLayouter.PutNextRectangle(new Size(2, 4)).GetValueOrThrow();
            rect.ShouldBeEquivalentTo(new Rectangle(1, 0, 2, 4));
        }

        [Test]
        public void SkipPointOnIntersection()
        {
            shapeContainer
                .Setup(x => x.IntersectsWith(It.Is<Rectangle>(r => r.IntersectsWith(new Rectangle(1, 0, 2, 4)))))
                .Returns(true);

            var rect = cloudLayouter.PutNextRectangle(new Size(2, 2)).GetValueOrThrow();

            rect.ShouldBeEquivalentTo(new Rectangle(3, 1, 2, 2));
            pointWalker.Verify(x => x.Reset(), Times.Exactly(1));
            pointWalker.Verify(x => x.GetNextPoint(), Times.Exactly(3));
        }
    }
}
