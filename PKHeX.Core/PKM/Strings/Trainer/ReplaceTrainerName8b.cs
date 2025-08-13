using System;
using static PKHeX.Core.LanguageID;

namespace PKHeX.Core;

/// <summary>
/// Logic for replacing the name of a Pokémon in <see cref="EntityContext.Gen8b"/>.
/// </summary>
public static class ReplaceTrainerName8b
{
    /// <summary>
    /// Checks if the original name is a trigger for replacement, and if the current name is a valid replacement.
    /// </summary>
    /// <param name="original">Original name to check for replacement trigger.</param>
    /// <param name="current">Current name to check for valid replacement.</param>
    /// <param name="language">Entity language.</param>
    /// <param name="origin">Entity game version.</param>
    public static bool IsTriggerAndReplace(ReadOnlySpan<char> original, ReadOnlySpan<char> current, LanguageID language, GameVersion origin)
    {
        if (!IsTrigger(original, language))
            return false;
        return IsReplace(current, language, origin);
    }

    /// <summary>
    /// Determines whether the specified name should be replaced based on language-specific rules.
    /// </summary>
    /// <remarks>This method checks for undefined characters in the name and applies additional rules for
    /// certain languages. For example, names longer than five characters are flagged for replacement in Asian languages
    /// such as Japanese, Korean, Simplified Chinese, and Traditional Chinese.</remarks>
    /// <param name="name">The name to evaluate, represented as a read-only span of characters.</param>
    /// <param name="language">The language identifier used to apply language-specific rules.</param>
    /// <returns><see langword="true"/> if the name contains undefined characters or violates language-specific constraints;
    /// otherwise, <see langword="false"/>.</returns>
    public static bool IsTrigger(ReadOnlySpan<char> name, LanguageID language)
    {
        var maxLength = language is Japanese or Korean or ChineseS or ChineseT ? Legal.MaxLengthTrainerAsian : Legal.MaxLengthTrainerWestern;
        if (name.Length > maxLength)
            return true; // Too long for Asian languages

        // Skip CheckNgWords: Numbers, whitespace, whitewords, nn::ngc -- implicitly flagged by our WordFilter. No legitimate events trigger this.

        return false; // OK
    }

    /// <summary>
    /// Checks if the provided name is one of the valid replacement names for the specified language and game version.
    /// </summary>
    /// <param name="name">Current name to check for valid replacement.</param>
    /// <param name="language">Entity language.</param>
    /// <param name="origin">Entity game version.</param>
    public static bool IsReplace(ReadOnlySpan<char> name, LanguageID language, GameVersion origin)
    {
        // Foreign games (not from BD/SP) will use Diamond.
        var expect = origin is GameVersion.SP ? GetNamePearl(language) : GetNameDiamond(language);
        return name.SequenceEqual(expect);
    }

    /// <summary>
    /// Gets the replacement name for <see cref="GameVersion.BD"/> in the given language.
    /// </summary>
    public static string GetNameDiamond(LanguageID language) => language switch
    {
        Japanese => "ダイヤ.",
        French => "Diamant.",
        Italian => "Diaman.",
        German => "Diamant.",
        Spanish => "Diamant.",
        Korean => "다이아몬드.",
        ChineseS => "戴亚.",
        ChineseT => "戴亞.",

        _ => "Diamond.",
    };

    /// <summary>
    /// Gets the replacement name for <see cref="GameVersion.SP"/> in the given language.
    /// </summary>
    public static string GetNamePearl(LanguageID language) => language switch
    {
        Japanese => "パール.",
        French => "Perlo.",
        Italian => "Perl.",
        German => "Perl.",
        Spanish => "Perla.",
        Korean => "펄.",
        ChineseS => "帕尔.",
        ChineseT => "帕爾.",

        _ => "Pearl.",
    };
}
