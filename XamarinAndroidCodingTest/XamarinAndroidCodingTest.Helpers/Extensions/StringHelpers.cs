using System.Linq;
using System.Text.RegularExpressions;

namespace XamarinAndroidCodingTest.Helpers.Extensions
{
    public static class StringHelpers
    {
        public static bool WithinCharacterCount(this string value, int lowerBound, int upperBound)
        {
            return value?.Length >= lowerBound && value?.Length <= upperBound;
        }

        public static bool HasMixtureOfLettersAndNumericalDigitsOnly(this string value)
        {
            return Regex.IsMatch(value, @"^[a-zA-Z0-9]+$")
                && value.Any(c => char.IsLetter(c))
                && value.Any(c => char.IsDigit(c));
        }

        // Assumption, a sequence of characters is 2 or more characters, so "aa" is not a match, but "aaaa" is
        public static bool HasRepeatingSequenceOfCharacters(this string value)
        {
            if(value == null)
            {
                return false;
            }

            // i equals the character counts to include in shifting comparison "frames"
            for(int i = 2; i <= value.Length / 2; i++)
            {
                int j = 0; // starting index
                int k = 0 + i; // divider index
                int l = k + i; // ending index

                while (l <= value.Length)
                {
                    if(value.Substring(j, i) == value.Substring(k, i))
                    {
                        return true;
                    }

                    j++;
                    k++;
                    l++;
                }
            }

            return false;
        }

        public static string GetFirstCharacterAsString(this string value)
        {
            if(string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            return value[0].ToString();
        }
    }
}
