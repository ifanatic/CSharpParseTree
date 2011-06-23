using System;
using System.Collections.Generic;

namespace CSharpParseTree.Library
{
    public interface IVisitor
    {
        void VisitMCSClassElement(MCSClassElement element);
        void VisitMCSOtherElement(MCSOtherElement element);
        void VisitEnumerableElement(EnumerableElement element);
        void VisitDictionaryElement(DictionaryElement element);
        void VisitSystemElement(SystemElement element);
    }
  
    public interface ITreeElement
    {
        IEnumerable<ITreeElement> GetChildrens();
        void Visit(IVisitor visitor);
        String Name { get;}
    }

    public interface ICompiler
    {
        bool Compile(string fileName);
    }

    public interface IFormatBuilder
    {
        void WriteStartEnumerable(string tag);
        void WriteEndEnumerable(string tag);
        void WriteStartDictionary(string tag);
        void WriteEndDictionary(string tag);
        void WriteStartMCSClass(string tag, string name);
        void WriteEndMCSClass(string tag);
        void WriteSingleElement(string tag, string elementName, string value);
        void WriteStartDocument();
        void WriteEndDocument();
        void WriteStartCustomElement(string tag, string attributeName, string value);
        void WriteEndElement();
    }

    public interface IStringProcessor
    {
        void Process(ref string someString);
    }
}
