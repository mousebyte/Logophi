using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MouseNet.Logophi
{
    internal class SearchHistory
    {
        private readonly List<string> _data = new List<string>();
        private int _currentIndex;

        private readonly string _filePath =
            Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                "Logophi", "history.lphi");

        private bool _persistentHistory;
        private int _maxItems;
        public bool CanGoForward => _currentIndex < Count - 1;
        public bool CanGoBackward => _currentIndex > 0;
        public string CurrentItem =>
            _currentIndex < Count
                ? _data[_currentIndex]
                : string.Empty;
        public int Count => _data.Count;

        public int MaxItems {
            get => _maxItems;
            set {
                _maxItems = value;
                TrimHistory();
            }
        }

        public SearchHistory()
            {
            if (!File.Exists(_filePath)) return;
            var formatter = new BinaryFormatter();
            using (var strm = File.OpenRead(_filePath))
                _data = formatter.Deserialize(strm) as List<string>;
            }

        public bool PersistentHistory {
            get => _persistentHistory;
            set {
                _persistentHistory = value;
                if (value) WriteHistory();
                else File.Delete(_filePath);
            }
        }

        private void WriteHistory()
            {
            var formatter = new BinaryFormatter();
            using (var strm = File.OpenWrite(_filePath))
                formatter.Serialize(strm, _data);
            }

        private void TrimHistory()
            {
            var i = Count - MaxItems;
            if (i > 0) _data.RemoveRange(0, i);
            }

        public void AddItem
            (string item)
            {
            if (Count > 1)
                _data.RemoveRange(_currentIndex + 1,
                                  Count - _currentIndex - 1);
            _data.Add(item);
            _currentIndex = Count - 1;
            TrimHistory();
            if (PersistentHistory) WriteHistory();
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
             || index > Count - 1
             || index < 0) return;
            _data.RemoveAt(index);
            WriteHistory();
            }

        public void Clear()
            {
            _data.Clear();
            File.Delete(_filePath);
            }
    }
}