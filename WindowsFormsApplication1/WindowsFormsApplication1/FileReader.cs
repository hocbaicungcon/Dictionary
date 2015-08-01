namespace WindowsFormsApplication1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    

    public class FileReader
    {
        private readonly string filename;
        
        public int LinesNumber { get; private set; }

        public int LinesProcessed { get; private set; }

        public List<MyWord> Output { get; private set; }
        
        public List<string> OutputRevokedElements { get; private set; }

        public FileReader(string filename)
        {
            this.filename = filename;
            Output = new List<MyWord>();
            OutputRevokedElements = new List<string>();
        }

        /// <summary>
        /// Reads the csv file, decides what type of words are stored there and put them into Output list
        /// </summary>
        /// <param name="worker"></param>
        public void ReadFile(BackgroundWorker worker)
        {        
            // Initialize lines numbers
            int oldProgress = 0;
            LinesNumber = File.ReadLines(this.filename).Count(); // don't need to close, it closes automatically
            LinesProcessed = 0;
            try
            {
                /* Using statement will call a Disposable object on the reader object after exiting using scope
                   This will ensure secure resources disposal
                   Use this construction for reading files or any other resource-sensitive objects */
                using (CsvFileReader reader = new CsvFileReader(this.filename))
                {
                    CsvRow row = new CsvRow();
                    while (reader.ReadRow(row))
                    {
                        ParseRowList(row);
                        
                        // Calculate and report progress in percentage
                        LinesProcessed++;
                        double progress = ((double)LinesProcessed / (double)LinesNumber) * 100;
                        
                        // report only if there was was a whole change in progress, 1->2, 56->57 etc.
                        if ((int)progress != oldProgress) 
                        {
                            worker.ReportProgress((int)progress); // Progress will be reported only 100 times
                        }

                        oldProgress = (int)progress;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Row is a LIST containing 4 elements:
        /// row[0] - word
        /// row[1] - definition
        /// row[2] - type
        /// row[3] - date added
        /// Parsed data is converted into appropiate word types and added to the Output field
        /// Data that was couldn't be parrsed is put as a string into OutputRevokedElements list
        /// </summary>
        /// <param name="row"></param>
        private void ParseRowList(CsvRow row)
        {
            if (row.Count >= 4)
            {
                // We can put different words (nouns, verbs etc.) into one list(MyWord) bcos they inherit from MyWord
                if (row[2].ToLower().Equals("noun")) Output.Add(new Noun(row[0], row[1], row[2], row[3]));
                else if (row[2].ToLower().Equals("verb")) Output.Add(new Verb(row[0], row[1], row[2], row[3]));
                else if (row[2].ToLower().Equals("adverb")) Output.Add(new Adverb(row[0], row[1], row[2], row[3]));
                else if (row[2].ToLower().Equals("adjective")) Output.Add(new Adjective(row[0], row[1], row[2], row[3]));
                else if (row[2].ToLower().Equals("saying")) Output.Add(new Saying(row[0], row[1], row[2], row[3]));
                else if (row[2].ToLower().Equals("preposition")) Output.Add(new Preposition(row[0], row[1], row[2], row[3]));
                else if (row[2].ToLower().Equals("idiom")) Output.Add(new Idiom(row[0], row[1], row[2], row[3]));
                else if (row[2].ToLower().Equals("phrasal verb")) Output.Add(new PhrasalVerb(row[0], row[1], row[2], row[3]));
                else Output.Add(new MyWord(row[0], row[1], row[2], row[3]));
            }
            else OutputRevokedElements.Add(string.Join(",", row.ToArray()));
        }
    }
}