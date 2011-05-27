using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpParseTreeLib;

namespace CSharpParseTreeLib.Tests
{
    [TestClass]
    public class EnumerableElementTest
    {
        List<object> someList = new List<object>();

        [TestInitialize]
        public void Initialize()
        {
            someList.Add(10);
            someList.Add("asd");
            someList.Add(new Testdata.ClassA());
            someList.Add(new Mono.CSharp.ClassB());
        }
        
        [TestCleanup]
        public void Cleanup()
        {
            someList.Clear();
        }

        [TestMethod]
        public void TestValidElementType()
        {
            EnumerableElement validElement = new EnumerableElement("some_name", new List<int>());
        }


        [TestMethod]
        [ExpectedException(typeof(BadElementTypeException))]
        public void TestsInvalidElementType()
        {
            EnumerableElement invalidElement = new EnumerableElement("some_name", 10.0);
        }

        [TestMethod]
        public void TestGetChildrensCount()
        {
            EnumerableElement enumElement = new EnumerableElement("name", someList);
            IEnumerable<ITreeElement> childrens = enumElement.GetChildrens();

            Assert.AreEqual(4, childrens.Count());
        }

        [TestMethod]
        public void TestGetChildensElements()
        {
            EnumerableElement enumElement = new EnumerableElement("name", someList);
            IEnumerable<ITreeElement> childrens = enumElement.GetChildrens();

            Assert.AreEqual((childrens.First() as BaseElement).ValueObject, 10);
            Assert.IsTrue(childrens.Last() is MCSClassElement);
        }
    }
}
