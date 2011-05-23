using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpParseTreeLib
{
    public class BaseElement : ITreeElement
    {
        private String _name = String.Empty;
        private Object _object = null;


        public BaseElement(String name, object o)
        {
            _name = name;
            _object = o;
        }

        public Object ValueObject
        {
            get { return _object; }
            protected set
            {
                _object = value;
            }
        }

        public virtual IEnumerable<ITreeElement> GetChildrens()
        {
            throw new NotImplementedException();
        }

        public virtual void Visit(IVisitor visitor)
        {
            throw new NotImplementedException();
        }
    
        public string Name
        {
            get
            {
                return _name;
            }
            protected set
            {
                _name = value;
            }
        }
    }
}
