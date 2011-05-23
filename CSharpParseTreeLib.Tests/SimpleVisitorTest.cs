using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CSharpParseTreeLib;

namespace CSharpParseTreeLib.Tests
{
    [TestClass]
    public class SimpleVisitorTest
    {
        [TestMethod]
        public void TestVisitSimpleElementsCount()
        {
            MCSElementVisitor sv = new MCSElementVisitor();
            Mono.CSharp.ClassB someClass = new Mono.CSharp.ClassB();

            MCSElement se = new MCSElement("name", someClass);
            se.Visit(sv);

            Assert.AreEqual(2, sv.SimpleElementsCount);
        }

        [TestMethod]
        public void TestVisitSystemElementsCount()
        {
            MCSElementVisitor sv = new MCSElementVisitor();
            Mono.CSharp.ClassB someClass = new Mono.CSharp.ClassB();

            MCSElement se = new MCSElement("name", someClass);
            se.Visit(sv);

            Assert.AreEqual(6, sv.SystemElementsCount);
        }

        [TestMethod]
        public void TestVisitDictionaryElementsCount()
        {
            MCSElementVisitor sv = new MCSElementVisitor();
            Mono.CSharp.ClassB someClass = new Mono.CSharp.ClassB();

            MCSElement se = new MCSElement("name", someClass);
            se.Visit(sv);

            Assert.AreEqual(0, sv.DictionaryElementsCount);
        }

        [TestMethod]
        public void TestVisitEnumerableElementsCount()
        {
            MCSElementVisitor sv = new MCSElementVisitor();
            Mono.CSharp.ClassB someClass = new Mono.CSharp.ClassB();

            MCSElement se = new MCSElement("name", someClass);
            se.Visit(sv);

            Assert.AreEqual(1, sv.EnumerableElementsCount);
        }
    }
}
