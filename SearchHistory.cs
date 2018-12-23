using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using MouseNet.Logophi.Properties;

namespace MouseNet.Logophi
{
    internal class SearchHistory : IEnumerable<string>
    {
        private readonly List<string> _data = new List<string>();

        private readonly string _filePath =
            Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                Resources.AppName,
                "history.lphi");

        private int _currentIndex;
        private int _maxItems;
        private bool _persistentHistory;

        public SearchHistory()
            {
            if (!File.Exists(_filePath)) return;
            _persistentHistory = true;
            var formatter = new BinaryFormatter();
            using (var strm = File.OpenRead(_filePath))
                _data = formatter.Deserialize(strm) as List<string>;
            }

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

        public bool PersistentHistory {
            get => _persistentHistory;
            set {
                _persistentHistory = value;
                if (value) WriteHistory();
                else File.Delete(_filePath);
            }
        }

        public IEnumerator<string> GetEnumerator()
            {
            return _data.GetEnumerator();
            }

        IEnumerator IEnumerable.GetEnumerator()
            {
            return GetEnumerator();
            }

        public void AddItem
            (string item)
            {
            if (Count > 1)
                _data.RemoveRange(_currentIndex + 1,
                                  Count - _currentIndex - 1);
            _data.Add(item);
            TrimHistory();
            _currentIndex = Count - 1;
            if (PersistentHistory) WriteHistory();
            }

        public void Clear()
            {
            _data.Clear();
            File.Delete(_filePath);
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

        private void TrimHistory()
            {
            var i = Count - MaxItems;
            if (i > 0) _data.RemoveRange(0, i);
            }

        private void WriteHistory()
            {
            var formatter = new BinaryFormatter();
            File.Delete(_filePath);
            using (var strm = File.OpenWrite(_filePath))
                formatter.Serialize(strm, _data);
            }
    }
}