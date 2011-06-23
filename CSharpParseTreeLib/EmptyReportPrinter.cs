using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpParseTree.Library
{
    public class EmptyReportPrinter : Mono.CSharp.ReportPrinter
    {
        public override void Print(Mono.CSharp.AbstractMessage msg)
        {
            base.Print(msg);
        }
    }
}
