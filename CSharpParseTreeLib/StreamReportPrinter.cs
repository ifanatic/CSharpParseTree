using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CSharpParseTreeLib 
{
    public class StreamReportPrinter : Mono.CSharp.ReportPrinter
    {
        readonly TextWriter writer;

        public StreamReportPrinter(TextWriter writer)
        {
            this.writer = writer;
        }

        public override void Print(Mono.CSharp.AbstractMessage msg)
        {
            Print(msg, writer);
            base.Print(msg);
        }
    }
}
