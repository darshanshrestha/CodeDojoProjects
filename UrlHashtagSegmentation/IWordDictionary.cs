using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlHashtagSegmentation
{
    public interface IWordDictionary
    {
        void Add(string word);
        bool Lookup(string word);
        

    }

    class WordDictionary : IWordDictionary
    {
        private HashSet<string> dictionary = new HashSet<string>();

        public void Add(string word)
        {
            dictionary.Add(word);
        }

        public bool Lookup(string word)
        {
            return dictionary.Contains(word);
        }
    }
}
