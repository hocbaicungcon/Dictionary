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

        private int linesNumber;

        private int linesProcessed;

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
        public void Read(ref BackgroundWorker worker)
        {
            // Initialize lines numbers
            int oldProgress = 0;
            linesNumber = File.ReadLines(this.filename).Count(); // don't need to close, it closes automatically
            linesProcessed = 0;

            if (this.filename.EndsWith("csv"))
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
                            CalculateProgress(ref worker, ref oldProgress);
                        }
                    }
            }
            else if (this.filename.EndsWith("tsv"))
            {
                throw new NotImplementedException("Sorry bro");
            }
            else if (this.filename.EndsWith("txt"))
            {
                throw new NotImplementedException("TXT jeszcze nie");
            }
            else
            {
                throw new NotImplementedException("sorry bro");
            }
        }

        private void CalculateProgress(ref BackgroundWorker worker, ref int oldProgress)
        {
            // Calculate and report progress in percentage
            linesProcessed++;
            double progress = ((double)linesProcessed / (double)linesNumber) * 100;

            // report only if there was was a whole change in progress, 1->2, 56->57 etc.
            if ((int)progress != oldProgress)
            {
                worker.ReportProgress((int)progress); // Progress will be reported only 100 times
            }

            oldProgress = (int)progress;
        }

        /// <summary>
        /// Row is a LIST containg all elements from single row, lets assume the following order:
        /// row[0] - ID
        /// row[1] - word
        /// row[2] - definition
        /// row[3] - type
        /// row[4] - date added
        /// Parsed data is converted into appropiate word types and added to the Output field
        /// Data that was couldn't be parrsed is put as a string into OutputRevokedElements list
        /// </summary>
        /// <param name="row"></param>
        private void ParseRowList(CsvRow row)
        {
            if (row.Count >= 5)
            {
                // We can put different words (nouns, verbs etc.) into one list(MyWord) bcos they inherit from MyWord
                if (row[3].ToLower().Equals("noun")) Output.Add(new Noun(row[0], row[1], row[2], row[3], row[4]));
                else if (row[3].ToLower().Equals("verb")) Output.Add(new Verb(row[0], row[1], row[2], row[3], row[4]));
                else if (row[3].ToLower().Equals("adverb")) Output.Add(new Adverb(row[0], row[1], row[2], row[3], row[4]));
                else if (row[3].ToLower().Equals("adjective")) Output.Add(new Adjective(row[0], row[1], row[2], row[3], row[4]));
                else if (row[3].ToLower().Equals("saying")) Output.Add(new Saying(row[0], row[1], row[2], row[3], row[4]));
                else if (row[3].ToLower().Equals("preposition")) Output.Add(new Preposition(row[0], row[1], row[2], row[3], row[4]));
                else if (row[3].ToLower().Equals("idiom")) Output.Add(new Idiom(row[0], row[1], row[2], row[3], row[4]));
                else if (row[3].ToLower().Equals("phrasal verb")) Output.Add(new PhrasalVerb(row[0], row[1], row[2], row[3], row[4]));
                else Output.Add(new MyWord(row[0], row[1], row[2], row[3], row[4]));
            }
            else OutputRevokedElements.Add(string.Join(",", row.ToArray()));
        }
    }
}