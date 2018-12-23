using System;
using Newtonsoft.Json;

namespace MouseNet.Logophi
{
    [JsonObject(MemberSerialization.OptIn), Serializable]
    public class TermEntry
    {
        public TermEntry
            (int similarity,
             string value)
            {
            Similarity = similarity;
            Value = value;
            }

        [JsonProperty("similarity")]
        public int Similarity { get; }
        [JsonProperty("term")]
        public string Value { get; }
    }
}