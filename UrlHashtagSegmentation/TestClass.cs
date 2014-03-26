using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace UrlHashtagSegmentation
{
    [TestFixture]
    public class TestClass
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

        #region Previous tests

        [Test]
        public void when_an_input_is_a_url_then_the_parser_can_determine_its_a_url()
        {
            string url = "www.domainname.com";
            UrlHashtagParser parser = new UrlHashtagParser(_dictionary);
            InputType inputType = parser.Identify(url);

            inputType.Should().Be(InputType.Url);
        }

        [Test]
        public void when_an_input_is_a_hashtag_then_parse_can_determine_its_a_hashtag()
        {
            string hash = "#thisisahash";
            UrlHashtagParser parser = new UrlHashtagParser(_dictionary);
            InputType inputType = parser.Identify(hash);

            inputType.Should().Be(InputType.Hashtag);
        }

        [Test]
        public void when_add_a_word_to_dictionary_when_should_be_able_look_it()
        {
            string word = "word";

            var dictionary = new WordDictionary();
            dictionary.Add(word);
            var result = dictionary.Lookup(word);
            result.Should().Be(true);
        }

        [Test]
        public void when_add_a_word_to_dictionary_when_should_not_be_able_look_it()
        {
            string word = "word";

            var dictionary = new WordDictionary();
            var result = dictionary.Lookup(word);
            result.Should().Be(false);
        }

        [Test]
        public void when_input_is_valid_then_get_the_list_of_words()
        {
            var input = "domain30name";
            var parser = new UrlHashtagParser(_dictionary);
            //var wordList = parser.Parse(input);


        }

        #endregion


        [Test]
        public void
            when_i_token_possible_words_for_the_input_republicanxiety_then_the_output_should_be_a_list_of_re_republic_republican
            ()
        {
            //Arrange
            var urlHashTagParser = new UrlHashtagParser(_dictionary);

            //Act
            List<string> tokens = urlHashTagParser.TokenizePossibleWordsFromStart("republicanxiety");

            //Assert
            tokens.Count.Should().Be(3);
            tokens.Contains("re").Should().BeTrue();
            tokens.Contains("republic").Should().BeTrue();
            tokens.Contains("republican").Should().BeTrue();
        }

        [Test]
        public void when_i_token_possible_words_for_the_input_2014republicannoucement_then_the_output_should_be_2014()
        {
            //Arrange
            var urlHashTagParser = new UrlHashtagParser(_dictionary);
            //Act
            List<string> tokens = urlHashTagParser.TokenizePossibleWordsFromStart("2014republicannoucement");

            //Assert
            tokens.Count.Should().Be(1);
            tokens.Contains("2014").Should().BeTrue();

        }

        //Guiding Test
        [Test]
        public void when_segmentize_the_input_2014republicanxiety_then_the_output_should_be_2014_republic_anxiety()
        {
            //Arrange
            var urlHashTagParser = new UrlHashtagParser(_dictionary);
            //Act
            List<string> words = urlHashTagParser.Parse("2014republicanxiety");

            //Assert
            words.Count.Should().Be(3);
            words[0].Should().Be("2014");
            words[1].Should().Be("republic");
            words[2].Should().Be("anxiety");
        }

        //Guiding Test
        [Test]
        public void when_segmentize_the_input_2014republiaxiety_then_the_output_should_be_empty_list()
        {
            //Arrange
            var urlHashTagParser = new UrlHashtagParser(_dictionary);
            //Act
            List<string> words = urlHashTagParser.Parse("2014republiaxiety");

            //Assert
            words.Count.Should().Be(0);
        }

        //Guiding Test
        [Test]
        public void when_segmentize_the_input_2014republic2013anxiety_then_the_output_should_be_2014_republic_2013_anxiety()
        {
            //Arrange
            var urlHashTagParser = new UrlHashtagParser(_dictionary);
            //Act
            List<string> words = urlHashTagParser.Parse("2014republic2013anxiety");

            //Assert
            words.Count.Should().Be(4);
            words[0].Should().Be("2014");
            words[1].Should().Be("republic");
            words[2].Should().Be("2013");
            words[3].Should().Be("anxiety");
        }

        //Guiding Test
        [Test]
        public void when_segmentize_the_input_2014republic2013anxiety_using_identify_then_the_output_should_be_2014_republic_2013_anxiety()
        {
            //Arrange
            var urlHashTagParser = new UrlHashtagParser(_dictionary);
            //Act

            List<string> words = urlHashTagParser.Parse("2014republic2013anxiety");

            //Assert
            words.Count.Should().Be(4);
            words[0].Should().Be("2014");
            words[1].Should().Be("republic");
            words[2].Should().Be("2013");
            words[3].Should().Be("anxiety");
        }

        [Test]
        public void when_GetWords_urlType_wwwThisisinsanecom_then_the_output_should_be_this_is_insane()
        {
            _dictionary.Add("is");
            _dictionary.Add("insane");
            var urlHashTagParser = new UrlHashtagParser(_dictionary);
            List<string> words = urlHashTagParser.GetWords("www.thisisinsane.com");

            words.Count.Should().Be(3);

            words[0].Should().Be("this");
            words[1].Should().Be("is");
            words[2].Should().Be("insane");
        }
    }

    public enum InputType
    {
        Url,
        Hashtag,
        Invalid
    }
}
