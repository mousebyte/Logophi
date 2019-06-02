using System;

namespace MouseNet.Logophi.Thesaurus {
    /// <inheritdoc />
    /// <summary>
    ///     Provides data for a search event.
    /// </summary>
    public class SearchEventArgs : EventArgs {
        public SearchEventArgs
        (string word,
         bool success)
            {
            Word = word;
            Success = success;
            }

        /// <summary>
        ///     A value indicating whether or not the search was successful.
        /// </summary>
        public bool Success { get; }

        /// <summary>
        ///     The word that was searched for.
        /// </summary>
        public string Word { get; }
    }
}