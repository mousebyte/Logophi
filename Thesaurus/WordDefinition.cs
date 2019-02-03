using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace MouseNet.Logophi.Thesaurus
{
    [JsonObject(MemberSerialization.OptIn), Serializable]
    public class WordDefinition
    {
        public WordDefinition
            (IReadOnlyCollection<TermEntry> antonyms,
             string definition,
             string partOfSpeech,
             IReadOnlyCollection<TermEntry> synonyms)
            {
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

        [JsonProperty("antonyms")]
        public List<TermEntry> Antonyms { get; }
        [JsonProperty("definition")]
        public string Definition { get; }
        [JsonProperty("pos")]
        public string PartOfSpeech { get; }
        [JsonProperty("synonyms")]
        public List<TermEntry> Synonyms { get; }
    }
}