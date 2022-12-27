using ReelWords.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ReelWords.Extensions
{
    public static class Extensions
    {
        public static string RemoveDiacritics(this string @this)
        {
            string normalizedString = @this.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (char t in normalizedString)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(t);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(t);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string WriteLetters(this IEnumerable<Letter> letters)
        {
            var builder = new StringBuilder();

            foreach (var letter in letters)
            {
                builder.Append(letter.Character);
            }

            return builder.ToString();
        }
    }
}
