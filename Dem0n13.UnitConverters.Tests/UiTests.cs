using System;
using System.Linq;
using NUnit.Framework;

namespace Dem0n13.UnitConverters.Tests
{
    [TestFixture]
    public class UiTests
    {
        private ConvertersFactory _convertersFactory;

        [SetUp]
        public void SetUp()
        {
            _convertersFactory = new ConvertersFactory();
        }

        [Test]
        public void GetAllDomains()
        {
            var domains = _convertersFactory.GetRegisteredConverters().Select(pair => pair.Key).ToArray();
            Assert.AreEqual(2, domains.Length);
            Assert.Contains(typeof(Length), domains);

            var uiStrings = domains.Select(type => type.ToUiKey()).ToArray();
            Assert.Contains("Длина", uiStrings);
        }

        [Test]
        public void GetAllUnits()
        {
            var units = Enum.GetValues(typeof(WoodWork)).Cast<WoodWork>().ToArray();
            Assert.AreEqual(3, units.Length);

            var uiStrings = units.Select(w => w.ToUiKey()).ToArray();
            Assert.Contains("Столы", uiStrings);
        }
    }
}