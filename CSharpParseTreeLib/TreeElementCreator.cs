using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpParseTreeLib
{
    public class TreeElementCreator
    {
        private static bool CanAndNeedProcessObject(string name, Object someObject)
        {
            if (someObject == null)
            {
                return false;
            }

            Type objectType = someObject.GetType();

            if (objectType.Name.Equals("CompilerContext"))
            {
                return false;
            }

            if (objectType.Namespace.StartsWith("System.Reflection"))
            {
                return false;
            }

            /*if (objectType.Namespace.StartsWith("System.") &&
                !objectType.Namespace.StartsWith("System.Collections") &&
                (objectType.Namespace.Length > 7))
            {
                return false;
            }*/

            if (name.Equals("TypeBuilder") || name.Equals("GenericTypeBuilder"))
            {
                return false;
            }

            return true;
        }
        
        public static ITreeElement CreateFromObject(string name, Object someObject)
        {
            if (!CanAndNeedProcessObject(name, someObject))
            {
                return null;
            }
            
            Type objectType = someObject.GetType();
                
            if (objectType.ToString().StartsWith("Mono.CSharp"))
            {
                if (objectType.IsClass)
                {
                    return new MCSClassElement(name, someObject);
                }
                else
                {
                    return new MCSOtherElement(name, someObject);
                }
            }

            if (someObject is IDictionary)
            {
                return new DictionaryElement(name, someObject);
            }

            if ((someObject is IEnumerable) && !objectType.Name.Equals("string", StringComparison.InvariantCultureIgnoreCase))
            {
                return new EnumerableElement(name, someObject);
            }
      
            return new SystemElement(name, someObject);
        }
    }
}
