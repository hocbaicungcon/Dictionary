// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Settings.cs" company="">
//   
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WindowsFormsApplication1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    public partial class Settings : Form
    {
        private int NumberOfFiles;

        public Settings()
        {
            NumberOfFiles = 0; // TODO: make it to be read from config file
            InitializeComponent();
        }

        private void SettingsLoadFileClick(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = @"D:\Projects\C#\Dictionary\WindowsFormsApplication1\WindowsFormsApplication1\Data\",
                RestoreDirectory = true

            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileDialog.FileName;
                NumberOfFiles++;
                var count = File.ReadLines(filename).Count();
                dictFileList.Items.Add(NumberOfFiles.ToString()).SubItems.AddRange(new string[] { filename, count.ToString() });
            }
        }
    }
}
