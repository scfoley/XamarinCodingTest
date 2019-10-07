using System.Linq;
using System.Text.RegularExpressions;

namespace XamarinAndroidCodingTest.Helpers.Extensions
{
    public static class StringHelpers
    {
        /// <summary>
        /// Used to see if a string value is withing a lower and upper bound.
        /// The lower and upperbound are inclusive.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="lowerBound"></param>
        /// <param name="upperBound"></param>
        /// <returns></returns>
        public static bool WithinCharacterCount(this string value, int lowerBound, int upperBound)
        {
            return value?.Length >= lowerBound && value?.Length <= upperBound;
        }

        /// <summary>
        /// Used to make sure that a value only contains letters and numerical digits.
        /// Additionally, checks that a value contians at least one number, and one digit.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool HasMixtureOfLettersAndNumericalDigitsOnly(this string value)
        {
            return Regex.IsMatch(value, @"^[a-zA-Z0-9]+$")
                && value.Any(c => char.IsLetter(c))
                && value.Any(c => char.IsDigit(c));
        }

        // Assumption, a sequence of characters is 2 or more characters, so "aa" is not a match, but "aaaa" is
        /// <summary>
        /// Used to make sure a value does not contain a repeating sequesnce of characters.
        /// In this situation, a sequence is interpretted as 2+ characters.
        /// The following would result in true result: "aaaa", "foofootest", "cbambamc"
        /// The following would result in false result: "aa", "footestfoo", "bamcbam"
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
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

                /*
                 * Until the frames are larger than half the string size
                 * continue expanding and checking for matchs
                 */
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

        /// <summary>
        /// Used to get the first character of a string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
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
