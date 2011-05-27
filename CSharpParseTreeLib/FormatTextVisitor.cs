using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CSharpParseTreeLib
{
    public class FormatTextVisitor : IVisitor
    {
        private IFormatBuilder _builder = null;
        private IEnumerable<IStringProcessor> _processors = null;
        private Dictionary<object, bool> _visited = new Dictionary<object, bool>();

        private bool IsNeedProcessElement(object key)
        {
            try
            {
                if (_visited.ContainsKey(key))
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            _visited.Add(key, true);

            return true; 
        }

        private void PrepareString(ref string str)
        {
            if (_processors == null)
            {
                return;
            }

            foreach (var stringProcessor in _processors)
            {
                stringProcessor.Process(ref str);
            }
        }

        private void VisitChildrens(ITreeElement element)
        {
            IEnumerable<ITreeElement> childrens = element.GetChildrens();

            foreach (var children in childrens)
            {
                children.Visit(this);
            }
        }

        public FormatTextVisitor(IFormatBuilder builder, IEnumerable<IStringProcessor> processors)
        {
            DbC.Helper.MustBeNotNull(builder, "builder");
            
            _builder = builder;
            _processors = processors;
        }

        public void VisitMCSClassElement(MCSClassElement element)
        {
            if (!IsNeedProcessElement(element.ValueObject))
            {
                return;
            }

            _builder.WriteStartMCSClass(element.TypeName, element.Name);

            VisitChildrens(element);

            _builder.WriteEndMCSClass(element.TypeName);
        }

        public void VisitMCSOtherElement(MCSOtherElement element)
        {
            if (!IsNeedProcessElement(element.ValueObject))
            {
                return;
            }

            string value = element.ValueObject.ToString();

            PrepareString(ref value);

            _builder.WriteSingleElement(element.TypeName,
                                        element.Name, 
                                        value);
        }

        public void VisitEnumerableElement(EnumerableElement element)
        {
            if (!IsNeedProcessElement(element.ValueObject))
            {
                return;
            }
            _builder.WriteStartEnumerable(element.TypeName);

            VisitChildrens(element);

            _builder.WriteEndEnumerable(element.TypeName);
        }

        public void VisitDictionaryElement(DictionaryElement element)
        {
            if (!IsNeedProcessElement(element.ValueObject))
            {
                return;
            }

            _builder.WriteStartDictionary(element.Name);

            VisitChildrens(element);

            _builder.WriteEndDictionary(element.Name);
        }

        public void VisitSystemElement(SystemElement element)
        {
            if (!IsNeedProcessElement(element.ValueObject))
            {
                return;
            }

            string value = element.ValueObject.ToString();

            PrepareString(ref value);

            _builder.WriteSingleElement(element.TypeName,
                                        element.Name,
                                        element.ValueObject.ToString());
        }
    }
}
