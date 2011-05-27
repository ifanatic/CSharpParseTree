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
    public class SomeTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Stream outputStream = File.OpenWrite("C:\\xml_output.xml");
            XMLFormatBuilder xmlBuilder = new XMLFormatBuilder(outputStream);
            FormatTextVisitor ftv = new FormatTextVisitor(xmlBuilder, null);
            xmlBuilder.WriteStartDocument();
            string _pathToTests = System.Reflection.Assembly.GetExecutingAssembly().Location;
            _pathToTests = System.IO.Path.GetDirectoryName(_pathToTests);

            string _dmcsAssemblyFullPath = System.IO.Path.Combine(_pathToTests, "dmcs.exe");

            MCSCompiler compiler = new MCSCompiler(_dmcsAssemblyFullPath);
            compiler.Compile(_pathToTests + @"\Testdata\FileWithValidSouceCode.cs");

            MCSClassElement se = new MCSClassElement("root", compiler.TreeRoot);
            se.Visit(ftv);
            xmlBuilder.WriteEndDocument();
            xmlBuilder.Close();
            outputStream.Close();
        }
    }
}
