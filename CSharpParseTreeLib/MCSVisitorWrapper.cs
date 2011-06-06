using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpParseTreeLib
{
    /// <summary>
    /// Класс обвертка над посететилем, для того, чтобы не посещать уже посещенные элементы
    /// </summary>
    public class MCSVisitorWrapper : IVisitor
    {
        private IVisitor _visitor = null;
        private Dictionary<object, bool> _visited = new Dictionary<object, bool>();

        private bool IsNeedProcessElement(BaseElement element)
        {
            try
            {
                if (_visited.ContainsKey(element.ValueObject))
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            _visited.Add(element, true);

            return true;
        }

        public MCSVisitorWrapper(IVisitor visitor)
        {
            DbC.Helper.MustBeNotNull(visitor);

            _visitor = visitor;
        }

        public void VisitMCSClassElement(MCSClassElement element)
        {
            if (!IsNeedProcessElement(element))
            {
                return;
            }

            _visitor.VisitMCSClassElement(element);
        }

        public void VisitMCSOtherElement(MCSOtherElement element)
        {
            if (!IsNeedProcessElement(element))
            {
                return;
            }

            _visitor.VisitMCSOtherElement(element);
        }

        public void VisitEnumerableElement(EnumerableElement element)
        {
            if (!IsNeedProcessElement(element))
            {
                return;
            }

            _visitor.VisitEnumerableElement(element);
        }

        public void VisitDictionaryElement(DictionaryElement element)
        {
            if (!IsNeedProcessElement(element))
            {
                return;
            }

            _visitor.VisitDictionaryElement(element);
        }

        public void VisitSystemElement(SystemElement element)
        {
            if (!IsNeedProcessElement(element))
            {
                return;
            }

            _visitor.VisitSystemElement(element);
        }
    }
}
