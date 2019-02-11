using System;
using Newtonsoft.Json;

namespace MouseNet.Logophi.Thesaurus
{
    /// <summary>
    ///     Represents an entry in a <see cref="WordDefinition" />'s list
    ///     of synonyms or antonyms.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    public class TermEntry
    {
        public TermEntry
            (int similarity,
             string value)
            {
            Similarity = similarity;
            Value = value;
            }

        /// <summary>
        ///     The similarity value of this word relative to
        ///     the <see cref="WordDefinition" />.
        /// </summary>
        [JsonProperty("similarity")]
        public int Similarity { get; }
        /// <summary>
        ///     The word itself.
        /// </summary>
        [JsonProperty("term")]
        public string Value { get; }
    }
}