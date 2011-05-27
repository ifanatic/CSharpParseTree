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

        public void VisitMCSClassElement(MCSClassElement element)
        {
            try
            {
                if (_visited.ContainsKey(element.ValueObject))
                {
                    return;
                }
            }
            catch
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
            
            _writer.WriteLine("Class: {0}, Name: {1}", element.TypeName, element.Name);

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

        public void VisitSystemElement(SystemElement element)
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
            
            _writer.WriteLine("Class: {0}, Name: {1}, Value: {2}", element.TypeName, element.Name, element.ValueObject.ToString());
            depth--;
        }

        public void VisitMCSOtherElement(MCSOtherElement element)
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

            string valueString = element.ValueObject.ToString();

            if (element.TypeName.Equals("Location"))
            {
                valueString = valueString.Replace(@"C:\Users\fanatic\documents\visual studio 2010\Projects\", "");
            }

            _writer.WriteLine("Class: {0}, Name: {1}, Value: {2}", element.TypeName, element.Name, valueString);
            depth--;
        }
    }
}
