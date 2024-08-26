using Xunit;

namespace DeveloperChallengeApp.UnitTests
{
    /// <summary>
    /// This is just a small series of unit tests used to help me ensure that all aspects of the functionality were being accounted for and met.
    /// </summary>
    public class WordFinderTests
    {
        WordFinder testInstance1 = new WordFinder(new List<string>() { "wordfinderuniTTesT", "wordfinderunittest", "wordFinderunittest", "wordfinderunittest" });

        [Fact]
        public void ShouldFindCorrectWords()
        {
            // Arrange
            WordFinder wordFinder = testInstance1;

            // Act
            List<string> result = new List<string>() { "test", "find", "word", "fff", "oooo", "tttt" };
            List<string> output = wordFinder.Find(new List<string> { "xxxx", "test", "find", "word", "fff", "OOOO", "tttt" }).ToList();
            // Assert all of the results should be in output and the count of the lists should be equal.
            Assert.True(result.All(output.Contains) && result.Count == output.Count);
        }

        [Fact]
        public void ShouldReturnEmptyListIfNoWordsFound()
        {
            // Arrange
            WordFinder wordFinder = new WordFinder(new List<string>() { "test", "test", "test" });

            // Act
            List<string> output = wordFinder.Find(new List<string> { "shouldnotfind", "x" }).ToList();
            // Assert
            Assert.False(output.Any());
        }

        [Fact]
        public void ShouldOnlyReturnDistinctWords()
        {
            // Arrange
            WordFinder wordFinder = testInstance1;

            // Act
            List<string> result = new List<string>() { "test", "ttt", "dd" };
            List<string> output = wordFinder.Find(new List<string> { "test", "test", "Test", "ttt", "dd" }).ToList();
            // Assert
            Assert.True(result.All(output.Contains) && result.Count == output.Count);
        }

        [Fact]
        public void ShouldOnlyReturnTopTenWords()
        {
            // Arrange
            WordFinder wordFinder = testInstance1;

            // Act
            List<string> result = new List<string>() { "test", "ttt", "dd", "wo", "rd", "ww", "ff", "dddd", "finder", "tt" };
            List<string> output = wordFinder.Find(new List<string> { "test", "ttt", "dd", "wo", "rd", "ww", "ff", "dddd", "ffff", "finder", "tt", "uuuu" }).ToList();
            // Assert
            Assert.True(result.All(output.Contains) && result.Count == output.Count);
        }
    }
}
