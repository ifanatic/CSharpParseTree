using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using CSharpParseTreeLib;

namespace CSharpParseTree
{
    public partial class fMainForm : Form
    {
        public fMainForm()
        {
            InitializeComponent();
        }

        private void bChooseFile_Click(object sender, EventArgs e)
        {
            if (dOpenFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = dOpenFile.FileName;

                if(!File.Exists(fileName))
                {
                    throw new FileNotFoundException();
                }

                 string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                 appPath = System.IO.Path.GetDirectoryName(appPath);

                 TextBoxReportPrinter reportPrinter = new TextBoxReportPrinter(tbCompileReport);

                 MCSCompiler compiler = new MCSCompiler(Path.Combine(appPath, "dmcs.exe"));
                 if (!compiler.Compile(fileName, reportPrinter))
                 {
                     return;
                 }

                 /*RemovePathProcessor rpProcessor = new RemovePathProcessor(fileName);
                 List<IStringProcessor> processors = new List<IStringProcessor>();
                 processors.Add(rpProcessor);
                   

                 Stream outputStream = File.OpenWrite("C:\\cspt_output.xml");
                 XMLFormatBuilder xmlBuilder = new XMLFormatBuilder(outputStream);
                 FormatTextVisitor visitor = new FormatTextVisitor(xmlBuilder, processors);
                 xmlBuilder.WriteStartDocument();
                */
                Stream outputStream = File.OpenWrite("C:\\tags.txt");
                TagsVisitor visitor = new TagsVisitor(outputStream);

                 MCSClassElement se = new MCSClassElement("root", compiler.TreeRoot);
                 se.Visit(visitor);
                 visitor.Close();
                 outputStream.Close();
                 /*xmlBuilder.WriteEndDocument();
                 xmlBuilder.Close();
                 outputStream.Close();*/
            }
        }
    }
}
