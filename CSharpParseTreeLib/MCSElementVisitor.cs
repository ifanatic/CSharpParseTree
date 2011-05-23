using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpParseTreeLib
{
    public class MCSElementVisitor : IVisitor
    {
        public int SimpleElementsCount { get; private set; }
        public int EnumerableElementsCount { get; private set; }
        public int DictionaryElementsCount { get; private set; }
        public int SystemElementsCount { get; private set; }


        public void VisitMCSElement(MCSElement element)
        {
            SimpleElementsCount += 1;
            IEnumerable<ITreeElement> childrens = element.GetChildrens();

            foreach (var child in childrens)
            {
                child.Visit(this);
            }
        }

        public void VisitEnumerableElement(EnumerableElement element)
        {
            EnumerableElementsCount += 1;
            IEnumerable<ITreeElement> childrens = element.GetChildrens();

            foreach (var child in childrens)
            {
                child.Visit(this);
            }
        }

        public void VisitDictionaryElement(DictionaryElement element)
        {
            DictionaryElementsCount += 1;
            IEnumerable<ITreeElement> childrens = element.GetChildrens();

            foreach (var child in childrens)
            {
                child.Visit(this);
            }
        }

        public void VisitOtherElement(OtherElement element)
        {
            SystemElementsCount += 1;
        }
    }
}
