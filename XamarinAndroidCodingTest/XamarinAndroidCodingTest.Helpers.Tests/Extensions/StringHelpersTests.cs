using NUnit.Framework;
using XamarinAndroidCodingTest.Helpers.Extensions;

namespace XamarinAndroidCodingTest.Helpers.Tests.Extensions
{
    public class StringHelpersTests
    {
        [Test]
        public void WithingCharacterCount_LowerThanBound()
        {
            string testString = "testing";

            var result = testString.WithinCharacterCount(
                testString.Length + 1, 
                testString.Length + 2);

            Assert.IsFalse(result);
        }

        [Test]
        public void WithingCharacterCount_BetweenBound()
        {
            string testString = "testing";

            var result = testString.WithinCharacterCount(
                testString.Length,
                testString.Length + 1);

            Assert.IsTrue(result);
        }

        [Test]
        public void WithingCharacterCount_AboveBound()
        {
            string testString = "testing";

            var result = testString.WithinCharacterCount(
                testString.Length - 2,
                testString.Length - 1);

            Assert.IsFalse(result);
        }

        [Test]
        public void HasMixtureOfLettersAndNumericalDigitsOnly()
        {
            string testString = "testing";

            var result = testString.HasMixtureOfLettersAndNumericalDigitsOnly();

            Assert.IsFalse(result);

            testString = "testing1!";
            result = testString.HasMixtureOfLettersAndNumericalDigitsOnly();

            Assert.IsFalse(result);

            testString = "testing1";
            result = testString.HasMixtureOfLettersAndNumericalDigitsOnly();

            Assert.IsTrue(result);
        }

        [Test]
        public void HasRepeatingSequenceOfCharacters()
        {
            string testString = "aaaa";

            var result = testString.HasRepeatingSequenceOfCharacters();

            Assert.IsTrue(result);

            testString = "foofoo";
            result = testString.HasRepeatingSequenceOfCharacters();

            Assert.IsTrue(result);

            testString = "footestfoofootest";
            result = testString.HasRepeatingSequenceOfCharacters();

            Assert.IsTrue(result);

            testString = "footestfoo";
            result = testString.HasRepeatingSequenceOfCharacters();

            Assert.IsFalse(result);
        }

        [Test]
        public void GetFirstCharacterAsString()
        {
            string testString = string.Empty;

            var result = testString.GetFirstCharacterAsString();

            Assert.IsEmpty(result);

            testString = "hello world!";

            result = testString.GetFirstCharacterAsString();

            Assert.AreEqual("h", result);
        }
    }
}
