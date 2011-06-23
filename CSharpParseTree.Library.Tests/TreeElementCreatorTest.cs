using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpParseTree.Library;

namespace CSharpParseTree.Library.Tests
{
    [TestClass]
    public class TreeElementCreatorTest
    {
        class CompilerContext { }
        class GenericTypeBuilder { }
        class TypeBuilder { }
        
        [TestMethod]
        public void CreateElementInMonoNamespace()
        {
            Mono.CSharp.ClassA someClass = new Mono.CSharp.ClassA();
            ITreeElement treeElement = TreeElementCreator.CreateFromObject("someClassName", someClass);

            Assert.IsNotNull(treeElement);
            Assert.IsTrue(treeElement is MCSClassElement);
        }

        [TestMethod]
        public void CreateElementInSystemNamespace()
        {
            System.Int32 someClass = 10;
            ITreeElement treeElement = TreeElementCreator.CreateFromObject("someClassName", someClass);

            Assert.IsNotNull(treeElement);
            Assert.IsTrue(treeElement is SystemElement);
        }

        [TestMethod]
        public void CreateEnumerableElement()
        {
            List<int> someIEnumerable = new List<int>();
            ITreeElement treeElement = TreeElementCreator.CreateFromObject("someIEnumerableName", someIEnumerable);

            Assert.IsNotNull(treeElement);
            Assert.IsTrue(treeElement is EnumerableElement);
        }

        [TestMethod]
        public void CreateDictionaryElement()
        {
            Dictionary<int, bool> someDict = new Dictionary<int, bool>();
            ITreeElement treeElement = TreeElementCreator.CreateFromObject("someDictName", someDict);

            Assert.IsNotNull(treeElement);
            Assert.IsTrue(treeElement is DictionaryElement);
        }
        
        [TestMethod]
        public void NotCreateOtherElements()
        {
            CompilerContext cc = new CompilerContext();
            GenericTypeBuilder gtb = new GenericTypeBuilder();
            TypeBuilder tb = new TypeBuilder();
            Testdata.ClassA classa = new Testdata.ClassA();
            System.Reflection.ReflectionClass rc = new System.Reflection.ReflectionClass();

            Assert.IsNull(TreeElementCreator.CreateFromObject("cc", cc));
            Assert.IsNull(TreeElementCreator.CreateFromObject("gtb", gtb));
            Assert.IsNull(TreeElementCreator.CreateFromObject("tb", tb));
            Assert.IsNull(TreeElementCreator.CreateFromObject("rc", rc));
        }

    }
}
