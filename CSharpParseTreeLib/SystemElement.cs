using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpParseTree.Library
{
    public class SystemElement : BaseElement
    {
        public SystemElement(String name, Object o)
            : base(name, o)
        {
        }

        public override IEnumerable<ITreeElement> GetChildrens()
        {
            return null;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.VisitSystemElement(this);
        }
    }
}
