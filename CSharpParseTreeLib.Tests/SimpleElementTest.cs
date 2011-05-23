using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpParseTreeLib;

using System.IO;

namespace CSharpParseTreeLib.Tests
{
    [TestClass]
    public class SimpleElementTest
    {               
        [TestMethod]
        public void TestGetChildrensCountWithSystemNamespace()
        {
            Testdata.ClassB someClass = new Testdata.ClassB();
            MCSElement simpleElement = new MCSElement("some_name", someClass);
            IEnumerable<ITreeElement> childrens = simpleElement.GetChildrens();

            Assert.AreEqual(3, childrens.Count());
        }

        [TestMethod]
        public void TestGetChildrensCountWithMonoNamespace()
        {
            Mono.CSharp.ClassB someClass = new Mono.CSharp.ClassB();
            MCSElement simpleElement = new MCSElement("someClass", someClass);
            IEnumerable<ITreeElement> childrens = simpleElement.GetChildrens();

            Assert.AreEqual(3, childrens.Count());
        }

        [TestMethod]
        public void TestGetChildrensGetElementsNames()
        {
            Mono.CSharp.ClassB someClass = new Mono.CSharp.ClassB();
            MCSElement simpleElement = new MCSElement("someClass", someClass);

            IEnumerable<ITreeElement> childrens = simpleElement.GetChildrens();

            Assert.AreEqual("aIntegerValue", childrens.First().Name);
            Assert.AreEqual("cClassAValue", childrens.Last().Name);
        }

        [TestMethod]
        public void TestGetChildrensGetElementsTypes()
        {
            Mono.CSharp.ClassB someClass = new Mono.CSharp.ClassB();
            MCSElement simpleElement = new MCSElement("someClass", someClass);

            IEnumerable<ITreeElement> childrens = simpleElement.GetChildrens();

            Assert.IsTrue(childrens.First() is OtherElement);
            Assert.IsTrue(childrens.Last() is MCSElement);
        }
    }
}
