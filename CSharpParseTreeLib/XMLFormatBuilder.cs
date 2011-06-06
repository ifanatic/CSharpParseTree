using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace CSharpParseTreeLib
{
    public class XMLFormatBuilder : IFormatBuilder
    {
        private TextWriter _writer = null;
        private XmlTextWriter _xmlWriter = null;

        private string GetPreparedTag(string tag)
        {
            string result = tag.Replace("[]", "");
            result = result.Replace("`", "");

            return result;
        }

        public XMLFormatBuilder(TextWriter writer)
        {
            DbC.Helper.MustBeNotNull(writer, "writer");

            _writer = writer;
            _xmlWriter = new XmlTextWriter(_writer);
        }

        public XMLFormatBuilder(Stream stream)
        {
            DbC.Helper.MustBeNotNull(stream, "stream");

            _writer = new StreamWriter(stream);
            _xmlWriter = new XmlTextWriter(_writer);
        }

        public void Close()
        {
            _xmlWriter.Close();
            _writer.Close();
        }

        public void WriteStartEnumerable(string tag)
        {
            _xmlWriter.WriteStartElement(GetPreparedTag(tag) + "_arr");
        }

        public void WriteEndEnumerable(string tag)
        {
            _xmlWriter.WriteEndElement();
        }

        public void WriteStartDictionary(string tag)
        {
            _xmlWriter.WriteStartElement(GetPreparedTag(tag) + "_dict");
        }

        public void WriteEndDictionary(string tag)
        {
            _xmlWriter.WriteEndElement();
        }

        public void WriteStartMCSClass(string tag, string name)
        {
            _xmlWriter.WriteStartElement(GetPreparedTag(tag));
            _xmlWriter.WriteAttributeString("Name", name);
        }

        public void WriteEndMCSClass(string tag)
        {
            _xmlWriter.WriteEndElement();
        }

        public void WriteSingleElement(string tag, string elementName, string value)
        {
            _xmlWriter.WriteStartElement(GetPreparedTag(tag));
            _xmlWriter.WriteAttributeString("Name", elementName);
            _xmlWriter.WriteAttributeString("Value", value);
            _xmlWriter.WriteEndElement();
        }

        public void WriteStartDocument()
        {
            _xmlWriter.WriteStartDocument();
        }

        public void WriteEndDocument()
        {
            _xmlWriter.WriteEndDocument();

            Close();
        }

        public void WriteStartCustomElement(string tag, string attributeName, string value)
        {
            _xmlWriter.WriteStartElement(tag);
            if (attributeName != null)
            {
                _xmlWriter.WriteAttributeString(attributeName, value);
            }
        }

        public void WriteEndElement()
        {
            _xmlWriter.WriteEndElement();
        }
    }
}
