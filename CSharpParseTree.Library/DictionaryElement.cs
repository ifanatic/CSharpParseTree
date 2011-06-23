using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpParseTree.Library
{
    public class DictionaryElement : BaseElement
    {
        public DictionaryElement(String name, Object o)
            : base(name, o)
        {
            if (!(o is IDictionary))
            {
                throw new BadElementTypeException("IDictionary", o.GetType());
            }
        }

        public override IEnumerable<ITreeElement> GetChildrens()
        {
            IDictionary dict = (ValueObject as IDictionary);
            
            ITreeElement newElement = null;

            foreach (var key in dict.Keys)
            {
                newElement = TreeElementCreator.CreateFromObject(key.ToString(), dict[key]);
                
                if(newElement != null)
                {
                    yield return newElement;
                }
            }                
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.VisitDictionaryElement(this);
        }
    }
}
