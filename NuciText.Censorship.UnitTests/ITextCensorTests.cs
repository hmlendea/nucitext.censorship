using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using NuciText.Censorship.English;

using Xunit;

namespace NuciText.Censorship.UnitTests;

/// <summary>
/// Unit tests for the ITextCensor interface contract.
/// Tests verify the contract behavior using a test implementation.
/// </summary>
public sealed class ITextCensorTests
{
    private readonly ITextCensor _censor;

    public ITextCensorTests()
    {
        _censor = new TestTextCensor();
    }

    [Fact]
    public void Censor_NullInput_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _censor.Censor(null!));
    }

    [Fact]
    public void Censor_EmptyString_ReturnsEmptyString()
    {
        // Act
        var result = _censor.Censor(string.Empty);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void Censor_WhitespaceOnly_ReturnsWhitespace()
    {
        // Act
        var result = _censor.Censor("   ");

        // Assert
        Assert.Equal("   ", result);
    }

    [Fact]
    public void Censor_NoBadWords_ReturnsOriginalText()
    {
        // Arrange
        var input = "This is a clean message with no bad words.";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal(input, result);
    }

    [Fact]
    public void Censor_SingleBadWord_ReplacesWithAsterisks()
    {
        // Arrange
        var input = "This contains a badword in it.";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("This contains a **** in it.", result);
    }

    [Fact]
    public void Censor_MultipleBadWords_ReplacesAll()
    {
        // Arrange
        var input = "This has badword and offensive content.";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("This has **** and **** content.", result);
    }

    [Fact]
    public void Censor_BadWordAtStart_ReplacesCorrectly()
    {
        // Arrange
        var input = "badword is at the start.";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("**** is at the start.", result);
    }

    [Fact]
    public void Censor_BadWordAtEnd_ReplacesCorrectly()
    {
        // Arrange
        var input = "This ends with badword";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("This ends with ****", result);
    }

    [Fact]
    public void Censor_BadWordCaseInsensitive_ReplacesCorrectly()
    {
        // Arrange
        var input = "This has BADWORD and BadWord and badword.";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("This has **** and **** and ****.", result);
    }

    [Fact]
    public void Censor_BadWordWithPunctuation_ReplacesCorrectly()
    {
        // Arrange
        var input = "This has badword! And badword? Also badword.";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("This has ****! And ****? Also ****.", result);
    }

    [Fact]
    public void Censor_URL_ReplacesWithAsterisks()
    {
        // Arrange
        var input = "Check out https://example.com for more info.";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("Check out **** for more info.", result);
    }

    [Fact]
    public void Censor_HTTP_URL_ReplacesWithAsterisks()
    {
        // Arrange
        var input = "Visit http://test.org now.";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("Visit **** now.", result);
    }

    [Fact]
    public void Censor_MultipleURLs_ReplacesAll()
    {
        // Arrange
        var input = "See https://a.com and http://b.org";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("See **** and ****", result);
    }

    [Fact]
    public void Censor_URLWithPath_ReplacesEntireURL()
    {
        // Arrange
        var input = "Go to https://example.com/path/to/page?query=value";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("Go to ****", result);
    }

    [Fact]
    public void Censor_BadWordAndURL_ReplacesBoth()
    {
        // Arrange
        var input = "This badword has https://example.com in it.";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("This **** has **** in it.", result);
    }

    [Fact]
    public void Censor_UnicodeText_HandlesCorrectly()
    {
        // Arrange
        var input = "Café has badword and naïve.";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("Café has **** and naïve.", result);
    }

    [Fact]
    public void Censor_EmojiText_HandlesCorrectly()
    {
        // Arrange
        var input = "Hello 😀 badword 😀";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("Hello 😀 **** 😀", result);
    }

    [Fact]
    public void Censor_LongText_ProcessesCorrectly()
    {
        // Arrange
        var input = string.Join(" ", Enumerable.Repeat("word", 1000)) + " badword " + string.Join(" ", Enumerable.Repeat("word", 1000));

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Contains("****", result);
        Assert.DoesNotContain("badword", result);
    }

    [Fact]
    public void Censor_RepeatedBadWords_ReplacesAllOccurrences()
    {
        // Arrange
        var input = "badword badword badword";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("**** **** ****", result);
    }

    [Fact]
    public void Censor_BadWordAsSubstring_NotReplaced()
    {
        // Arrange
        var input = "This is notbadword but badword is here.";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("This is notbadword but **** is here.", result);
    }

    [Fact]
    public void Censor_CustomBadWords_UsesCustomList()
    {
        // Arrange
        var customCensor = new TestTextCensor(["custom", "words"]);
        var input = "This has custom and words in it.";

        // Act
        var result = customCensor.Censor(input);

        // Assert
        Assert.Equal("This has **** and **** in it.", result);
    }

    [Fact]
    public void Censor_CustomReplacement_UsesCustomReplacement()
    {
        // Arrange
        var customCensor = new TestTextCensor(replacement: "[CENSORED]");
        var input = "This has badword in it.";

        // Act
        var result = customCensor.Censor(input);

        // Assert
        Assert.Equal("This has [CENSORED] in it.", result);
    }

    [Fact]
    public void Censor_NewlinesAndTabs_PreservesFormatting()
    {
        // Arrange
        var input = "Line 1\nLine 2\tbadword\nLine 3";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("Line 1\nLine 2\t****\nLine 3", result);
    }

    [Fact]
    public void Censor_NumbersAndSpecialChars_PreservesThem()
    {
        // Arrange
        var input = "Price: $100.50, 123 badword 456!";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("Price: $100.50, 123 **** 456!", result);
    }

    [Fact]
    public void Censor_EmailAddresses_NotReplaced()
    {
        // Arrange
        var input = "Contact me at user@example.com";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("Contact me at user@example.com", result);
    }

    [Fact]
    public void Censor_IPAddresses_NotReplaced()
    {
        // Arrange
        var input = "Server at 192.168.1.1";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("Server at 192.168.1.1", result);
    }

    [Fact]
    public void Censor_MixedCaseURL_ReplacesCorrectly()
    {
        // Arrange
        var input = "Visit HTTPS://Example.COM";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("Visit ****", result);
    }

    [Fact]
    public void Censor_URLWithPort_ReplacesCorrectly()
    {
        // Arrange
        var input = "Connect to http://localhost:8080";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("Connect to ****", result);
    }

    [Fact]
    public void Censor_Contract_ReturnsString()
    {
        // Arrange
        var input = "test";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.IsType<string>(result);
    }

    [Fact]
    public void Censor_Contract_NeverReturnsNull()
    {
        // Arrange
        var input = "test";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void Censor_URLWithAuth_ReplacesCorrectly()
    {
        // Arrange
        var input = "Visit http://user:pass@example.com";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("Visit ****", result);
    }

    [Fact]
    public void Censor_URLWithFragment_ReplacesCorrectly()
    {
        // Arrange
        var input = "See https://example.com/page#section";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("See ****", result);
    }

    [Fact]
    public void Censor_URLWithQueryAndFragment_ReplacesCorrectly()
    {
        // Arrange
        var input = "Go to https://example.com/path?query=value#fragment";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("Go to ****", result);
    }

    [Fact]
    public void Censor_IPv6URL_ReplacesCorrectly()
    {
        // Arrange
        var input = "Connect to http://[::1]:8080";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("Connect to ****", result);
    }

    [Fact]
    public void Censor_IDNDomain_ReplacesCorrectly()
    {
        // Arrange
        var input = "Visit https://münchen.de";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("Visit ****", result);
    }

    [Fact]
    public void Censor_BadWordWithRegexMetacharacters_HandledCorrectly()
    {
        // Arrange
        var customCensor = new TestTextCensor(["test.word", "badword"]);
        var input = "This has test.word and badword in it.";

        // Act
        var result = customCensor.Censor(input);

        // Assert
        Assert.Equal("This has **** and **** in it.", result);
    }

    [Fact]
    public void Censor_OverlappingBadWords_ReplacesAll()
    {
        // Arrange
        var customCensor = new TestTextCensor(["badword", "word"]);
        var input = "This has badword and word in it.";

        // Act
        var result = customCensor.Censor(input);

        // Assert
        Assert.Equal("This has **** and **** in it.", result);
    }

    [Fact]
    public void Censor_VeryLongURL_ProcessesCorrectly()
    {
        // Arrange
        var longPath = string.Join("/", Enumerable.Repeat("path", 100));
        var input = $"Visit https://example.com/{longPath}";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("Visit ****", result);
    }

    [Fact]
    public void Censor_MultipleSpaces_PreservesSpacing()
    {
        // Arrange
        var input = "This  has   badword    here";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("This  has   ****    here", result);
    }

    [Fact]
    public void Censor_TabAndNewlineInURL_StopsAtWhitespace()
    {
        // Arrange
        var input = "Visit https://example.com/path\nNext line";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("Visit ****\nNext line", result);
    }

    [Fact]
    public void Censor_CaseInsensitiveURLScheme_ReplacesCorrectly()
    {
        // Arrange
        var input = "Visit HTTP://example.com and HttPs://test.org";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("Visit **** and ****", result);
    }

    [Fact]
    public void Censor_EmptyBadWordsList_OnlyReplacesURLs()
    {
        // Arrange
        var customCensor = new TestTextCensor([]);
        var input = "This has badword and https://example.com";

        // Act
        var result = customCensor.Censor(input);

        // Assert
        Assert.Equal("This has badword and ****", result);
    }

    [Fact]
    public void Censor_NullBadWordsList_UsesDefaults()
    {
        // Arrange
        var customCensor = new TestTextCensor(null);
        var input = "This has badword in it.";

        // Act
        var result = customCensor.Censor(input);

        // Assert
        Assert.Equal("This has **** in it.", result);
    }

    [Fact]
    public void Censor_ReplacementWithSpecialChars_WorksCorrectly()
    {
        // Arrange
        var customCensor = new TestTextCensor(replacement: "[***]");
        var input = "This has badword.";

        // Act
        var result = customCensor.Censor(input);

        // Assert
        Assert.Equal("This has [***].", result);
    }

    [Fact]
    public void Censor_ReplacementEmptyString_RemovesBadWords()
    {
        // Arrange
        var customCensor = new TestTextCensor(replacement: "");
        var input = "This has badword here.";

        // Act
        var result = customCensor.Censor(input);

        // Assert
        Assert.Equal("This has  here.", result);
    }

    [Fact]
    public void Censor_URLAtStartOfText_ReplacesCorrectly()
    {
        // Arrange
        var input = "https://example.com is the site";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("**** is the site", result);
    }

    [Fact]
    public void Censor_URLAtEndOfText_ReplacesCorrectly()
    {
        // Arrange
        var input = "Visit https://example.com";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("Visit ****", result);
    }

    [Fact]
    public void Censor_OnlyBadWords_ReturnsOnlyReplacements()
    {
        // Arrange
        var input = "badword offensive profanity";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("**** **** ****", result);
    }

    [Fact]
    public void Censor_OnlyURLs_ReturnsOnlyReplacements()
    {
        // Arrange
        var input = "https://a.com http://b.org";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("**** ****", result);
    }

    [Fact]
    public void Censor_MixedScriptText_HandlesCorrectly()
    {
        // Arrange
        var input = "English badword 中文 русский";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("English **** 中文 русский", result);
    }

    [Fact]
    public void Censor_RightToLeftText_HandlesCorrectly()
    {
        // Arrange
        var input = "English badword עברית";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("English **** עברית", result);
    }

    [Fact]
    public void Censor_ZeroWidthCharacters_Preserved()
    {
        // Arrange
        var input = "bad\u200Bword badword"; // zero-width space in first

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("bad\u200Bword ****", result);
    }

    [Fact]
    public void Censor_SurrogatePairs_HandledCorrectly()
    {
        // Arrange
        var input = "😀 badword 😀"; // emoji are surrogate pairs in UTF-16

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.Equal("😀 **** 😀", result);
    }
}