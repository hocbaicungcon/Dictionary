using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection.Emit;
using System.Threading;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public FileReader GetFile { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
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
                var oThread = new Thread(new ThreadStart(GetFile.ReadFile));
                oThread.Start();

                //Warning! it takes like 2 seconds to load all the data from this thread.
                //When you want to access the data while the thread is working it will cause an exception.
                //This while will wait till the thread is dead and while thread is working it will spit out logs     
                while (oThread.IsAlive)
                {
                    System.Console.WriteLine("The thread is processing data:{0}/{1}", GetFile.LinesProcessed, GetFile.LinesNumber);
                    System.Threading.Thread.Sleep(10);
                }
                System.Console.Write(GetFile.LinesProcessed); //check if all the lines were processed

                Update_List();
                
                 
            }
         }

        private void Update_List()
        {
            foreach (var word in GetFile.Output)
            {
                string[] row = { word.Word, word.Definition, word.Type, word.Date};
                listView1.Items.Add(GetFile.LinesProcessed.ToString()).SubItems.AddRange(row);      
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var output = GetFile.Output;
        }

    }
}
