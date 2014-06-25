using System;

namespace Dem0n13.UnitConverters
{
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
    public class UiKeyAttribute : Attribute
    {
        public readonly string Value;

        public UiKeyAttribute(string value)
        {
            Value = value;
        }
    }
}