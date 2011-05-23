using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpParseTreeLib.Tests
{
    [TestClass]
    public class TreeElementCreatorTest
    {
        [TestMethod]
        public void TestCreateElementInMonoNamespace()
        {
            Mono.CSharp.ClassA someClass = new Mono.CSharp.ClassA();
            ITreeElement treeElement = TreeElementCreator.CreateFromObject("someClassName", someClass);

            Assert.IsNotNull(treeElement);
            Assert.IsTrue(treeElement is MCSElement);
        }

        [TestMethod]
        public void TestCreateelementInSystemNamespace()
        {
            System.Int32 someClass = 10;
            ITreeElement treeElement = TreeElementCreator.CreateFromObject("someClassName", someClass);

            Assert.IsNotNull(treeElement);
            Assert.IsTrue(treeElement is OtherElement);
        }

        [TestMethod]
        public void TestCreateEnumerableElement()
        {
            List<int> someIEnumerable = new List<int>();
            ITreeElement treeElement = TreeElementCreator.CreateFromObject("someIEnumerableName", someIEnumerable);

            Assert.IsNotNull(treeElement);
            Assert.IsTrue(treeElement is EnumerableElement);
        }

        [TestMethod]
        public void TestCreateDictionaryElement()
        {
            Dictionary<int, bool> someDict = new Dictionary<int, bool>();
            ITreeElement treeElement = TreeElementCreator.CreateFromObject("someDictName", someDict);

            Assert.IsNotNull(treeElement);
            Assert.IsTrue(treeElement is DictionaryElement);
        }
    }
}
