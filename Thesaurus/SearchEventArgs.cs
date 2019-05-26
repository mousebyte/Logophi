using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseNet.Logophi.Thesaurus
{
    /// <inheritdoc />
    /// <summary>
    /// Provides data for a search event.
    /// </summary>
    public class SearchEventArgs : EventArgs
    {
        public SearchEventArgs
            (string word,
             bool success)
            {
            Word = word;
            Success = success;
            }

        /// <summary>
        /// The word that was searched for.
        /// </summary>
        public string Word { get; }
        /// <summary>
        /// A value indicating whether or not the search was successful.
        /// </summary>
        public bool Success { get; }
    }
}