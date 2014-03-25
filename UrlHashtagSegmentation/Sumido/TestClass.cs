using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace UrlHashtagSegmentation.Sumido
{
    [TestFixture]
    class TestClass
    {
        private WordDictionary _dictionary;

        [SetUp]
        public void Initialize()
        {
            _dictionary = new WordDictionary();

            var allWords = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "words.txt"));

            foreach (var word in allWords)
            {
                _dictionary.Add(word.ToLower());
            }
        }

        [Test]
        public void when_i_token_possible_words_for_the_input_republicannoucement_then_the_output_should_be_a_list_of_re_republic_republican()
        {
            //Arrange
            var wordTokenizer = new WordTokenizer(_dictionary);

            //Act
            List<string> possibleWords = wordTokenizer.TokenizePossibleWords("republicannoucement");

            //Assert
            possibleWords.Count.Should().Be(3);
            possibleWords.Contains("re").Should().BeTrue();
            possibleWords.Contains("republic").Should().BeTrue();
            possibleWords.Contains("republican").Should().BeTrue();
        }

        [Test]
        public void when_i_token_possible_words_for_the_input_2014republicannoucement_then_the_output_should_be_123()
        {
            //Arrange
            var wordTokenizer = new WordTokenizer(_dictionary);

            //Act
            List<string> possibleWords = wordTokenizer.TokenizePossibleWords("123republic");

            //Assert
            possibleWords.Count.Should().Be(1);
            possibleWords.Contains("2014").Should().BeTrue();

        }

        [Test]
        public void when_segmentize_the_input_2014republicannouncement_then_the_output_should_be_2014_republic_announcement()
        {
            //Arrange
            var wordTokenizer = new WordTokenizer(_dictionary);

            //Act
            bool success = wordTokenizer.TokenizeAll("2014republicannouncement");

            //Assert
            success.Should().BeTrue();
            wordTokenizer.Ouput[1] = "2014";
            wordTokenizer.Ouput[2] = "republic";
            wordTokenizer.Ouput[3] = "announcement";


        }

    }

    public class WordTokenizer
    {
        private readonly IWordDictionary _wordDictionary;

        public WordTokenizer(WordDictionary dictionary)
        {
            _wordDictionary = dictionary;
        }

        public List<string> Ouput { get; set; }

        public List<string> TokenizePossibleWords(string input)
        {
            var tokens = new List<string>();
            var builder = new StringBuilder();
            var longestNumber = string.Empty;
            foreach (var character in input)
            {
                builder.Append(character);

                if(_wordDictionary.Lookup(builder.ToString()))
                {
                    tokens.Add(builder.ToString());
                }
                else if (IsNumeric(builder.ToString()))
                {
                    longestNumber = builder.ToString();
                }
            }

            return tokens.Count != 0 ? tokens :  new List<string>(){longestNumber};
        }

        public static bool IsNumeric(string input)
        {
            Double temp;
            bool result = Double.TryParse(input,out temp);
            return result;
        }

        public bool TokenizeAll(string input)
        {
            var tokens = TokenizePossibleWords(input);
            return true; //TODO fix this
        }
    }

    
}
