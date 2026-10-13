using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using NuciText.Censorship.English;

namespace NuciText.Censorship.UnitTests;

/// <summary>
/// Test implementation of ITextCensor for unit testing the interface contract.
/// This implementation provides basic censorship logic for testing purposes.
/// </summary>
public sealed class TestTextCensor : ITextCensor
{
    private readonly HashSet<string> _badWords;
    private readonly string _replacement;

    public TestTextCensor(IEnumerable<string>? badWords = null, string replacement = "****")
    {
        _badWords = new HashSet<string>(badWords ?? DefaultBadWords, StringComparer.OrdinalIgnoreCase);
        _replacement = replacement;
    }

    private static readonly string[] DefaultBadWords =
    [
        "badword",
        "offensive",
        "profanity",
        "curse",
        "swear"
    ];

    public string Censor(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        if (string.IsNullOrWhiteSpace(text))
        {
            return text;
        }

        var result = text;

        // Replace bad words
        foreach (var badWord in _badWords)
        {
            var pattern = $@"\b{Regex.Escape(badWord)}\b";
            result = Regex.Replace(result, pattern, _replacement, RegexOptions.IgnoreCase);
        }

        // Replace URLs
        var urlPattern = @"https?://[^\s]+";
        result = Regex.Replace(result, urlPattern, _replacement, RegexOptions.IgnoreCase);

        return result;
    }
}