using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Caching;
using System.Runtime.Serialization.Formatters.Binary;
using MouseNet.Logophi.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MouseNet.Logophi.Thesaurus
{
    public class TunaInterface
    {
        private readonly string _bookmarkPath;
        private readonly string _cachePath;

        private ObjectCache
            _cache = new MemoryCache("ThesaurusCache");

        private string _searchTerm;

        protected TunaInterface
            (string dataDirectory,
             bool persistentCache)
            {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls
              | SecurityProtocolType.Tls11
              | SecurityProtocolType.Tls12
              | SecurityProtocolType.Ssl3;
            Definitions = new List<WordDefinition>();
            PersistentCache = persistentCache;
            _bookmarkPath =
                Path.Combine(dataDirectory, "bookmarks.lphi");
            _cachePath = Path.Combine(dataDirectory, "cache.lphi");
            LoadSavedData();
            }

        public List<WordDefinition> Definitions { get; private set; }
        public bool PersistentCache { get; set; }

        public string SearchTerm {
            get => _searchTerm;
            set => SearchWord(value);
        }

        public void ClearCache()
            {
            _cache = new MemoryCache("ThesaurusCache");
            if (PersistentCache)
                File.Delete(_cachePath);
            }

        public void SearchWord
            (string word)
            {
            _searchTerm = word;
            if (_cache.Contains(word))
                Definitions = _cache[word] as List<WordDefinition>;
            else LoadFromWeb(word);
            InvokeWordSearched(this, word);
            }

        private void LoadFromWeb
            (string term)
            {
            var request = WebRequest.CreateHttp(
                Resources.ThesaurusUrl + term.Trim().ToLower());
            var data = RequestWordData(request)
               .SelectToken("data.definitionData.definitions");
            Definitions = data?.ToObject<List<WordDefinition>>();
            UpdateCache();
            }

        private void LoadSavedData()
            {
            if (!File.Exists(_bookmarkPath)) return;
            var formatter = new BinaryFormatter();
            if (!PersistentCache || !File.Exists(_cachePath)) return;
            using (var strm = File.OpenRead(_cachePath))
                if (formatter.Deserialize(strm) is
                        KeyValuePair<string, object>[] items)
                    foreach (var i in items)
                        _cache[i.Key] = i.Value;
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

        private void UpdateCache()
            {
            if (Definitions == null || _cache.Contains(SearchTerm))
                return;
            _cache[SearchTerm] = Definitions;
            if (!PersistentCache) return;
            var formatter = new BinaryFormatter();
            using (var strm = File.OpenWrite(_cachePath))
                formatter.Serialize(strm, _cache.ToArray());
            }

        protected virtual void InvokeWordSearched
            (object sender,
             string args)
            {
            WordSearched?.Invoke(sender, args);
            }

        public event EventHandler<string> WordSearched;
    }
}