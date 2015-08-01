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
        }

        private Settings Form2;

        public FileReader GetFile { get; private set; }

        private void Button1Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = @"D:\Projects\C#\Dictionary\WindowsFormsApplication1\WindowsFormsApplication1\Data\plainWords.txt", 
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filename = openFileDialog.FileName;
                GetFile = new FileReader(filename);
                
                // Creating worker to run Reading File in the background
                BackgroundWorker worker = new BackgroundWorker();
                worker.WorkerReportsProgress = true; // needs to be set on true to be able to report progress
                worker.DoWork += (obj, ea) => GetFile.ReadFile(worker); // delegate reading file to the bck worker
                worker.RunWorkerCompleted += (obj, ea) => this.UpdateList(); // when the work is done, call update list
                worker.ProgressChanged += new ProgressChangedEventHandler(ReportProgress); // Call ReportProgress() whenever progress is reported from ReadFile()
                worker.RunWorkerAsync(); // run the worker in async mode
            }
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

        private void UpdateList()
        {
            for (int i = 0; i < GetFile.Output.Count(); i++)
            {
                var word = GetFile.Output[i];
                string[] row = {word.Word, word.Definition, word.Type, word.Date};
                listView1.Items.Add((i + 1).ToString()).SubItems.AddRange(row);
            }

            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void BtnSearchClick(object sender, EventArgs e)
        {
            var moreItems = GetFile.Output.FindAll(ni => ni.Word.Equals("adhesive")); // TODO: implement searching
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            Form2.ShowDialog();
        }
    }
}
