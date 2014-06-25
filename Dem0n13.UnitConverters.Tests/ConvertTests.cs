using NUnit.Framework;

namespace Dem0n13.UnitConverters.Tests
{
    [TestFixture]
    public class ConvertTests
    {
        private ConvertersFactory _convertersFactory;

        [SetUp]
        public void SetUp()
        {
            _convertersFactory = new ConvertersFactory();
        }
        
        [Test]
        public void LengthConvertion()
        {
            var converter = _convertersFactory.GetConverter<Length, double>();
            var result = converter.Convert(1000, Length.Meters, Length.Kilometers);
            Assert.AreEqual(1.0, result);

            result = converter.Convert(1, Length.Miles, Length.Meters);
            Assert.AreEqual(1609.344, result);
        }

        [Test]
        public void WoodWorkConvertion()
        {
            var converter = _convertersFactory.GetConverter<WoodWork, int>();
            var result = converter.Convert(5, WoodWork.Bench, WoodWork.Board);
            Assert.AreEqual(10, result);

            result = converter.Convert(5, WoodWork.Bench, WoodWork.Table); // 5 лавок = 2 столам
            Assert.AreEqual(2, result);
        }
    }
}