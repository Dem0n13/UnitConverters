using System;
using System.Collections.Generic;

namespace Dem0n13.UnitConverters
{
    public abstract class UnitConverter<TUnit, TValue>
    {
        private readonly Dictionary<TUnit, Converter<TValue, TValue>> _toBaseConverters =
            new Dictionary<TUnit, Converter<TValue, TValue>>();

        private readonly Dictionary<TUnit, Converter<TValue, TValue>> _fromBaseConverters =
            new Dictionary<TUnit, Converter<TValue, TValue>>();

        protected UnitConverter()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            _toBaseConverters[BaseUnit] = input => input;
            _fromBaseConverters[BaseUnit] = input => input;
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        public abstract TUnit BaseUnit { get; }

        public TValue Convert(TValue value, TUnit from, TUnit to)
        {
            Converter<TValue, TValue> toBaseConverter;
            if (!_toBaseConverters.TryGetValue(from, out toBaseConverter))
                throw new NotSupportedException();

            Converter<TValue, TValue> fromBaseConverter;
            if (!_fromBaseConverters.TryGetValue(to, out fromBaseConverter))
                throw new NotSupportedException();

            return fromBaseConverter(toBaseConverter(value));
        }

        public void Register(TUnit unit, Converter<TValue, TValue> toBaseConverter, Converter<TValue, TValue> fromBaseConverter)
        {
            _toBaseConverters[unit] = toBaseConverter;
            _fromBaseConverters[unit] = fromBaseConverter;
        }

        public void Register(TUnit newUnit, TUnit knownUnit, Converter<TValue, TValue> fromNewToKnownConverter, Converter<TValue, TValue> fromKnownToNewConverter)
        {
            Converter<TValue, TValue> fromKnownToBaseConverter;
            if (!_toBaseConverters.TryGetValue(knownUnit, out fromKnownToBaseConverter))
                throw new NotSupportedException();

            Converter<TValue, TValue> fromBaseToKnownConverter;
            if (!_toBaseConverters.TryGetValue(knownUnit, out fromBaseToKnownConverter))
                throw new NotSupportedException();

            _toBaseConverters[newUnit] = input => fromKnownToBaseConverter(fromNewToKnownConverter(input));
            _fromBaseConverters[newUnit] = input => fromBaseToKnownConverter(fromKnownToNewConverter(input));
        }
    }
}