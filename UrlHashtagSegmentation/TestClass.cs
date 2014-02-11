using System;
using System.Collections.Generic;
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
        [Test]
        public void when_an_input_is_a_url_then_the_parser_can_determine_its_a_url()
        {
            string url = "www.domainname.com";
            UrlHashtagParser parser = new UrlHashtagParser();
            InputType inputType = parser.Identify(url);

            inputType.Should().Be(InputType.Url);
        }
        [Test]
        public void when_an_input_is_a_hashtag_then_parse_can_determine_its_a_hashtag()
        {
            string hash = "#thisisahash";
            UrlHashtagParser parser = new UrlHashtagParser();
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
            var parser = new UrlHashtagParser();
            var wordList = parser.Parse(input);

            
        }
    }

    public class UrlHashtagParser
    {
        public InputType Identify(String input)
        {
            var retType = InputType.Invalid;

            if (String.IsNullOrEmpty(input) == false)
            {
                if(input.StartsWith("www."))
                    retType = InputType.Url;
                else if(input.StartsWith("#"))
                    retType = InputType.Hashtag;
            }
            //TODO
            return retType;
        }

        internal object Parse(string input)
        {

            throw new NotImplementedException();
        }
    }



    public enum InputType
    {
        Url,Hashtag,Invalid
    }
}
