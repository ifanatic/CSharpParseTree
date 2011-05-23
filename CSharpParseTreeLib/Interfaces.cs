using System;
using System.Collections.Generic;

namespace CSharpParseTreeLib
{
    public interface IVisitor
    {
        void VisitMCSElement(MCSElement element);
        void VisitEnumerableElement(EnumerableElement element);
        void VisitDictionaryElement(DictionaryElement element);
        void VisitOtherElement(OtherElement element);
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
        bool Compile(string fileName, Mono.CSharp.ReportPrinter reportPrinter);
    }

    public interface IFormatBuilder
    {
        void WriteStartElement(string name);
        void WriteEndElement(string name);
        void WriteStartArray(string arrayName);
        void WriteEndArray(string arrayName);
        void WriteSimpleElement(string name, object value);
        
    }
}
