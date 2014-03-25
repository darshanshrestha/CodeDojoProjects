using System;
using System.Collections.Generic;
using System.Text;

namespace UrlHashtagSegmentation
{

    /*    Pseudo code for tokenizer 
 
     2014republicanxiety
    bool GenerateList(input)
    {
        Get list of words matching the beginning of input
    
        // ex: watermelons  would return { water ; watermelon ; watermelons } but would not include {me ; melon ; melons}
        // if input begins with a number, return a single element list that includes all the consecutive numbers
        // ex: 214phonenumbers would return a list {214}
    
        IF longest word == input // the end of the input has been reached, and a word (or number) exists to the end
        {
            Add word to outside list variable // this will be the first entry in the list, and will be the last word tokenized
            Return true
        }
 
        For each word in list, beginning from longest
        {
            IF(GenerateList(substring input characters after current word)) // returning true means we have completed searching and found the end of the input with a suitable word
                Add current word to outside List variable
                Return true
        }
 
        Return false // no suitable token list is available for this substring
        This will continue the for each from previous recursion calls, or if returned to the initial call, then no suitable word list was found.
    }
 
    Intial usage 
    IF(GenerateList(fullInput))
    THEN The outside list variable is now the tokenized list in backwards order
    ELSE No suitable list exists

    */
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
                if(input.StartsWith("www."))
                    retType = InputType.Url;
                else if(input.StartsWith("#"))
                    retType = InputType.Hashtag;
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
            for (; idx>0;idx--)
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

            if(string.IsNullOrEmpty(longestNumber))
                return returnList;
            else
                return new List<string>{longestNumber};
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
                    // TODO: only support www.???.domain.domain.domain
                    var uriObj = input.Substring(input.IndexOf('.')+1);
                    return uriObj.Substring(0,uriObj.IndexOf('.'));
                    break;
                    default:
                        return input;
            }
        }

        public List<string> GetWords(string input)
        {
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