using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpParseTreeLib
{
    public class BadElementTypeException : ApplicationException
    {
        public BadElementTypeException(string expectedTypeName, Type type)
        {
            ExpectedTypeName = expectedTypeName;
            RealType = type;
        }

        public string ExpectedTypeName
        {
            get;
            private set;
        }

        public Type RealType
        {
            get;
            private set;
        }

        public override string Message
        {
            get
            {
                return "Ошибка преобразования типа элемента: ожидаемый тип \"" + ExpectedTypeName + "\""; 
            }
        }
    }
}
