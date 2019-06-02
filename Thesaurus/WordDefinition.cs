using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace MouseNet.Logophi.Thesaurus {
    /// <summary>
    ///     Contains data related to a specific definition of a word.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    public class WordDefinition {
        public WordDefinition
        (IReadOnlyCollection<TermEntry> antonyms,
         string definition,
         string partOfSpeech,
         IReadOnlyCollection<TermEntry> synonyms)
            {
            //sort antonyms and synonyms by similarity,
            //then by alphabetical order
            Antonyms = antonyms
                       .OrderBy(t => t.Similarity)
                       .ThenBy(t => t.Value)
                       .ToList();
            Definition = definition;
            PartOfSpeech = partOfSpeech;
            Synonyms = synonyms
                       .OrderByDescending(t => t.Similarity)
                       .ThenBy(t => t.Value)
                       .ToList();
            }

        /// <summary>
        ///     A list of the word's antonyms.
        /// </summary>
        [JsonProperty("antonyms")]
        public List<TermEntry> Antonyms { get; }

        /// <summary>
        ///     The definition of the word.
        /// </summary>
        [JsonProperty("definition")]
        public string Definition { get; }

        /// <summary>
        ///     The word's part of speech.
        /// </summary>
        [JsonProperty("pos")]
        public string PartOfSpeech { get; }

        /// <summary>
        ///     A list of the word's synonyms.
        /// </summary>
        [JsonProperty("synonyms")]
        public List<TermEntry> Synonyms { get; }
    }
}