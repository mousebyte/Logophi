using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MouseNet.Logophi.Thesaurus
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a collection of strings that track the history
    /// of a browser or similar application. The collection can be
    /// traversed and modified similarly to traditional browser history.
    /// </summary>
    internal class SearchHistory : IEnumerable<string>
    {
        private readonly List<string> _data = new List<string>();
        private readonly string _filePath;
        private int _currentIndex;
        private int _maxItems;
        private bool _persistentHistory;

        /// <summary>
        /// Creates a new instance of the <see cref="SearchHistory"/> class.
        /// </summary>
        /// <param name="dataDirectory">The location of the persistent cache on the filesystem.</param>
        /// <param name="persistentHistory">A value indicating whether or not a persistent history
        /// file should be used.</param>
        public SearchHistory
            (string dataDirectory,
             bool persistentHistory)
            {
            _filePath = Path.Combine(dataDirectory, "history.lphi");
            _persistentHistory = persistentHistory;
            
            //if persistent history is enabled and a history file exists, attempt to load it
            if (!persistentHistory || !File.Exists(_filePath)) return;
            var formatter = new BinaryFormatter();
            using (var strm = File.OpenRead(_filePath))
                _data = formatter.Deserialize(strm) as List<string>;
            }

        /// <summary>
        /// Gets a value indicating whether or not the history can be traversed
        /// backwards from the current item.
        /// </summary>
        public bool CanGoForward => _currentIndex < Count - 1;
        
        /// <summary>
        /// Gets a value indicating whether or not the history can be traversed
        /// forwards from the current item.
        /// </summary>
        public bool CanGoBackward => _currentIndex > 0;
        
        /// <summary>
        /// Gets the item at the current position in the history.
        /// </summary>
        public string CurrentItem =>
            _currentIndex < Count
                ? _data[_currentIndex]
                : string.Empty;
        
        /// <summary>
        /// Gets the number of items in the history.
        /// </summary>
        public int Count => _data.Count;

        /// <summary>
        /// Gets or sets the maximum number of items to keep in history.
        /// </summary>
        public int MaxItems {
            get => _maxItems;
            set {
                _maxItems = value;
                TrimHistory();
            }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether or not a persistent
        /// history file should be used.
        /// </summary>
        public bool PersistentHistory {
            get => _persistentHistory;
            set {
                _persistentHistory = value;
                if (value) WriteHistory();
                else File.Delete(_filePath);
            }
        }

        /// <inheritdoc />
        public IEnumerator<string> GetEnumerator()
            {
            return _data.GetEnumerator();
            }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
            {
            return GetEnumerator();
            }

        /// <summary>
        /// Deletes all items in front of the current position and
        /// adds the given value as the most recent item. The current
        /// position will be set to that of the newly added item.
        /// </summary>
        /// <param name="item">The item to add to the history.</param>
        public void AddItem
            (string item)
            {
            //delete any items ahead of the current position
            if (Count > 1)
                _data.RemoveRange(_currentIndex + 1,
                                  Count - _currentIndex - 1);
            
            //add the item and trim the history to ensure MaxItems
            //hasn't been exceeded
            _data.Add(item);
            TrimHistory();
            
            //set the current index to the index of the added item
            //and write the history to the disk if persistent history is
            //enabled.
            _currentIndex = Count - 1;
            if (PersistentHistory) WriteHistory();
            }

        /// <summary>
        /// Clears all items from the history.
        /// </summary>
        public void Clear()
            {
            _data.Clear();
            File.Delete(_filePath);
            }

        /// <summary>
        /// If possible, traverses the history in the backwards direction.
        /// </summary>
        public void GoBack()
            {
            if (!CanGoBackward) return;
            _currentIndex--;
            }

        /// <summary>
        /// If possible, traverses the history in the forwards direction.
        /// </summary>
        public void GoForward()
            {
            if (!CanGoForward) return;
            _currentIndex++;
            }

        /// <summary>
        /// Removes the item at the given index from the history.
        /// </summary>
        /// <param name="index">The index of the item to remove.</param>
        public void RemoveItem
            (int index)
            {
            //don't remove the current item, and eat any bounds errors
            if (index == _currentIndex
             || index > Count - 1
             || index < 0) return;
            _data.RemoveAt(index);
            WriteHistory();
            }

        /// <summary>
        /// Trims the back of the history to ensure that a count
        /// of <see cref="MaxItems"/> is not exceeded.
        /// </summary>
        private void TrimHistory()
            {
            var i = Count - MaxItems;
            if (i > 0) _data.RemoveRange(0, i);
            }

        /// <summary>
        /// Writes the history to a file.
        /// </summary>
        private void WriteHistory()
            {
            var formatter = new BinaryFormatter();
            File.Delete(_filePath);
            using (var strm = File.OpenWrite(_filePath))
                formatter.Serialize(strm, _data);
            }
    }
}