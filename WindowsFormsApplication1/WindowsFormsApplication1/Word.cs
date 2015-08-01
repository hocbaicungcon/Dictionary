
namespace WindowsFormsApplication1
{
    using System;
    using System.Collections.Generic;


    public class MyWord
    {
        public MyWord()
        {
            
        }

        public MyWord(string word, string definition, string type, string date)
        {
            Word = word;
            Definition = definition;
            Type = type;
            Date = date;
        }

        public string Word { get; set; }

        public string Definition { get; set; }

        public string Type { get; set; }

        public string Date { get; set; }


        public MyWord SetValues(List<string> valuesList)
        {
            try
            {
                Word = valuesList[0];
                Definition = valuesList[1];
                Type = valuesList[2];
                Date = valuesList[3];
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            return this;
        }
    }

    public class Noun : MyWord
    {
        public Noun(string word, string definition, string type, string date)
            : base(word, definition, type, date)
        {
        }
    }

    public class Verb : MyWord
    {
        public Verb(string word, string definition, string type, string date)
            : base(word, definition, type, date)
        {
        }
    }

    public class Adjective : MyWord
    {
        public Adjective(string word, string definition, string type, string date)
            : base(word, definition, type, date)
        {
        }
    }

    public class Adverb : MyWord
    {
        public Adverb(string word, string definition, string type, string date)
            : base(word, definition, type, date)
        {
        }
    }

    public class Saying : MyWord
    {
        public Saying(string word, string definition, string type, string date)
            : base(word, definition, type, date)
        {
        }
    }

    public class Preposition : MyWord
    {
        public Preposition(string word, string definition, string type, string date)
            : base(word, definition, type, date)
        {
        }
    }

    public class Idiom : MyWord
    {
        public Idiom(string word, string definition, string type, string date)
            : base(word, definition, type, date)
        {
        }
    }

        public class PhrasalVerb : MyWord
    {
        public PhrasalVerb(string word, string definition, string type, string date)
            : base(word, definition, type, date)
        {
        }
    }
}
