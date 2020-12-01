using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System.Text.Unicode
{
    public static class UnicodeExtensions
    {
        // https://en.wikipedia.org/wiki/List_of_Unicode_characters
        // ⌬

        /// <summary>
        /// IsGraphic reports whether the unicode charater is interpreted as a Graphic. 
        /// Such characters include letters, marks, numbers, punctuation, symbols, and spaces,
        /// from categories L, M, N, P, S, Zs.
        /// </summary>
        public static bool IsGraphic(this char ch)
        {
            var cat = CharUnicodeInfo.GetUnicodeCategory(ch);
            return GraphicCategories.Contains(cat);
        }
        private static UnicodeCategory[] GraphicCategories = new UnicodeCategory[]
        {
            UnicodeCategory.UppercaseLetter,
            UnicodeCategory.LowercaseLetter,
            UnicodeCategory.TitlecaseLetter,
            UnicodeCategory.ModifierLetter,
            UnicodeCategory.OtherLetter,
            UnicodeCategory.NonSpacingMark,
            UnicodeCategory.SpacingCombiningMark,
            UnicodeCategory.EnclosingMark,
            UnicodeCategory.DecimalDigitNumber,
            UnicodeCategory.LetterNumber,
            UnicodeCategory.OtherNumber,
            UnicodeCategory.SpaceSeparator,
            //UnicodeCategory.LineSeparator,
            //UnicodeCategory.ParagraphSeparator,
            //UnicodeCategory.Control,
            //UnicodeCategory.Format,
            //UnicodeCategory.Surrogate,
            //UnicodeCategory.PrivateUse,
            UnicodeCategory.ConnectorPunctuation,
            UnicodeCategory.DashPunctuation,
            UnicodeCategory.OpenPunctuation,
            UnicodeCategory.ClosePunctuation,
            UnicodeCategory.InitialQuotePunctuation,
            UnicodeCategory.FinalQuotePunctuation,
            UnicodeCategory.OtherPunctuation,
            UnicodeCategory.MathSymbol,
            UnicodeCategory.CurrencySymbol,
            UnicodeCategory.ModifierSymbol,
            UnicodeCategory.OtherSymbol,
            //UnicodeCategory.OtherNotAssigned
        };

        /// <summary>
        /// IsPrintable reports whether the rune is defined as printable. Such 
        /// characters include letters, marks, numbers, punctuation, symbols, and 
        /// the ASCII space character, from categories L, M, N, P, S and the ASCII 
        /// space character. This categorization is the same as IsGraphic except 
        /// that the only spacing character is ASCII space, U+0020.
        /// </summary>
        public static bool IsPrintable(this char ch)
        {
            var cat = CharUnicodeInfo.GetUnicodeCategory(ch);
            return PrintableCategories.Contains(cat);
        }
        private static UnicodeCategory[] PrintableCategories = new UnicodeCategory[]
        {
            UnicodeCategory.UppercaseLetter,
            UnicodeCategory.LowercaseLetter,
            UnicodeCategory.TitlecaseLetter,
            UnicodeCategory.ModifierLetter,
            UnicodeCategory.OtherLetter,
            UnicodeCategory.NonSpacingMark,
            UnicodeCategory.SpacingCombiningMark,
            UnicodeCategory.EnclosingMark,
            UnicodeCategory.DecimalDigitNumber,
            UnicodeCategory.LetterNumber,
            UnicodeCategory.OtherNumber,
            //UnicodeCategory.SpaceSeparator,
            //UnicodeCategory.LineSeparator,
            //UnicodeCategory.ParagraphSeparator,
            //UnicodeCategory.Control,
            //UnicodeCategory.Format,
            //UnicodeCategory.Surrogate,
            //UnicodeCategory.PrivateUse,
            UnicodeCategory.ConnectorPunctuation,
            UnicodeCategory.DashPunctuation,
            UnicodeCategory.OpenPunctuation,
            UnicodeCategory.ClosePunctuation,
            UnicodeCategory.InitialQuotePunctuation,
            UnicodeCategory.FinalQuotePunctuation,
            UnicodeCategory.OtherPunctuation,
            UnicodeCategory.MathSymbol,
            UnicodeCategory.CurrencySymbol,
            UnicodeCategory.ModifierSymbol,
            UnicodeCategory.OtherSymbol
            //UnicodeCategory.OtherNotAssigned
        };

        private static bool IsBreakLine(this char ch)
        {
            return (ch == '\r') || (ch == '\n');
        }


        private static string ToTextCore(this string s, bool allowBreakLines)
        {
            var sb = new StringBuilder(s);
            for (int i = 0; i < sb.Length; i++)
            {
                if (sb[i].IsBreakLine() && !allowBreakLines)
                {
                    sb[i] = '↵';
                    if ((i + 1 < sb.Length) && (sb[i] != sb[i + 1]) && sb[i].IsBreakLine())
                    {
                        // "\r\n" or "\n\r"
                        sb[i + 1] = char.MinValue;
                        i++;
                    }
                }
                else if (sb[i] == '\t')
                {
                    sb[i] = '⇥';
                }
                else if (sb[i] == char.MinValue)
                {
                    sb[i] = '␀';
                }
                if (char.IsControl(sb[i]))
                {
                    sb[i] = '�';
                }
                else
                {
                    var cat = CharUnicodeInfo.GetUnicodeCategory(sb[i]);
                    switch (cat)
                    {
                        case UnicodeCategory.Format:
                            sb[i] = '⌇'; 
                            break;
                        case UnicodeCategory.LineSeparator:
                            if (!allowBreakLines)
                                sb[i] = '↵';
                            break;
                        case UnicodeCategory.ParagraphSeparator:
                            if (!allowBreakLines)
                                sb[i] = '↩';
                            break;
                        case UnicodeCategory.SpaceSeparator:
                            sb[i] = ' ';
                            break;
                        case UnicodeCategory.NonSpacingMark:
                        case UnicodeCategory.SpacingCombiningMark:
                            sb[i] = '░';
                            break;
                        case UnicodeCategory.ModifierLetter:
                        case UnicodeCategory.ModifierSymbol:
                            sb[i] = '↝';
                            break;
                        case UnicodeCategory.OtherNotAssigned:
                            sb[i] = '�';
                            break;
                        case UnicodeCategory.PrivateUse:
                            sb[i] = '⁉';
                            break;
                        case UnicodeCategory.Surrogate:
                            sb[i] = '⁑';
                            break;
                        default:
                            // Do nothing
                            break;
                    }
                }
            }
            return sb.ToString();
        }

        public static string ToText(this string s)
        {
            return s.ToTextCore(true);
        }

        public static string ToTextLine(this string s)
        {
            return s.ToTextCore(false);
        }
    }
}
