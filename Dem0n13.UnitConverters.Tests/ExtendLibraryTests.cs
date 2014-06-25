using System.Reflection;
using NUnit.Framework;

namespace Dem0n13.UnitConverters.Tests
{
    [TestFixture]
    public class ExtendLibraryTests
    {
        private ConvertersFactory _convertersFactory;

        [SetUp]
        public void SetUp()
        {
            _convertersFactory = new ConvertersFactory();
            _convertersFactory.RegisterAssembly(Assembly.GetExecutingAssembly());
        }

        [Test]
        public void TempretureConversion()
        {
            var converter = _convertersFactory.GetConverter<Temperature, float>();
            var result = converter.Convert(500.0f, Temperature.Kelvins, Temperature.Celsium);
            Assert.AreEqual(226.5f, result);

            result = converter.Convert(90, Temperature.Celsium, Temperature.Fahrenheit);
            Assert.AreEqual(194, result);
        }
    }

    public enum Temperature
    {
        Fahrenheit,
        Celsium,
        Kelvins
    }

    public class TemperatureConverter : UnitConverter<Temperature, float>
    {
        public override Temperature BaseUnit
        {
            get { return Temperature.Fahrenheit; }
        }

        public TemperatureConverter()
        {
            Register(Temperature.Celsium, input => input*9f/5f + 32, input => (input - 32f)*5f/9f);
            Register(Temperature.Kelvins, Temperature.Celsium, input => input - 273.5f, input => input + 273.5f);
        }
    }
}