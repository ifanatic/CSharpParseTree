namespace CSharpParseTree
{
    partial class fMainForm
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
            this.tbFolderWithProject = new System.Windows.Forms.TextBox();
            this.bChooseFolder = new System.Windows.Forms.Button();
            this.tbFile = new System.Windows.Forms.TextBox();
            this.bChooseFile = new System.Windows.Forms.Button();
            this.dOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.tbCompileReport = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbFolderWithProject
            // 
            this.tbFolderWithProject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFolderWithProject.Location = new System.Drawing.Point(12, 12);
            this.tbFolderWithProject.Name = "tbFolderWithProject";
            this.tbFolderWithProject.Size = new System.Drawing.Size(544, 20);
            this.tbFolderWithProject.TabIndex = 0;
            // 
            // bChooseFolder
            // 
            this.bChooseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bChooseFolder.Location = new System.Drawing.Point(562, 10);
            this.bChooseFolder.Name = "bChooseFolder";
            this.bChooseFolder.Size = new System.Drawing.Size(119, 23);
            this.bChooseFolder.TabIndex = 1;
            this.bChooseFolder.Text = "Выбор папки";
            this.bChooseFolder.UseVisualStyleBackColor = true;
            // 
            // tbFile
            // 
            this.tbFile.Location = new System.Drawing.Point(12, 51);
            this.tbFile.Name = "tbFile";
            this.tbFile.Size = new System.Drawing.Size(544, 20);
            this.tbFile.TabIndex = 2;
            // 
            // bChooseFile
            // 
            this.bChooseFile.Location = new System.Drawing.Point(562, 48);
            this.bChooseFile.Name = "bChooseFile";
            this.bChooseFile.Size = new System.Drawing.Size(118, 23);
            this.bChooseFile.TabIndex = 3;
            this.bChooseFile.Text = "Выбор файла";
            this.bChooseFile.UseVisualStyleBackColor = true;
            this.bChooseFile.Click += new System.EventHandler(this.bChooseFile_Click);
            // 
            // dOpenFile
            // 
            this.dOpenFile.FileName = "Выбор файла";
            // 
            // tbCompileReport
            // 
            this.tbCompileReport.Location = new System.Drawing.Point(12, 216);
            this.tbCompileReport.Multiline = true;
            this.tbCompileReport.Name = "tbCompileReport";
            this.tbCompileReport.Size = new System.Drawing.Size(669, 188);
            this.tbCompileReport.TabIndex = 4;
            // 
            // fMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 416);
            this.Controls.Add(this.tbCompileReport);
            this.Controls.Add(this.bChooseFile);
            this.Controls.Add(this.tbFile);
            this.Controls.Add(this.bChooseFolder);
            this.Controls.Add(this.tbFolderWithProject);
            this.Name = "fMainForm";
            this.Text = "CSharpParseTree";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbFolderWithProject;
        private System.Windows.Forms.Button bChooseFolder;
        private System.Windows.Forms.TextBox tbFile;
        private System.Windows.Forms.Button bChooseFile;
        private System.Windows.Forms.OpenFileDialog dOpenFile;
        private System.Windows.Forms.TextBox tbCompileReport;
    }
}

