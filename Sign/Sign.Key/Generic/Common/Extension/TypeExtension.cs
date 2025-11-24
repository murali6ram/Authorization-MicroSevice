using System.Globalization;
using System.Text.RegularExpressions;

namespace Common.Extension;

public static partial class TypeExtension
{
    [GeneratedRegex(@"\s+")]
    private static partial Regex WhiteSpace();
    [GeneratedRegex("[^a-zA-Z0-9]+")]
    private static partial Regex SpecialCharacters();

    public static string RemoveSpecialCharactersAndWhitespace(this string value)
        => WhiteSpace().Replace(SpecialCharacters().Replace(value, ""), "");

    public static string ToTitleCase(this string value)
        => CultureInfo.InvariantCulture.TextInfo.ToTitleCase(value.ToLower());
}
