using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Dem0n13.UnitConverters
{
    public class ConvertersFactory
    {
        private readonly Dictionary<Type, Dictionary<Type, object>> _converters = new Dictionary<Type, Dictionary<Type, object>>();
        
        public ConvertersFactory()
        {
            RegisterAssembly(Assembly.GetExecutingAssembly());
        }
        
        public UnitConverter<TUnit, TValue> GetConverter<TUnit, TValue>()
        {
            return (UnitConverter<TUnit, TValue>) _converters[typeof (TUnit)][typeof (TValue)];
        }

        public IEnumerable<KeyValuePair<Type, Type>> GetRegisteredConverters()
        {
            return _converters.SelectMany(
                pairByUnitType => pairByUnitType.Value,
                (pairByUnitType, pairByValueType) => new KeyValuePair<Type, Type>(pairByUnitType.Key, pairByValueType.Key));
        }

        public void RegisterAssembly(Assembly assembly)
        {
            foreach (var type in typeof(UnitConverter<,>).GetDerivedTypes(assembly).Where(type => !type.IsAbstract))
            {
                var genericArgs = type.BaseType.GenericTypeArguments;
                Register(genericArgs[0], genericArgs[1], Activator.CreateInstance(type));
            }
        }

        public void Register<TUnit, TValue>(UnitConverter<TUnit, TValue> converter)
        {
            Register(typeof(TUnit), typeof(TValue), converter);
        }

        private void Register(Type unitType, Type valueType, object converter)
        {
            if (!_converters.ContainsKey(unitType)) _converters[unitType] = new Dictionary<Type, object>();
            _converters[unitType][valueType] = converter;
        }
    }
}