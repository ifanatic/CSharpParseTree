using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpParseTree.Library
{
    public class MCSOtherElement : BaseElement
    {
        public MCSOtherElement(string name, object value)
            : base(name, value) { }

        public override IEnumerable<ITreeElement> GetChilds()
        {
            return null;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.VisitMCSOtherElement(this);
        }
    }
}
