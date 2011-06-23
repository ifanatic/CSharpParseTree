using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpParseTree.Library.Tests.Testdata
{
    class ClassA
    {
        public string aStringValue = "class A sval";
    }
    
    class ClassB
    {
        private int aIntegerValue = 1123;
        public string bStringValue = "string_Val1";
        protected ClassA cClassAValue = new ClassA();
    }
}
