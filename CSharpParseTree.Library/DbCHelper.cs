using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpParseTree.Library
{
    namespace DbC
    {
        public class Helper
        {
            public static void MustBeNotNull(object value)
            {
                MustBeNotNull(value, string.Empty);
            }
            
            public static void MustBeNotNull(object value, string argumentName)
            {
                if (value == null)
                {
                    throw new ArgumentNullException(argumentName);
                }
            }
        }
    }
}
