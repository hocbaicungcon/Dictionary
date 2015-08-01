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

        public FileReader(string filename)
        {
            this.filename = filename;
            Output = new List<MyWord>();
        }

        /// <summary>
        /// Reads the csv file, decides what type of words are stored there and put them into Output list
        /// </summary>
        /// <param name="worker"></param>
        public void ReadFile(BackgroundWorker worker)
        {        
            // Initialize lines numbers
            LinesNumber = File.ReadLines(this.filename).Count();//don't need to close, it closes automatically
            LinesProcessed = 0;
            try
            {
                /* Using statement will call a Disposable object on the reader object after exiting using scope
                   This will ensure secure resources disposal
                   Use this construction for reading files or any other resource-sensitive objects */
                using (CsvFileReader reader = new CsvFileReader(this.filename))
                {
                    CsvRow row = new CsvRow();
                    List<string> tempList = new List<string>();
                    while (reader.ReadRow(row))
                    {
                        // This will iterate over all the wards in one line
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
                        else System.Console.WriteLine("Not enough elements in the lien number{0}:{1}", LinesProcessed, string.Join(",", row.ToArray()));
                        LinesProcessed++;

                        // Calculate and report progress in percentage
                        int progress = (LinesProcessed / LinesNumber) * 100;
                        worker.ReportProgress(progress);
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