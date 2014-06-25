using System;

namespace Dem0n13.UnitConverters
{
    public static class EnumExtensions
    {
        public static string ToUiKey(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attributes = (UiKeyAttribute[]) fieldInfo.GetCustomAttributes(typeof (UiKeyAttribute), false);
            return attributes.Length > 0 ? attributes[0].Value : value.ToString();
        }

        public static string ToUiKey(this Type value)
        {
            if (!value.IsEnum)
                throw new ArgumentException("value");

            var attributes = (UiKeyAttribute[]) value.GetCustomAttributes(typeof (UiKeyAttribute), false);
            return attributes.Length > 0 ? attributes[0].Value : value.ToString();
        }
    }
}