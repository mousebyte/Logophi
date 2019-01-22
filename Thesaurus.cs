using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseNet.Logophi
{
    internal class Thesaurus : TunaInterface
    {
        public SearchHistory History { get; }
        private readonly StringCollection _str = new StringCollection();

        public Thesaurus
            (string dataDirectory,
             bool persistentCache,
             bool persistentHistory)
            : base(dataDirectory, persistentCache)
            {
            History =
                new SearchHistory(dataDirectory, persistentHistory);
            }

        public bool Bookmarked {
            get => _str.Contains(SearchTerm);
            set {
                if(value) AB(SearchTerm);
                else RB(SearchTerm);
            }
        }

        public void AB
            (string word)
            {
            if (_str.Contains(word)) return;
            _str.Add(word);
            
            }

        public void RB
            (string word)
            {
            if (!_str.Contains(word)) return;
            _str.Remove(word);
            }

        protected override void InvokeWordSearched
            (object sender,
             string args)
            {
            if(args != History.CurrentItem)
                History.AddItem(args);
            base.InvokeWordSearched(sender, args);
            }
    }
}