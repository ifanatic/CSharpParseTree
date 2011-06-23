using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharpParseTree.Library;

namespace CSharpParseTree.Common
{
    public class RemovePathProcessor : IStringProcessor
    {
        private string _path = string.Empty;
        
        public RemovePathProcessor(string path)
        {
            _path = path;
        }

        public void Process(ref string someString)
        {
            someString = someString.Replace(_path, "");
        }
    }
}
