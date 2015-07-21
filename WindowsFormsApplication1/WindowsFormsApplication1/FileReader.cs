using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace WindowsFormsApplication1
{
    public class FileReader
    {
        public struct MyWord
        {
            public string Word { get; set; }
            public string Definition { get; set; }
            public string Type { get; set; }
            public string Date { get; set; }

            //How To Improve It?
            public MyWord setValues(List<string> valuesList)
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
        
        
        private readonly string _filename;
        public int LinesNumber { get; private set; }
        public int LinesProcessed { get; private set; }
        public List<MyWord> Output { get; private set; }
        

        public FileReader(string filename)
        {
            this._filename = filename;
            Output = new List<MyWord>();
        }

        
        public void ReadFile()
        {        
            //Initialize lines numbers
            LinesNumber = File.ReadLines(_filename).Count();//don't need to close, it closes automatically
            LinesProcessed = 0;
            try
            {
                //Using statement will call a Disposable object on the reader object after exiting using scope
                //This will ensure secure resources disposal
                //Use this construction for reading files or any other resource-sensitive objects
                using (CsvFileReader reader = new CsvFileReader(_filename))
                {
                    CsvRow row = new CsvRow();
                    MyWord word = new MyWord();
                    List<string> tempList = new List<string>();
                    while (reader.ReadRow(row))
                    {
                        //This will iterate over all the wards in one line
                                
                        foreach (string s in row)
                        {
                            tempList.Add(s);
                        }
                        LinesProcessed++;
                        Output.Add(word.setValues(tempList));
                        tempList.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

    }
}