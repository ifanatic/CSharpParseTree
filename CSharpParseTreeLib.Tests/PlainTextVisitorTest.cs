using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpParseTree.Library.Tests
{
    [TestClass]
    public class PlainTextVisitorTest
    {
        MemoryStream _stream;
        StreamWriter _writer;

        [TestInitialize]
        public void Initialize()
        {
            _stream = new MemoryStream();
            _writer = new StreamWriter(_stream);
            _writer.AutoFlush = true;
        }

        [TestCleanup]
        public void Cleanup()
        {
            _writer.Close();
        }

        
        [TestMethod]
        public void VisitEnumerable()
        {
            List<object> list = new List<object>();
            list.Add("first");
            list.Add(2);
            list.Add(3.0);

            PlainTextVisitor visitor = new PlainTextVisitor(_writer);

            EnumerableElement elem = new EnumerableElement("elem_root", list);
            elem.Visit(visitor);

            _stream.Seek(0, SeekOrigin.Begin);
            StreamReader reader = new StreamReader(_stream);
            string actual = reader.ReadToEnd();

            string expected = "Name: elem_root\r\n" + 
                              "  Class: String, Name: elem_root, Value: first\r\n" +
                              "  Class: Int32, Name: elem_root, Value: 2\r\n" + 
                              "  Class: Double, Name: elem_root, Value: 3\r\n";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void VisitDictionary()
        {
            Dictionary<object, object> dict = new Dictionary<object, object>();
            dict.Add("first", 1);
            dict.Add(2, "second");

            PlainTextVisitor visitor = new PlainTextVisitor(_writer);

            DictionaryElement elem = new DictionaryElement("dict", dict);
            elem.Visit(visitor);

            _stream.Seek(0, SeekOrigin.Begin);
            StreamReader reader = new StreamReader(_stream);
            string actual = reader.ReadToEnd();

            string expected = "Name: dict\r\n" +
                              "  Class: Int32, Name: first, Value: 1\r\n" +
                              "  Class: String, Name: 2, Value: second\r\n";


            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void VisitMCSClass()
        {
            Mono.CSharp.ClassC mcsclass = new Mono.CSharp.ClassC();
            mcsclass.SetValue(5);

            MCSClassElement elem = new MCSClassElement("mcsclass", mcsclass);

            PlainTextVisitor visitor = new PlainTextVisitor(_writer);
            elem.Visit(visitor);

            _stream.Seek(0, SeekOrigin.Begin);
            StreamReader reader = new StreamReader(_stream);
            string actual = reader.ReadToEnd();

            string expected = "Class: ClassC, Name: mcsclass\r\n" + 
                              "  Class: Int32, Name: _value, Value: 5\r\n";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void VisitMCSOtherElement()
        {
            Mono.CSharp.ClassD mcsother = new Mono.CSharp.ClassD();
            mcsother.SetValue(5);

            MCSOtherElement elem = new MCSOtherElement("mcsother", mcsother);

            PlainTextVisitor visitor = new PlainTextVisitor(_writer);
            elem.Visit(visitor);

            _stream.Seek(0, SeekOrigin.Begin);
            StreamReader reader = new StreamReader(_stream);
            string actual = reader.ReadToEnd();

            string expected = "Class: ClassD, Name: mcsother, Value: 5\r\n";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void VisitSystem()
        {
            System.String str = "some string";

            SystemElement elem = new SystemElement("str", str);

            PlainTextVisitor visitor = new PlainTextVisitor(_writer);
            elem.Visit(visitor);

            _stream.Seek(0, SeekOrigin.Begin);
            StreamReader reader = new StreamReader(_stream);
            string actual = reader.ReadToEnd();

            string expected = "Class: String, Name: str, Value: some string\r\n";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void VisitNested()
        {
            Mono.CSharp.ClassE typeWithNestedType = new Mono.CSharp.ClassE();
            typeWithNestedType.SetValue(5);

            MCSClassElement elem = new MCSClassElement("type", typeWithNestedType);

            PlainTextVisitor visitor = new PlainTextVisitor(_writer);
            elem.Visit(visitor);

            _stream.Seek(0, SeekOrigin.Begin);
            StreamReader reader = new StreamReader(_stream);
            string actual = reader.ReadToEnd();

            string expected = "Class: ClassE, Name: type\r\n" +
                              "  Class: ClassC, Name: _classC\r\n" +
                              "    Class: Int32, Name: _value, Value: 5\r\n";

            Assert.AreEqual(expected, actual);
        }
    }
}
