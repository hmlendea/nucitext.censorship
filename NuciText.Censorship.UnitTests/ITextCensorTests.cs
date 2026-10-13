using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using NuciText.Censorship.English;

using NUnit.Framework;

namespace NuciText.Censorship.UnitTests;

/// <summary>
/// Unit tests for the ITextCensor interface contract.
/// Tests verify the contract behavior using a test implementation.
/// </summary>
[TestFixture]
public sealed class ITextCensorTests
{
    private ITextCensor _censor = null!;

    [SetUp]
    public void SetUp()
    {
        _censor = new TestTextCensor();
    }

    [Test]
    public void Censor_NullInput_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _censor.Censor(null!));
    }

    [Test]
    public void Censor_EmptyString_ReturnsEmptyString()
    {
        // Act
        var result = _censor.Censor(string.Empty);

        // Assert
        Assert.That(result, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Censor_WhitespaceOnly_ReturnsWhitespace()
    {
        // Act
        var result = _censor.Censor("   ");

        // Assert
        Assert.That(result, Is.EqualTo("   "));
    }

    [Test]
    public void Censor_NoBadWords_ReturnsOriginalText()
    {
        // Arrange
        var input = "This is a clean message with no bad words.";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo(input));
    }

    [Test]
    public void Censor_SingleBadWord_ReplacesWithAsterisks()
    {
        // Arrange
        var input = "This contains a badword in it.";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("This contains a **** in it."));
    }

    [Test]
    public void Censor_MultipleBadWords_ReplacesAll()
    {
        // Arrange
        var input = "This has badword and offensive content.";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("This has **** and **** content."));
    }

    [Test]
    public void Censor_BadWordAtStart_ReplacesCorrectly()
    {
        // Arrange
        var input = "badword is at the start.";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("**** is at the start."));
    }

    [Test]
    public void Censor_BadWordAtEnd_ReplacesCorrectly()
    {
        // Arrange
        var input = "This ends with badword";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("This ends with ****"));
    }

    [Test]
    public void Censor_BadWordCaseInsensitive_ReplacesCorrectly()
    {
        // Arrange
        var input = "This has BADWORD and BadWord and badword.";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("This has **** and **** and ****."));
    }

    [Test]
    public void Censor_BadWordWithPunctuation_ReplacesCorrectly()
    {
        // Arrange
        var input = "This has badword! And badword? Also badword.";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("This has ****! And ****? Also ****."));
    }

    [Test]
    public void Censor_URL_ReplacesWithAsterisks()
    {
        // Arrange
        var input = "Check out https://example.com for more info.";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("Check out **** for more info."));
    }

    [Test]
    public void Censor_HTTP_URL_ReplacesWithAsterisks()
    {
        // Arrange
        var input = "Visit http://test.org now.";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("Visit **** now."));
    }

    [Test]
    public void Censor_MultipleURLs_ReplacesAll()
    {
        // Arrange
        var input = "See https://a.com and http://b.org";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("See **** and ****"));
    }

    [Test]
    public void Censor_URLWithPath_ReplacesEntireURL()
    {
        // Arrange
        var input = "Go to https://example.com/path/to/page?query=value";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("Go to ****"));
    }

    [Test]
    public void Censor_BadWordAndURL_ReplacesBoth()
    {
        // Arrange
        var input = "This badword has https://example.com in it.";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("This **** has **** in it."));
    }

    [Test]
    public void Censor_UnicodeText_HandlesCorrectly()
    {
        // Arrange
        var input = "Café has badword and naïve.";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("Café has **** and naïve."));
    }

    [Test]
    public void Censor_EmojiText_HandlesCorrectly()
    {
        // Arrange
        var input = "Hello 😀 badword 😀";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("Hello 😀 **** 😀"));
    }

    [Test]
    public void Censor_LongText_ProcessesCorrectly()
    {
        // Arrange
        var input = string.Join(" ", Enumerable.Repeat("word", 1000)) + " badword " + string.Join(" ", Enumerable.Repeat("word", 1000));

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Does.Contain("****"));
        Assert.That(result, Does.Not.Contain("badword"));
    }

    [Test]
    public void Censor_RepeatedBadWords_ReplacesAllOccurrences()
    {
        // Arrange
        var input = "badword badword badword";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("**** **** ****"));
    }

    [Test]
    public void Censor_BadWordAsSubstring_NotReplaced()
    {
        // Arrange
        var input = "This is notbadword but badword is here.";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("This is notbadword but **** is here."));
    }

    [Test]
    public void Censor_CustomBadWords_UsesCustomList()
    {
        // Arrange
        var customCensor = new TestTextCensor(["custom", "words"]);
        var input = "This has custom and words in it.";

        // Act
        var result = customCensor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("This has **** and **** in it."));
    }

    [Test]
    public void Censor_CustomReplacement_UsesCustomReplacement()
    {
        // Arrange
        var customCensor = new TestTextCensor(replacement: "[CENSORED]");
        var input = "This has badword in it.";

        // Act
        var result = customCensor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("This has [CENSORED] in it."));
    }

    [Test]
    public void Censor_NewlinesAndTabs_PreservesFormatting()
    {
        // Arrange
        var input = "Line 1\nLine 2\tbadword\nLine 3";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("Line 1\nLine 2\t****\nLine 3"));
    }

    [Test]
    public void Censor_NumbersAndSpecialChars_PreservesThem()
    {
        // Arrange
        var input = "Price: $100.50, 123 badword 456!";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("Price: $100.50, 123 **** 456!"));
    }

    [Test]
    public void Censor_EmailAddresses_NotReplaced()
    {
        // Arrange
        var input = "Contact me at user@example.com";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("Contact me at user@example.com"));
    }

    [Test]
    public void Censor_IPAddresses_NotReplaced()
    {
        // Arrange
        var input = "Server at 192.168.1.1";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("Server at 192.168.1.1"));
    }

    [Test]
    public void Censor_MixedCaseURL_ReplacesCorrectly()
    {
        // Arrange
        var input = "Visit HTTPS://Example.COM";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("Visit ****"));
    }

    [Test]
    public void Censor_URLWithPort_ReplacesCorrectly()
    {
        // Arrange
        var input = "Connect to http://localhost:8080";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("Connect to ****"));
    }

    [Test]
    public void Censor_Contract_ReturnsString()
    {
        // Arrange
        var input = "test";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.InstanceOf<string>());
    }

    [Test]
    public void Censor_Contract_NeverReturnsNull()
    {
        // Arrange
        var input = "test";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void Censor_URLWithAuth_ReplacesCorrectly()
    {
        // Arrange
        var input = "Visit http://user:pass@example.com";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("Visit ****"));
    }

    [Test]
    public void Censor_URLWithFragment_ReplacesCorrectly()
    {
        // Arrange
        var input = "See https://example.com/page#section";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("See ****"));
    }

    [Test]
    public void Censor_URLWithQueryAndFragment_ReplacesCorrectly()
    {
        // Arrange
        var input = "Go to https://example.com/path?query=value#fragment";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("Go to ****"));
    }

    [Test]
    public void Censor_IPv6URL_ReplacesCorrectly()
    {
        // Arrange
        var input = "Connect to http://[::1]:8080";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("Connect to ****"));
    }

    [Test]
    public void Censor_IDNDomain_ReplacesCorrectly()
    {
        // Arrange
        var input = "Visit https://münchen.de";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("Visit ****"));
    }

    [Test]
    public void Censor_BadWordWithRegexMetacharacters_HandledCorrectly()
    {
        // Arrange
        var customCensor = new TestTextCensor(["test.word", "badword"]);
        var input = "This has test.word and badword in it.";

        // Act
        var result = customCensor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("This has **** and **** in it."));
    }

    [Test]
    public void Censor_OverlappingBadWords_ReplacesAll()
    {
        // Arrange
        var customCensor = new TestTextCensor(["badword", "word"]);
        var input = "This has badword and word in it.";

        // Act
        var result = customCensor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("This has **** and **** in it."));
    }

    [Test]
    public void Censor_VeryLongURL_ProcessesCorrectly()
    {
        // Arrange
        var longPath = string.Join("/", Enumerable.Repeat("path", 100));
        var input = $"Visit https://example.com/{longPath}";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("Visit ****"));
    }

    [Test]
    public void Censor_MultipleSpaces_PreservesSpacing()
    {
        // Arrange
        var input = "This  has   badword    here";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("This  has   ****    here"));
    }

    [Test]
    public void Censor_TabAndNewlineInURL_StopsAtWhitespace()
    {
        // Arrange
        var input = "Visit https://example.com/path\nNext line";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("Visit ****\nNext line"));
    }

    [Test]
    public void Censor_CaseInsensitiveURLScheme_ReplacesCorrectly()
    {
        // Arrange
        var input = "Visit HTTP://example.com and HttPs://test.org";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("Visit **** and ****"));
    }

    [Test]
    public void Censor_EmptyBadWordsList_OnlyReplacesURLs()
    {
        // Arrange
        var customCensor = new TestTextCensor([]);
        var input = "This has badword and https://example.com";

        // Act
        var result = customCensor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("This has badword and ****"));
    }

    [Test]
    public void Censor_NullBadWordsList_UsesDefaults()
    {
        // Arrange
        var customCensor = new TestTextCensor(null);
        var input = "This has badword in it.";

        // Act
        var result = customCensor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("This has **** in it."));
    }

    [Test]
    public void Censor_ReplacementWithSpecialChars_WorksCorrectly()
    {
        // Arrange
        var customCensor = new TestTextCensor(replacement: "[***]");
        var input = "This has badword.";

        // Act
        var result = customCensor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("This has [***]."));
    }

    [Test]
    public void Censor_ReplacementEmptyString_RemovesBadWords()
    {
        // Arrange
        var customCensor = new TestTextCensor(replacement: "");
        var input = "This has badword here.";

        // Act
        var result = customCensor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("This has  here."));
    }

    [Test]
    public void Censor_URLAtStartOfText_ReplacesCorrectly()
    {
        // Arrange
        var input = "https://example.com is the site";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("**** is the site"));
    }

    [Test]
    public void Censor_URLAtEndOfText_ReplacesCorrectly()
    {
        // Arrange
        var input = "Visit https://example.com";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("Visit ****"));
    }

    [Test]
    public void Censor_OnlyBadWords_ReturnsOnlyReplacements()
    {
        // Arrange
        var input = "badword offensive profanity";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("**** **** ****"));
    }

    [Test]
    public void Censor_OnlyURLs_ReturnsOnlyReplacements()
    {
        // Arrange
        var input = "https://a.com http://b.org";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("**** ****"));
    }

    [Test]
    public void Censor_MixedScriptText_HandlesCorrectly()
    {
        // Arrange
        var input = "English badword 中文 русский";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("English **** 中文 русский"));
    }

    [Test]
    public void Censor_RightToLeftText_HandlesCorrectly()
    {
        // Arrange
        var input = "English badword עברית";

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("English **** עברית"));
    }

    [Test]
    public void Censor_ZeroWidthCharacters_Preserved()
    {
        // Arrange
        var input = "bad\u200Bword badword"; // zero-width space in first

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("bad\u200Bword ****"));
    }

    [Test]
    public void Censor_SurrogatePairs_HandledCorrectly()
    {
        // Arrange
        var input = "😀 badword 😀"; // emoji are surrogate pairs in UTF-16

        // Act
        var result = _censor.Censor(input);

        // Assert
        Assert.That(result, Is.EqualTo("😀 **** 😀"));
    }
}