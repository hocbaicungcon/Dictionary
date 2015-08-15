namespace WindowsFormsApplication1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;


    public partial class Form1 : Form
    {
        public Form1()
        {
            Form2 = new Settings();
            InitializeComponent();

            autoCompleteWordsList = new AutoCompleteStringCollection();
            this.findTextBox.AutoCompleteCustomSource = autoCompleteWordsList;
        }
        public FileReader LoadedFile { get; private set; } //TODO: refactor LoadedFile so it is a separate class, not FileReader
        private Settings Form2;
        private AutoCompleteStringCollection autoCompleteWordsList;


        //==========================================================================================================================================
        //                                              UI RESPONSE METHODS
        //==========================================================================================================================================

        private void btnLoadFileclick(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = @"D:\Projects\C#\Dictionary\WindowsFormsApplication1\WindowsFormsApplication1\Data\plainWords.txt", 
                RestoreDirectory = true
            }; 
            
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadedFile = new FileReader(openFileDialog.FileName);
                
                // Creating worker to run Reading File in the background
                BackgroundWorker worker = new BackgroundWorker();
                worker.WorkerReportsProgress = true; // needs to be set on true to be able to report progress
                worker.DoWork += (obj, ea) => LoadedFile.Read(ref worker); // delegate reading file to the bck worker
                worker.RunWorkerCompleted += (obj, ea) => this.UpdateList(); // when the work is done, call update list
                worker.ProgressChanged += new ProgressChangedEventHandler(ReportProgress); // Call ReportProgress() whenever progress is reported from ReadFile()
                worker.RunWorkerAsync(); // run the worker in async mode
            }
         }


        private void BtnSearchClick(object sender, EventArgs e)
        {
            
        }

        private void findTextBox_TextChanged(object sender, EventArgs e)
        {
            FilterListViewBasedOnSearchingCriteria(findTextBox.Text);
        }

        private void SettingsClick(object sender, EventArgs e)
        {
            Form2.ShowDialog(); 
        }

        /// <summary>
        /// Will be called, whenever progress is reported from worker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportProgress(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        //==========================================================================================================================================
        //                                              UI RESPONSE METHODS
        //==========================================================================================================================================

        /*
         * some old shit, maybe will be useful someday
        private List<int> FindItemsIndexesSearchingItemsAndSubitems()
        {
            //var moreItems = LoadedFile.Output.FindAll(ni => ni.Word.Equals(findTextBox.Text)); // TODO: searching through the output file
            List<int> foundMatchesIndexList = new List<int>();

            int index = 0;
            ListViewItem firstMatchForSearch = null;

            do
            {
                try
                {
                    firstMatchForSearch = listView1.FindItemWithText(findTextBox.Text, true, index, true);
                    foundMatchesIndexList.Add(firstMatchForSearch.Index);
                    if (firstMatchForSearch != null) index = firstMatchForSearch.Index + 1;
                }
                catch (Exception) { }
            } while (firstMatchForSearch != null);

            return foundMatchesIndexList;
        }
        */

        private void UpdateList()
        {
            for (int i = 0; i < LoadedFile.Output.Count(); i++)
            {
                var word = LoadedFile.Output[i];
                string[] row = { word.Word, word.Definition, word.Type, word.Date };
  
                listView1.Items.Add(word.ID).SubItems.AddRange(row);
                
                //AddWordsToAutoCompleteList(word.Word);
                //AddWordsToAutoCompleteList(word.Definition);
            }
            
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void AddWordsToAutoCompleteList(string stringToExtractWordsFrom)
        {
            //create char array with only alnum characters
            char[] arr = stringToExtractWordsFrom.Where(c => (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c))).ToArray();
            var str = new string(arr);

            var wordsList = str.Split(' ').ToList();

            foreach (var word in wordsList.Where(word => !autoCompleteWordsList.Contains(word)))
            {
                autoCompleteWordsList.Add(word);
            }
        }

        private void FilterListViewBasedOnSearchingCriteria(string searchingQuery)
        {
            listView1.Items.Clear(); // clear list items before adding 

            // filter the items match with search key and add result to list view 
            /*
             * Searching works like this:
             * 1. It will look for words starting with searching query and apply filters based on those found parts of words
             * 2. Next, If no words are found (condition = false) next condition is verified
             * 3. It will check if Definitions contain searching query inside and apply filter based on what was found
             * 4. When nothing is found in the definitions, last thing to check is word type.
             * TIP: when using ||, if the first condition is true -> next conditions aren't even checked
             */
          
            listView1.Items.AddRange(LoadedFile.Output
                .Where(i => string.IsNullOrEmpty(searchingQuery) || i.Word.StartsWith(searchingQuery, !checkBoxFilterCaseSensitive.Checked, null) 
                                                                 || i.Definition.Contains(searchingQuery, !checkBoxFilterCaseSensitive.Checked)
                                                                 || i.Type.StartsWith(searchingQuery, !checkBoxFilterCaseSensitive.Checked, null))
                .Select(c => new ListViewItem(new string[] { c.ID, c.Word, c.Definition, c.Date, c.Type })).ToArray());
        }

    }
}
