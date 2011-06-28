using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CSharpParseTree.Library;


namespace CSharpParseTree.Library.Tests
{
    [TestClass]
    public class DictionaryElementTest
    {
        class NotADictionaryElement
        {
            public int SomeProp { get; set; }
        }
        
        [TestMethod]
        public void IsDictionaryElement()
        {
            Dictionary<object, string> dict = new Dictionary<object,string>();

            DictionaryElement elem = new DictionaryElement("some_name", dict);
            Assert.IsNotNull(elem);
        }

        [TestMethod]
        [ExpectedException(typeof(BadElementTypeException))]
        public void IsNotDictionaryElement()
        {
            NotADictionaryElement notADict = new NotADictionaryElement();

            DictionaryElement elem = new DictionaryElement("some_element", notADict);
            Assert.IsNull(elem);
        }

        [TestMethod]
        public void ChildsCount()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("asd", "fgh");
            dict.Add("123", 123);

            DictionaryElement elem = new DictionaryElement("some_name", dict);

            Assert.AreEqual(2, elem.GetChilds().Count());
        }

        [TestMethod]
        public void ChildsElements()
        {
            Dictionary<object, object> dict = new Dictionary<object, object>();
            dict.Add("ads", 123);
            dict.Add("fgh", 456);
            dict.Add("jkl", 789);
            dict.Add(10, "11");
            dict.Add(1, 1);

            DictionaryElement elem = new DictionaryElement("dict", dict);

            IEnumerable<ITreeElement> childs = elem.GetChilds();

            Assert.AreEqual("ads", childs.ElementAt(0).Name);
            Assert.AreEqual("1", childs.Last().Name);
            Assert.AreEqual("jkl", childs.ElementAt(2).Name);
        }

    }
}
