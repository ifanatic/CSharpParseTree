using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CSharpParseTreeLib
{
    public class PlainTextVisitor : IVisitor
    {
        private int depth = -1;
        private FileStream _fileStream = null;
        private StreamWriter _writer = null;
        private Dictionary<object, bool> _visited = new Dictionary<object, bool>();


        public PlainTextVisitor(string outputFileName)
        {
            _fileStream = new FileStream(outputFileName, FileMode.Create, FileAccess.Write);
            _writer = new StreamWriter(_fileStream);
        }

        public void Close()
        {
            _writer.Close();
            _fileStream.Close();
        }

        public void VisitMCSElement(MCSElement element)
        {
            if (_visited.ContainsKey(element.ValueObject))
            {
                return;
            }

            _visited.Add(element.ValueObject, true);


            if (_visited.ContainsKey(element.ValueObject))

            depth++;
            IEnumerable<ITreeElement> childrens = element.GetChildrens();

            for (int i = 0; i < depth; ++i)
            {
                _writer.Write("  ");
            }
            string className = element.ValueObject.GetType().Name;

            _writer.WriteLine("Class: {0}, Name: {1}", className, element.Name);

            foreach (var child in childrens)
            {
                child.Visit(this);
            }

            depth--;
        }

        public void VisitEnumerableElement(EnumerableElement element)
        {
            if (_visited.ContainsKey(element.ValueObject))
            {
                return;
            }

            _visited.Add(element.ValueObject, true);

            depth++;
            IEnumerable<ITreeElement> childrens = element.GetChildrens();

            for (int i = 0; i < depth; ++i)
            {
                _writer.Write("  ");
            }
            _writer.WriteLine("Name: {0}", element.Name);

            foreach (var child in childrens)
            {      
                child.Visit(this);
            }
            depth--;
        }

        public void VisitDictionaryElement(DictionaryElement element)
        {
            if (_visited.ContainsKey(element.ValueObject))
            {
                return;
            }

            _visited.Add(element.ValueObject, true);

            depth++;
            IEnumerable<ITreeElement> childrens = element.GetChildrens();

            for (int i = 0; i < depth; ++i)
            {
                _writer.Write("  ");
            }
            _writer.WriteLine("Name: {0}", element.Name);

            foreach (var child in childrens)
            {      
                child.Visit(this);
            }
            depth--;
        }

        public void VisitOtherElement(OtherElement element)
        {
            if (_visited.ContainsKey(element.ValueObject))
            {
                return;
            }

            _visited.Add(element.ValueObject, true);
            
            depth++;
            for (int i = 0; i < depth; ++i)
            {
                _writer.Write("  ");
            }

            string className = element.ValueObject.GetType().Name;

            _writer.WriteLine("Class: {0}, Name: {1}, Value: {2}", className, element.Name, element.ValueObject.ToString());
            depth--;
        }
    }
}
