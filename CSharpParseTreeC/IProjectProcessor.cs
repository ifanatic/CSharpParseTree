using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CSharpParseTree.Common;

namespace CSharpParseTreeC
{
    interface IProjectProcessor
    {
        void Process(SourceCodeProject project);
    }
}
