using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpParseTreeLib;

namespace CSharpParseTreeLib.Tests
{
    [TestClass]
    public class MCSCompilerTest
    {
        private string _dmcsAssemblyFullPath = string.Empty;
        private string _pathToTests = string.Empty;
        private string _pathToTestData = string.Empty;

        [TestInitialize()]
        public void Initialize()
        {
            string _pathToTests = System.Reflection.Assembly.GetExecutingAssembly().Location;
            _pathToTests = System.IO.Path.GetDirectoryName(_pathToTests);
            
            _dmcsAssemblyFullPath = System.IO.Path.Combine(_pathToTests, "dmcs.exe");
            _pathToTestData = System.IO.Path.Combine(_pathToTests, "Testdata");
        }
        
        [TestMethod]
        public void TestCreateCompilerFromValidAssembly()
        {
            MCSCompiler compiler = new MCSCompiler(_dmcsAssemblyFullPath);

            Assert.IsTrue(compiler.SuccessfulCreated);
        }

        [TestMethod]
        public void TestCreateCompilerFromInvalidAssembly()
        {
            bool isTestSuccessful = false;

            try
            {
                MCSCompiler compiler =
                    new MCSCompiler(_pathToTests + @"\ParseTree.exe");
            }
            catch
            {
                isTestSuccessful = true;
            }
            Assert.IsTrue(isTestSuccessful);
        }

        [TestMethod]
        public void TestSuccessfulCompileFile()
        {
            MCSCompiler compiler = new MCSCompiler(_dmcsAssemblyFullPath);

            bool compileResult = compiler.Compile(_pathToTestData + @"\FileWithValidSouceCode.cs");

            Assert.IsTrue(compileResult);
        }

        

        [TestMethod]
        public void TestsunSuccessfulCompileFile()
        {
            MCSCompiler compiler = new MCSCompiler(_dmcsAssemblyFullPath);
            bool compileResult = compiler.Compile(_pathToTestData + @"\FileWithInvalidSouceCode.cs");
            Assert.IsFalse(compileResult);
        }
    }
}
