using System.Collections.Generic;

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
}