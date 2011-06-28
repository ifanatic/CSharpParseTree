using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpParseTree.Library;

using System.IO;

namespace CSharpParseTree.Library.Tests
{
    [TestClass]
    public class SimpleElementTest
    {               
        [TestMethod]
        public void TestGetChildrensCountWithSystemNamespace()
        {
            Testdata.ClassB someClass = new Testdata.ClassB();
            MCSClassElement simpleElement = new MCSClassElement("some_name", someClass);
            IEnumerable<ITreeElement> childrens = simpleElement.GetChilds();

            Assert.AreEqual(3, childrens.Count());
        }

        [TestMethod]
        public void TestGetChildrensCountWithMonoNamespace()
        {
            Mono.CSharp.ClassB someClass = new Mono.CSharp.ClassB();
            MCSClassElement simpleElement = new MCSClassElement("someClass", someClass);
            IEnumerable<ITreeElement> childrens = simpleElement.GetChilds();

            Assert.AreEqual(3, childrens.Count());
        }

        [TestMethod]
        public void TestGetChildrensGetElementsNames()
        {
            Mono.CSharp.ClassB someClass = new Mono.CSharp.ClassB();
            MCSClassElement simpleElement = new MCSClassElement("someClass", someClass);

            IEnumerable<ITreeElement> childrens = simpleElement.GetChilds();

            Assert.AreEqual("aIntegerValue", childrens.First().Name);
            Assert.AreEqual("cClassAValue", childrens.Last().Name);
        }

        [TestMethod]
        public void TestGetChildrensGetElementsTypes()
        {
            Mono.CSharp.ClassB someClass = new Mono.CSharp.ClassB();
            MCSClassElement simpleElement = new MCSClassElement("someClass", someClass);

            IEnumerable<ITreeElement> childrens = simpleElement.GetChilds();

            Assert.IsTrue(childrens.First() is SystemElement);
            Assert.IsTrue(childrens.Last() is MCSClassElement);
        }
    }
}
