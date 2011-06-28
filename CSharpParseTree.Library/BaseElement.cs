using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CSharpParseTree.Library
{
    public class BaseElement : ITreeElement
    {
        private String _name = String.Empty;
        private Object _object = null;


        public BaseElement(String name, object o)
        {
            _name = name;
            _object = o;

            if (o == null)
            {
                TypeName = string.Empty;
            }
            else
            {
                TypeName = o.GetType().Name;
                TypeNamespace = o.GetType().Namespace;
            }
        }

        public Object ValueObject
        {
            get { return _object; }
            protected set
            {
                _object = value;
            }
        }

        public virtual IEnumerable<ITreeElement> GetChilds()
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

        public string TypeName
        {
            get;
            protected set;
        }

        public string TypeNamespace
        {
            get;
            protected set;
        }
    }
}
