using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Dem0n13.UnitConverters
{
    public static class TypeExtensions
    {
        public static IEnumerable<Type> GetDerivedTypes(this Type baseType)
        {
            if (baseType == null)
                throw new ArgumentNullException("baseType");

            return GetDerivedTypesCore(baseType, baseType.Assembly);
        }

        public static IEnumerable<Type> GetDerivedTypes(this Type baseType, Assembly assembly)
        {
            if (baseType == null)
                throw new ArgumentNullException("baseType");
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            return GetDerivedTypesCore(baseType, assembly);
        }

        private static IEnumerable<Type> GetDerivedTypesCore(Type baseType, Assembly assembly)
        {
            return GetDerivedTypesCore(baseType, assembly.DefinedTypes);
        }

        private static IEnumerable<Type> GetDerivedTypesCore(Type baseType, IEnumerable<Type> definedTypes)
        {
            var derivedTypes = definedTypes.Where(type => type.BaseType != null && type.BaseType.GUID == baseType.GUID).ToList();
            var result = derivedTypes.ToList();

            foreach (var derivedType in derivedTypes)
            {
                result.AddRange(GetDerivedTypesCore(derivedType, definedTypes));
            }
            return result;
        }
    }
}