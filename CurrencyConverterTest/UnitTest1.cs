using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using Xunit;
using CurrencyConverter;

namespace CurrencyConverterTest
{
    public class UnitTest1
    {
        [Fact]
        public void CurrencyConversionTest()
        {
            Assert.Equal(2.0946218808106896720052898078m, CurrencyConverter.Program.ConvertCurrency(2.36m, 1m, 1.126695m));
            Assert.Equal(28.403089656615144293708590169m, CurrencyConverter.Program.ConvertCurrency(39.53m, 0.809552722m, 1.126695m));
            Assert.Equal(-1m, CurrencyConverter.Program.ConvertCurrency(2.36m, 6.32m, 0));
        }
    }
}
