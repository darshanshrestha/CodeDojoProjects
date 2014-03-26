using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Submission
{
    public enum InputType
    {
        Url,
        Hashtag,
        Invalid
    }

    class Solution
    {
        static void Main(string[] args)
        {
            var dictionary = new WordDictionary();

            var allWords = File.ReadAllLines("words.txt");

            foreach (var word in allWords)
            {
                dictionary.Add(word.ToLower());
            }

            var urlHashTagParser = new UrlHashtagParser(dictionary);

            var numberOfItems = int.Parse(Console.ReadLine());

            List<string> urlHashtags = new List<string>();

            for (int i = 0; i < numberOfItems; i++)
            {
                urlHashtags.Add(Console.ReadLine());
            }

            foreach (string urlHashtag in urlHashtags)
            {
                var words = urlHashTagParser.GetWords(urlHashtag);

                Console.WriteLine(string.Join(" ", words));

            }

            
        }
    }

    public interface IWordDictionary
    {
        void Add(string word);
        bool Lookup(string word);


    }

    public class WordDictionary : IWordDictionary
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

    public class UrlHashtagParser
    {
        private IWordDictionary _dictionary;
        public List<string> Output = new List<string>();

        public UrlHashtagParser(IWordDictionary dictionary)
        {
            _dictionary = dictionary;
        }

        public InputType Identify(String input)
        {
            var retType = InputType.Invalid;

            if (String.IsNullOrEmpty(input) == false)
            {
                if (input.StartsWith("#"))
                    retType = InputType.Hashtag;
                else
                    retType = InputType.Url;

            }
            //TODO
            return retType;
        }

        public List<string> ParseLine(string line)
        {
            var outputList = new List<string>();

            int currentLength = line.Length;
            string currentLine = line;

            while (currentLength > 0)
            {
                currentLength = currentLength - FindLongestWord(currentLine);
                currentLine = currentLine.Substring(0, currentLength);
                outputList.Add(currentLine);
            }
            return outputList;
        }

        public int FindLongestWord(string input)
        {
            var idx = input.Length - 1;
            for (; idx > 0; idx--)
            {
                if (_dictionary.Lookup(input.Substring(0, idx)))
                {
                    Output.Add(input);
                    break;
                }
            }
            return idx;
        }

        public List<string> TokenizePossibleWordsFromStart(string input)
        {
            var builder = new StringBuilder();
            var returnList = new List<string>();
            var longestNumber = string.Empty;
            foreach (var character in input)
            {
                builder.Append(character);
                if (_dictionary.Lookup(builder.ToString()))
                    returnList.Add(builder.ToString());
                else
                {
                    if (IsNumeric(builder.ToString()))
                    {
                        longestNumber = builder.ToString();
                    }
                }
            }

            if (string.IsNullOrEmpty(longestNumber))
                return returnList;
            else
                return new List<string> { longestNumber };
        }

        private bool IsNumeric(string input)
        {
            int data;
            return int.TryParse(input, out data);
        }

        List<string> FinalOutput = new List<string>();

        public string SanitizeInput(string input)
        {
            var type = Identify(input);

            switch (type)
            {
                case InputType.Hashtag:
                    return input.TrimStart('#');
                    break;
                case InputType.Url:
                    if (input.StartsWith("www."))
                    {
                        input = input.Substring(4);
                    }
                    //var uriObj = input.Substring(input.IndexOf('.') + 1);
                    return input.Substring(0, input.IndexOf('.'));
                    break;
                default:
                    return input;
            }
        }

        public List<string> GetWords(string input)
        {
            FinalOutput = new List<string>();
            return Parse(SanitizeInput(input));
        }

        public List<string> Parse(string input)
        {
            if (GenerateList(input))
            {
                FinalOutput.Reverse();
                return FinalOutput;
            }
            return new List<string>();
        }

        private bool GenerateList(string input)
        {
            var tokens = TokenizePossibleWordsFromStart(input);
            if (tokens.Count == 0)
                return false;
            tokens.Reverse();
            if (tokens[0] == input)
            {
                FinalOutput.Add(tokens[0]);
                return true;
            }

            foreach (string token in tokens)
            {
                if (GenerateList(input.Substring(token.Length)))
                {
                    FinalOutput.Add(token);
                    return true;
                }
            }
            return false;
        }
    }
}
