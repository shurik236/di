using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.TagGeneration;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    internal class FrequencyCounterShould
    {
        private static FrequencyCounter _freqCount;
        private static string[] _wordList;

        [SetUp]
        public static void SetUp()
        {
            _freqCount = new FrequencyCounter();
        }

        [Test]
        public static void ReturnEmptyOnEmpty()
        {
            _wordList = new string[] {};
            _freqCount.AssignWeights(_wordList).Should().BeEmpty();
        }

        [Test]
        public static void CountOnceWhenNoRepeatingWords()
        {
            _wordList = new[] { "banana", "mango", "kiwi", "apple", "lemon" };
            var result = _freqCount.AssignWeights(_wordList).ToList();
            result.Should().HaveCount(5);
            result.Should().Contain(new[]
            {
                Tuple.Create("banana", 1),
                Tuple.Create("mango", 1),
                Tuple.Create("lemon", 1),
                Tuple.Create("kiwi", 1),
                Tuple.Create("apple", 1)
            });
        }

        [Test]
        public static void CountSeveralTimesWhenRepeatingWords()
        {
            _wordList = new[]
            {
                "ninety", "nine", "bottles", "of", "beer", "on", "the", "wall", "ninty", "nine", "bottles", "of",
                "beer"
            };
            var result = _freqCount.AssignWeights(_wordList).ToList();
            result.Should().HaveCount(9);
            result.Should().Contain(new[]
            {
                Tuple.Create("ninety", 1),
                Tuple.Create("ninty", 1),
                Tuple.Create("nine", 2),
                Tuple.Create("bottles", 2),
                Tuple.Create("of", 2),
                Tuple.Create("beer", 2),
                Tuple.Create("on", 1),
                Tuple.Create("the", 1),
                Tuple.Create("wall", 1)
            });
        }
    }
}
