using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.SupportModules;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    internal class CloudContainerShould
    {
        [Test]
        public void BeEmptyOnInitialization()
        {
            new Cloud().Should().BeEmpty();
        }

        [TestCase(1, TestName = "Add single rectangle")]
        [TestCase(50, TestName = "Add fifty rectangles")]
        public void AddSeveralElements(int count)
        {
            var cloud = new Cloud();
            for (var i = 1; i<=count; i++)
                cloud.Add(new Rectangle(i, i, i, i));
            cloud.Count.Should().Be(count);
        }

        [TestCase(70, 70, 80, 20, true,
            TestName = "Intersect with (70, 70, 80, 20), having (50, 50, 100, 50) and (150, 100, 100, 50)")]
        [TestCase(200, 200, 100, 50, false,
            TestName = "Not intersect with (200, 200, 100, 50), having (50, 50, 100, 50) and (150, 100, 100, 50)")]
        public void CheckIntersection(int x, int y, int width, int height, bool result)
        {
            var cloud = new Cloud {new Rectangle(50, 50, 100, 50), new Rectangle(150, 100, 100, 50)};
            cloud.IntersectsWith(new Rectangle(x, y, width, height)).Should().Be(result);
        }
    }
}
