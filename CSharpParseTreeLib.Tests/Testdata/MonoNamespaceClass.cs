using System;
using System.Collections.Generic;

namespace Mono
{
    namespace CSharp
    {
        class ClassA
        {
            public string aStringValue = "class A sval";
            private List<string> bListValue = new List<string>();

            public ClassA()
            {
                bListValue.Add("First");
                bListValue.Add("Second");
                bListValue.Add("last=/ ");
            }
        }

        class ClassB
        {
            private int aIntegerValue = 1123;
            public string bStringValue = "string_Val1";
            protected ClassA cClassAValue = new ClassA();
        }

        class ClassC
        {
            private int _value = 10;

            public void SetValue(int value) { _value = value; }
        }

        class ClassD
        {
            private int _value = 10;

            public void SetValue(int value) { _value = value; }

            public override string  ToString()
            {
                return _value.ToString();
            }
        }

        class ClassE
        {
            private ClassC _classC = new ClassC();

            public void SetValue(int value) { _classC.SetValue(value); }
        }
    }
}