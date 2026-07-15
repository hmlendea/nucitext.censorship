namespace NuciText.Censorship.English
{
    /// <summary>
    /// Filters text by censoring offensive words and URLs.
    /// </summary>
    public interface ITextCensor
    {
        /// <summary>
        /// Censors the given text by replacing bad words and URLs with asterisks.
        /// </summary>
        /// <param name="text">The text to censor.</param>
        /// <returns>The censored text.</returns>
        string Censor(string text);
    }
}
