using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpParseTreeLib
{
    public class MCSOtherElement : BaseElement
    {
        public MCSOtherElement(string name, object value)
            : base(name, value) { }

        public override IEnumerable<ITreeElement> GetChildrens()
        {
            return null;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.VisitMCSOtherElement(this);
        }
    }
}
