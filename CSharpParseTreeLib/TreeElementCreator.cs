using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpParseTreeLib
{
    public class TreeElementCreator
    {
        public static ITreeElement CreateFromObject(string name, Object someObject)
        {
            if (someObject == null)
            {
                return null;
            }
            
            Type objectType = someObject.GetType();

            if (objectType.Name.Equals("CompilerContext"))
            {
                return null;
            }
    
            if (objectType.IsClass && objectType.ToString().StartsWith("Mono.CSharp"))
            {
                return new MCSElement(name, someObject);
            }

            if (someObject is IDictionary)
            {
                return new DictionaryElement(name, someObject);
            }

            if ((someObject is IEnumerable) && !objectType.Name.Equals("string", StringComparison.InvariantCultureIgnoreCase))
            {
                return new EnumerableElement(name, someObject);
            }

            //if (objectType.ToString().StartsWith("System."))
            //{
                return new OtherElement(name, someObject);
            //}
            //else
            //{
            //    return null;
            //}

            //return new SimpleElement(name, someObject);
        }
    }
}
