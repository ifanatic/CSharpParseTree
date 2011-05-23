using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpParseTreeLib
{
    public class OtherElement : BaseElement
    {
        public OtherElement(String name, Object o)
            : base(name, o)
        {
        }

        public override IEnumerable<ITreeElement> GetChildrens()
        {
            return null;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.VisitOtherElement(this);
        }
    }
}
