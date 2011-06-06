using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Mono.CSharp;

namespace CSharpParseTree
{
    class TextBoxReportPrinter : ReportPrinter
    {
        private TextBox _outputTextBox = null;
        
        public TextBoxReportPrinter(TextBox textBox)
        {
            _outputTextBox = textBox;
        }

        public override void Print(AbstractMessage msg)
        {
            _outputTextBox.AppendText(msg.MessageType + ": " + msg.Text + " - " + msg.Location.ToString() + "\n");
            base.Print(msg);
        }
    }
}
