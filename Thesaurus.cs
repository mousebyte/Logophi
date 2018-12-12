﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Caching;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MouseNet.Logophi
{
    public class Thesaurus
    {
        private readonly string _bookmarkPath =
            Path.Combine(Directory.GetCurrentDirectory(),
                         "data/bookmarks.lphi");

        private readonly string _cachePath =
            Path.Combine(Directory.GetCurrentDirectory(),
                         "data/cache.lphi");

        private List<string> _bookmarks = new List<string>();

        private ObjectCache
            _cache = new MemoryCache("ThesaurusCache");

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

        public void SearchWord
            (string word)
            {
            _searchTerm = word;
            if (_cache.Contains(word))
                Definitions = _cache[word] as List<WordDefinition>;
            else LoadFromWeb(word);
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
    }
}