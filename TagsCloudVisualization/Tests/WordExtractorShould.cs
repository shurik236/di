using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.TagGeneration;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    internal class WordExtractorShould
    {
        private static readonly WordExtractor WordExtractor = new WordExtractor();

        [Test]
        public static void CastToLowerCase()
        {
            var result = WordExtractor.ExtractWords("AbeRRaTion tEXt").Value.ToList();
            result.Should().HaveCount(2);
            result.Should().Contain(new[] {"aberration", "text"});
        }

        [Test]
        public static void RemovePunctuation()
        {
            var result = WordExtractor.ExtractWords("Fruits: banana, lemon, mango. Vegetables - carrot; cabbage; potato. One + one = one.").Value.ToList();
            result.Should().NotContain(c => c.IndexOfAny(":,.-;+=".ToCharArray()) != -1);
        }

        [Test]
        public static void RemoveIllegalWords()
        {
            var result = WordExtractor.ExtractWords(
                "Sit down awhile And let us once again assail your ears That are so fortified against our story What we have two nights seen").Value.ToList();
            result.Should().NotContain("us", "your", "are", "so", "our", "that", "we");
        }

        [Test]
        public static void LemmatizeSimpleWords()
        {
            var result = WordExtractor.ExtractWords("test tested testing tests").Value;
            result.Should().OnlyContain(c => c.Equals("test"));
        }
    }
}
