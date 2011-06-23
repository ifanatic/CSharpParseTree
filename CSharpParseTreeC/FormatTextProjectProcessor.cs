using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using CSharpParseTree.Library;
using CSharpParseTree.Common;

namespace CSharpParseTreeC
{
    class FormatTextProjectProcessor : AbstractProjectProcessor
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

        public override void Process(SourceCodeProject project)
        {
            MCSCompiler compiler = new MCSCompiler(Utils.GetDmcsAssemblyPath(), _reporter);
            if (!compiler.SuccessfulCreated)
            {
                return;
            }

            _formatter.WriteStartDocument();
            _formatter.WriteStartCustomElement("Project", "Name", project.Name);

            int filesProcessed = 0;

            DoProgress(0);

            foreach (SourceFile file in project)
            {
                if (!compiler.Compile(file.FileFullPath))
                {
                    continue;
                }

                FormatTextVisitor visitor = new FormatTextVisitor(_formatter, _processors);

                _formatter.WriteStartCustomElement("SourceFile", "Name", file.PathInProject);

                MCSClassElement root = new MCSClassElement("Root", compiler.TreeRoot);
                root.Visit(visitor);

                _formatter.WriteEndElement();

                filesProcessed++;

                DoProgress(filesProcessed * 100 / project.FilesCount);
            }

            _formatter.WriteEndElement();
            _formatter.WriteEndDocument();
        }
    }
}
