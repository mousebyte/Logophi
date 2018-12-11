using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Caching;
using System.Runtime.Serialization.Formatters.Binary;
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
        private readonly string _bookmarkPath =
            Path.Combine(Directory.GetCurrentDirectory(),
                         "data/bookmarks.lphi");

        private readonly string _cachePath =
            Path.Combine(Directory.GetCurrentDirectory(),
                         "data/cache.lphi");

        private ObjectCache
            _cache = new MemoryCache("ThesaurusCache");

        private List<string> _bookmarks = new List<string>();
        private string _searchTerm;

        public Thesaurus
            (bool persistentCache)
            {
            Definitions = new List<WordDefinition>();
            PersistentCache = persistentCache;
            var dataPath =
                Path.Combine(Directory.GetCurrentDirectory(), "data");
            if (!Directory.Exists(dataPath))
                Directory.CreateDirectory(dataPath);
            else LoadSavedData();
            }

        public List<WordDefinition> Definitions { get; private set; }
        public IEnumerable<string> Bookmarks => _bookmarks;

        public bool IsBookmarked {
            get => Bookmarks.Contains(SearchTerm);
            set {
                if (value) AddBookmark(SearchTerm);
                else RemoveBookmark(SearchTerm);
            }
        }

        public bool PersistentCache { get; set; }

        public string SearchTerm {
            get => _searchTerm;
            set => SearchWord(value);
        }

        public void AddBookmark
            (string value)
            {
            if (_bookmarks.Contains(value)) return;
            _bookmarks.Add(value);
            SaveBookmarks();
            }

        public void RemoveBookmark
            (string value)
            {
            if (!_bookmarks.Contains(value)) return;
            _bookmarks.Remove(value);
            SaveBookmarks();
            }

        private void UpdateCache()
            {
            if (Definitions == null || _cache.Contains(SearchTerm))
                return;
            _cache[SearchTerm] = Definitions;
            if (!PersistentCache) return;
            var formatter = new BinaryFormatter();
            using (var strm = File.OpenWrite(_cachePath))
                formatter.Serialize(strm, _cache);
            }

        private void LoadFromWeb
            (string term)
            {
            var request = WebRequest.CreateHttp(
                "https://tuna.thesaurus.com/pageData/"
              + term.Trim().ToLower());
            var data = RequestWordData(request)
               .SelectToken("data.definitionData.definitions");
            Definitions = data?.ToObject<List<WordDefinition>>();
            UpdateCache();
            }

        public void SearchWord
            (string word)
            {
            _searchTerm = word;
            if (_cache.Contains(word))
                Definitions = _cache[word] as List<WordDefinition>;
            else LoadFromWeb(word);
            }

        public void SearchWord
            (TermEntry term)
            {
            SearchWord(term.Value);
            }

        private void LoadSavedData()
            {
            if (!File.Exists(_bookmarkPath)) return;
            var formatter = new BinaryFormatter();
            using (var strm = File.OpenRead(_bookmarkPath))
                _bookmarks =
                    formatter.Deserialize(strm) as List<string>;
            if (!PersistentCache || !File.Exists(_cachePath)) return;
            using (var strm = File.OpenRead(_cachePath))
                _cache = formatter.Deserialize(strm) as MemoryCache;
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

        private void SaveBookmarks()
            {
            var formatter = new BinaryFormatter();
            using (var strm = File.OpenWrite(_bookmarkPath))
                formatter.Serialize(strm, _bookmarks);
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