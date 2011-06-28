using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpParseTree.Library
{
    public class MCSElementVisitor : IVisitor
    {
        public int MCSCLassElementsCount { get; private set; }
        public int EnumerableElementsCount { get; private set; }
        public int DictionaryElementsCount { get; private set; }
        public int SystemElementsCount { get; private set; }
        public int MCSOtherElementsCount { get; private set; }

        public void VisitMCSClassElement(MCSClassElement element)
        {
            MCSCLassElementsCount += 1;
            IEnumerable<ITreeElement> childrens = element.GetChilds();

            foreach (var child in childrens)
            {
                child.Visit(this);
            }
        }

        public void VisitEnumerableElement(EnumerableElement element)
        {
            EnumerableElementsCount += 1;
            IEnumerable<ITreeElement> childrens = element.GetChilds();

            foreach (var child in childrens)
            {
                child.Visit(this);
            }
        }

        public void VisitDictionaryElement(DictionaryElement element)
        {
            DictionaryElementsCount += 1;
            IEnumerable<ITreeElement> childrens = element.GetChilds();

            foreach (var child in childrens)
            {
                child.Visit(this);
            }
        }

        public void VisitSystemElement(SystemElement element)
        {
            SystemElementsCount += 1;
        }


        public void VisitMCSOtherElement(MCSOtherElement element)
        {
            MCSOtherElementsCount += 1;
        }
    }
}
