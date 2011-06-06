using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using CSharpParseTreeLib;
using CSharpParseTree.Common;

namespace CSharpParseTreeC
{
    class FormatTextProjectProcessor : IProjectProcessor
    {
        private Mono.CSharp.ReportPrinter _reporter = null;
        private IFormatBuilder _formatter = null;
        private List<IStringProcessor> _processors = null;

        public FormatTextProjectProcessor(Mono.CSharp.ReportPrinter reporter, IFormatBuilder formatter, List<IStringProcessor> processors)
        {
            _reporter = reporter;
            _formatter = formatter;
            _processors = processors;
        }
        
        public void Process(SourceCodeProject project)
        {
            MCSCompiler compiler = new MCSCompiler(Utils.GetDmcsAssemblyPath());
            if (!compiler.SuccessfulCreated)
            {
                return;
            }

            _formatter.WriteStartDocument();
            _formatter.WriteStartCustomElement("Project", "Name", project.Name);

            foreach (SourceFile file in project)
            {
                if (!compiler.Compile(file.FileFullPath, _reporter))
                {
                    continue;
                }

                FormatTextVisitor visitor = new FormatTextVisitor(_formatter, _processors);

                _formatter.WriteStartCustomElement("SourceFile", "Name", file.FileName);

                MCSClassElement root = new MCSClassElement("Root", compiler.TreeRoot);
                root.Visit(visitor);

                _formatter.WriteEndElement();
            }

            _formatter.WriteEndElement();
            _formatter.WriteEndDocument();
        }
    }
}
