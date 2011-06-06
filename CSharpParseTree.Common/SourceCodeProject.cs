using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpParseTree.Common
{
    public abstract class SourceCodeProject : IEnumerable<SourceFile>
    {
        public string Name { get; protected set; }
        public int FilesCount { get; protected set; }

        public virtual IEnumerator<SourceFile> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
