using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace WindowsFormsApplication1
{
    public class FileReader
    {
        private readonly string _filename;
        public int LinesNumber { get; private set; }
        public int LinesProcessed { get; private set; }
		/*
		public FileReader(string filename)
		{
			//Think about overloading constructor so that the class could handle more files
			//for example .csv .txt etc.
			//Not sure how do do this, since .txt file can have infinite variety
			//Just try to make this class as generic as possible
		}
		*/
        public FileReader(string filename)
        {
            this._filename = filename;
            Output = new List<string>();
        }

        
        public List<string> Output { get; private set; }

        public void ReadFile()
        {        
            //Initialize lines numbers
            LinesNumber = File.ReadLines(_filename).Count();
            LinesProcessed = 0;


            //Using statement will call a Disposable object on the reader object after exiting using scope
            //This will ensure secure resources disposal
            //Use this construction for reading files or any other resource-sensitive objects
            using (CsvFileReader reader = new CsvFileReader(_filename))
            {
                CsvRow row = new CsvRow();
                while (reader.ReadRow(row))
                {
                    //This will iterate over all the wards in one line
                    foreach (string s in row)
                    {
                        Output.Add(s);
                    }
                    LinesProcessed++;
                }
            }
        }

    }
}