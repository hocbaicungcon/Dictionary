namespace WindowsFormsApplication1
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLoadData = new System.Windows.Forms.Button();
            this.dictFileList = new System.Windows.Forms.ListView();
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Filename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NumberOfRecords = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // btnLoadData
            // 
            this.btnLoadData.Location = new System.Drawing.Point(408, 13);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(135, 23);
            this.btnLoadData.TabIndex = 1;
            this.btnLoadData.Text = "Load File";
            this.btnLoadData.UseVisualStyleBackColor = true;
            this.btnLoadData.Click += new System.EventHandler(this.SettingsLoadFileClick);
            // 
            // dictFileList
            // 
            this.dictFileList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dictFileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.Filename,
            this.NumberOfRecords});
            this.dictFileList.FullRowSelect = true;
            this.dictFileList.GridLines = true;
            this.dictFileList.Location = new System.Drawing.Point(12, 12);
            this.dictFileList.MultiSelect = false;
            this.dictFileList.Name = "dictFileList";
            this.dictFileList.Size = new System.Drawing.Size(390, 385);
            this.dictFileList.TabIndex = 2;
            this.dictFileList.UseCompatibleStateImageBehavior = false;
            this.dictFileList.View = System.Windows.Forms.View.Details;
            // 
            // ID
            // 
            this.ID.Text = "ID";
            this.ID.Width = 43;
            // 
            // Filename
            // 
            this.Filename.Text = "FilePath";
            this.Filename.Width = 250;
            // 
            // NumberOfRecords
            // 
            this.NumberOfRecords.Text = "No. Of Records";
            this.NumberOfRecords.Width = 99;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 436);
            this.Controls.Add(this.dictFileList);
            this.Controls.Add(this.btnLoadData);
            this.Name = "Settings";
            this.Text = "Settings";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.ListView dictFileList;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader Filename;
        private System.Windows.Forms.ColumnHeader NumberOfRecords;
    }
}