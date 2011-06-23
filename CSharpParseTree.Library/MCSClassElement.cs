using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CSharpParseTree.Library
{
    public class MCSClassElement : BaseElement
    {
        public MCSClassElement(String name, Object value)
            : base(name, value)
        {

        }

        public override IEnumerable<ITreeElement> GetChildrens()
        {
            List<Type> baseTypes = new List<Type>();
            Type typeInfo = ValueObject.GetType();

            while (typeInfo != null && !(typeInfo.ToString().StartsWith("System.")))
            {
                baseTypes.Add(typeInfo);
                typeInfo = typeInfo.BaseType;
            }

            Dictionary<string, bool> shown = new Dictionary<string, bool>();

            foreach (Type type in baseTypes)
            {
                FieldInfo[] fields = type.GetFields(BindingFlags.Instance |
                    BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                
                foreach (FieldInfo fi in fields)
                {
                    if (shown.ContainsKey(fi.Name))
                    {
                        continue;
                    }

                    shown.Add(fi.Name, true);

                    ITreeElement newElement = TreeElementCreator.CreateFromObject(fi.Name, fi.GetValue(ValueObject));

                    if (newElement != null)
                    {
                        yield return newElement;
                    }
                }
            }
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.VisitMCSClassElement(this);
        }
    }
}
