using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpParseTree.Library
{
    public class EnumerableElement : BaseElement
    {
        public EnumerableElement(String name, Object value)
            : base(name, value)
        {
            if (!(value is IEnumerable))
            {
                throw new BadElementTypeException("IEnumerable", value.GetType());
            }
        }

        public override IEnumerable<ITreeElement> GetChildrens()
        {
            IEnumerable collection = ValueObject as IEnumerable;
            ITreeElement newElement = null;

            foreach (var element in collection)
            {
                newElement = TreeElementCreator.CreateFromObject(Name, element);

                if(newElement != null)
                {
                    yield return newElement;
                }
            }
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.VisitEnumerableElement(this);
        }
    }
}
