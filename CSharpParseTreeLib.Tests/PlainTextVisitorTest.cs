using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpParseTreeLib.Tests
{
    [TestClass]
    public class PlainTextVisitorTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            PlainTextVisitor ptv = new PlainTextVisitor("C:\\ptv_output.txt");
            Mono.CSharp.ClassB classB = new Mono.CSharp.ClassB();
            MCSClassElement se = new MCSClassElement("root", classB);
            se.Visit(ptv);
            ptv.Close();
        }

        [TestMethod]
        public void TestMethod2()
        {
            string _pathToTests = System.Reflection.Assembly.GetExecutingAssembly().Location;
            _pathToTests = System.IO.Path.GetDirectoryName(_pathToTests);
            
            string _dmcsAssemblyFullPath = System.IO.Path.Combine(_pathToTests, "dmcs.exe");          
            
            PlainTextVisitor ptv = new PlainTextVisitor("C:\\ptv_output1.txt");
            MCSCompiler compiler = new MCSCompiler(_dmcsAssemblyFullPath);
            Assert.IsTrue(compiler.Compile(_pathToTests + @"\Testdata\FileWithValidSouceCode.cs"));

            MCSClassElement se = new MCSClassElement("root", compiler.TreeRoot);
            se.Visit(ptv);
            ptv.Close();
        }

        [TestMethod]
        public void TestMethod3()
        {
            PlainTextVisitor ptv = new PlainTextVisitor("C:\\ptv_output2.txt");
            Mono.CSharp.MemberName mn = new Mono.CSharp.MemberName("SomeName", new Mono.CSharp.Location(10, 10));
            MCSClassElement se = new MCSClassElement("mn", mn);
            se.Visit(ptv);
            ptv.Close();
        }
    }
}
