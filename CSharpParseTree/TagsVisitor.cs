using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CSharpParseTreeLib;

namespace CSharpParseTree
{
    public class TagsVisitor : IVisitor
    {
        private Dictionary<string, bool> _visitedTags = new Dictionary<string, bool>();
        private Dictionary<object, bool> _visited = new Dictionary<object, bool>();
        private StreamWriter _writer = null;
        private List<string> _tags = new List<string>();

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
        private bool WriteTag(BaseElement element)
        {
            if (!IsNeedProcessElement(element.ValueObject))
            { 
                return false;
            }

            string result = element.TypeName.Replace("[]", "");
            result = result.Replace("`", "");

            if (_visitedTags.ContainsKey(result))
            {
                return true;
            }

            _visitedTags.Add(result, true);

            if (_writer != null)
            {
                _writer.WriteLine(result);
            }

            _tags.Add(result);
            return true;
        }

        private void VisitChildrens(ITreeElement element)
        {
            IEnumerable<ITreeElement> childrens = element.GetChildrens();

            foreach (var children in childrens)
            {
                children.Visit(this);
            }
        }

        public TagsVisitor(Stream outputStream)
        {
            if (outputStream != null)
            {
                _writer = new StreamWriter(outputStream);
            }
        }

        public TagsVisitor() : this(null)
        {
        }

        public List<string> Tags
        {
            get
            {
                return _tags; 
            }
        }

        public void Close()
        {
            _writer.Close();
        }
        
        public void VisitMCSClassElement(MCSClassElement element)
        {
            if (!WriteTag(element))
            {
                return;
            }

            VisitChildrens(element);
        }

        public void VisitMCSOtherElement(MCSOtherElement element)
        {
            WriteTag(element);

        }

        public void VisitEnumerableElement(EnumerableElement element)
        {
            if (!WriteTag(element))
            {
                return;
            }

            VisitChildrens(element);
        }

        public void VisitDictionaryElement(DictionaryElement element)
        {
            if (!WriteTag(element))
            {
                return;
            }

            VisitChildrens(element);
        }

        public void VisitSystemElement(SystemElement element)
        {
            WriteTag(element);
        }
    }
}
