using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Caching;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MouseNet.Logophi
{
    internal class SearchHistory
    {
        private readonly List<string> _data = new List<string>();
        private int _currentIndex;
        public bool CanGoForward => _currentIndex < _data.Count - 1;
        public bool CanGoBackward => _currentIndex > 0;
        public string CurrentItem =>
            _currentIndex < _data.Count
                ? _data[_currentIndex]
                : string.Empty;
        public int Count => _data.Count;

        public void AddItem
            (string item)
            {
            if (_data.Count > 1)
                _data.RemoveRange(_currentIndex + 1,
                                  _data.Count - _currentIndex - 1);
            _data.Add(item);
            _currentIndex = _data.Count - 1;
            }

        public void GoBack()
            {
            if (!CanGoBackward) return;
            _currentIndex--;
            }

        public void GoForward()
            {
            if (!CanGoForward) return;
            _currentIndex++;
            }

        public void RemoveItem
            (int index)
            {
            if (index == _currentIndex
             || index > _data.Count - 1
             || index < 0) return;
            _data.RemoveAt(index);
            }
    }

    [JsonObject(MemberSerialization.OptIn)]
    internal class TermEntry
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

    [JsonObject(MemberSerialization.OptIn)]
    internal class WordDefinition
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

    internal class Thesaurus
    {
        private string _searchTerm;

        public Thesaurus()
            {
            Definitions = new List<WordDefinition>();
            }

        public List<WordDefinition> Definitions { get; private set; }

        public string SearchTerm {
            get => _searchTerm;
            set => SearchWord(value);
        }

        public void SearchWord
            (string word)
            {
            _searchTerm = word;
            if (!MemoryCache.Default.Contains(_searchTerm))
                {
                var request = WebRequest.CreateHttp(
                    "https://tuna.thesaurus.com/pageData/"
                  + word.Trim().ToLower());
                var data = RequestWordData(request)
                   .SelectToken("data.definitionData.definitions");
                Definitions = data?.ToObject<List<WordDefinition>>();
                if (Definitions != null)
                    MemoryCache.Default[_searchTerm] = Definitions;
                } else
                Definitions =
                    MemoryCache.Default[_searchTerm] as
                        List<WordDefinition>;
            }

        public void SearchWord
            (TermEntry term)
            {
            SearchWord(term.Value);
            }

        private static JObject RequestWordData
            (WebRequest request)
            {
            using (var resp = request.GetResponse())
                using (var sr =
                    new StreamReader(resp.GetResponseStream()))
                    using (var jr = new JsonTextReader(sr))
                        return JObject.Load(jr);
            }
    }

    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
            {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            }
    }
}