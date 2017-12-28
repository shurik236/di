using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TagsCloudVisualization.TagGeneration;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    internal class TagGeneratorShould
    {
        private Mock<IWeightAssigner> weightAssigner;
        private TagGenerator generator;
        private readonly TagGeneratorConfig configuration = new TagGeneratorConfig();

        private static readonly List<string> TestWords = new List<string>{"apple", "banana", "lemon", "orange", "raspberry"};
        private static readonly List<int> TestWeights = new List<int> {1, 20, 256, 1024, 32767};

        [SetUp]
        public void SetUp()
        {
            weightAssigner = new Mock<IWeightAssigner>();
            weightAssigner
                .Setup(x => x.AssignWeights(TestWords))
                .Returns(TestWords.Zip(TestWeights, Tuple.Create));
            generator = new TagGenerator(weightAssigner.Object);
        }

        [Test]
        public void SetDifferentColors()
        {
            var tags = generator.GenerateTags(TestWords, configuration).GetValueOrThrow().ToList();
            tags.Select(x => x.Brush).Should().OnlyHaveUniqueItems();
        }

        [Test]
        public void SetDifferentSizes()
        {
            var tags = generator.GenerateTags(TestWords, configuration).GetValueOrThrow().ToList();
            tags.Select(x => x.Font.Size).Should().OnlyHaveUniqueItems();
        }

        [Test]
        public void NotAffectContent()
        {
            var tags = generator.GenerateTags(TestWords, configuration).GetValueOrThrow().ToList();
            tags.Select(t => t.Value).Should().Contain(TestWords);
            TestWords.Should().Contain(tags.Select(t => t.Value));
        }
    }
}
