
namespace WindowsFormsApplication1
{
    using System;
    using System.Collections.Generic;


    public class MyWord
    {
        public MyWord()
        {
            
        }

        public MyWord(string id, string word, string definition, string type, string date)
        {
            ID = id;
            Word = word;
            Definition = definition;
            Type = type;
            Date = date;
        }
        public string ID { get; set; }

        public string Word { get; set; }

        public string Definition { get; set; }

        public string Type { get; set; }

        public string Date { get; set; }

    }

    public class Noun : MyWord
    {
        public Noun(string id, string word, string definition, string type, string date)
            : base(id, word, definition, type, date)
        {
        }
    }

    public class Verb : MyWord
    {
        public Verb(string id, string word, string definition, string type, string date)
            : base(id, word, definition, type, date)
        {
        }
    }

    public class Adjective : MyWord
    {
        public Adjective(string id, string word, string definition, string type, string date)
            : base(id, word, definition, type, date)
        {
        }
    }

    public class Adverb : MyWord
    {
        public Adverb(string id, string word, string definition, string type, string date)
            : base(id, word, definition, type, date)
        {
        }
    }

    public class Saying : MyWord
    {
        public Saying(string id, string word, string definition, string type, string date)
            : base(id, word, definition, type, date)
        {
        }
    }

    public class Preposition : MyWord
    {
        public Preposition(string id, string word, string definition, string type, string date)
            : base(id, word, definition, type, date)
        {
        }
    }

    public class Idiom : MyWord
    {
        public Idiom(string id,string word, string definition, string type, string date)
            : base(id, word, definition, type, date)
        {
        }
    }

        public class PhrasalVerb : MyWord
    {
        public PhrasalVerb(string id,string word, string definition, string type, string date)
            : base(id, word, definition, type, date)
        {
        }
    }
}
