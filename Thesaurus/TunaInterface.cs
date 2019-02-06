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
    /// <summary>
    /// Consumes the <![CDATA["http://thesaurus.com/"]]> Tuna API and exposes
    /// functions to retrieve information about words from the web.
    /// </summary>
    public class TunaInterface
    {
        private readonly string _cachePath;

        private ObjectCache
            _cache = new MemoryCache("ThesaurusCache");

        private string _searchTerm;

        /// <summary>
        /// Creates a new instance of the <see cref="TunaInterface"/> class.
        /// </summary>
        /// <param name="dataDirectory">The location of the persistent cache on the filesystem.</param>
        /// <param name="persistentCache">A value indicating whether or not a persistent cache
        /// should be used.</param>
        protected TunaInterface
            (string dataDirectory,
             bool persistentCache)
            {
            //required for compatibility with Windows 7
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls
              | SecurityProtocolType.Tls11
              | SecurityProtocolType.Tls12
              | SecurityProtocolType.Ssl3;
            
            Definitions = new List<WordDefinition>();
            PersistentCache = persistentCache;
            _cachePath = Path.Combine(dataDirectory, "cache.lphi");
            LoadSavedData();
            }

        /// <summary>
        /// Gets a list of definitions returned by the last search.
        /// </summary>
        public List<WordDefinition> Definitions { get; private set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether or not a persistent cache should be used.
        /// </summary>
        public bool PersistentCache { get; set; }

        /// <summary>
        /// Gets or sets the value of the search term.
        /// </summary>
        public string SearchTerm {
            get => _searchTerm;
            set => SearchWord(value);
        }

        /// <summary>
        /// Clears the cache in memory, and from the filesystem if a persistent cache
        /// is being used.
        /// </summary>
        public void ClearCache()
            {
            _cache = new MemoryCache("ThesaurusCache");
            if (PersistentCache)
                File.Delete(_cachePath);
            }

        /// <summary>
        /// Retrieves word data from the web, or from the cache if possible.
        /// </summary>
        /// <param name="word">The word to search for.</param>
        public void SearchWord
            (string word)
            {
            _searchTerm = word;
            //if the word is present in the cache, load it from memory
            //otherwise attempt to load it from the web.
            if (_cache.Contains(word))
                Definitions = _cache[word] as List<WordDefinition>;
            else LoadFromWeb(word);
            InvokeWordSearched(this, word);
            }

        /// <summary>
        /// Loads word data from the web using the Tuna API.
        /// </summary>
        /// <param name="term">The word to search for.</param>
        private void LoadFromWeb
            (string term)
            {
            //create a new request to the Tuna API
            var request = WebRequest.CreateHttp(
                Resources.ThesaurusUrl + term.Trim().ToLower());
            
            //request the word data, and select the JToken
            //containing the definition data
            var data = RequestWordData(request)
               .SelectToken("data.definitionData.definitions");
            
            //deserialize the token into a list of WordDefinition objects
            //then update the cache
            Definitions = data?.ToObject<List<WordDefinition>>();
            UpdateCache();
            }

        /// <summary>
        /// Attempts to load the persistent cache file into memory.
        /// </summary>
        private void LoadSavedData()
            {
            var formatter = new BinaryFormatter();
            if (!PersistentCache || !File.Exists(_cachePath)) return;
            
            //open the file and deserialize its contents
            //if it can be successfully cast into an array of key-value pairs,
            //then iterate through the array to load it into the cache
            using (var strm = File.OpenRead(_cachePath))
                if (formatter.Deserialize(strm) is
                        KeyValuePair<string, object>[] items)
                    foreach (var i in items)
                        _cache[i.Key] = i.Value;
            }

        /// <summary>
        /// Gets the JSON response from the given Tuna API request and loads it into
        /// a <see cref="JObject"/>.
        /// </summary>
        /// <param name="request">The <see cref="WebRequest"/> representing a request to
        /// the Tuna API.</param>
        /// <returns></returns>
        private static JObject RequestWordData
            (WebRequest request)
            {
            //get the response and create a JsonTextReader from the response stream
            //then use that to load a JObject
            using (var resp = request.GetResponse())
                using (var sr =
                    new StreamReader(resp.GetResponseStream()))
                    using (var jr = new JsonTextReader(sr))
                        return JObject.Load(jr);
            }

        /// <summary>
        /// Caches the current search, and serializes the cache
        /// to a file if persistent caching is enabled.
        /// </summary>
        private void UpdateCache()
            {
            //if the current search is invalid or the cache already contains
            //a value for the current search term, don't bother updating
            if (Definitions == null || _cache.Contains(SearchTerm))
                return;
            _cache[SearchTerm] = Definitions;
            if (!PersistentCache) return;
            
            //if persistent caching is enabled, convert the cache to an array
            //and serialize it to a file
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

        /// <summary>
        /// Occurs when a word search has completed.
        /// </summary>
        public event EventHandler<string> WordSearched;
    }
}