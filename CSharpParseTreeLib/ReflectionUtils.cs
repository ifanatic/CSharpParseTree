using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CSharpParseTreeLib
{
    class ReflectionUtils
    {
        public static Type ExtractTypeByName(Assembly assembly, string typeName)
        {
            Type result = null;

            Type[] types = assembly.GetTypes();

            foreach (Type type in types)
            {
                if (type.FullName.Equals("Mono.CSharp.Driver"))
                {
                    result = type;
                }
            }

            return result;
        }
    }
}
