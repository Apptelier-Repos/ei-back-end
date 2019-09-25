using System;
using System.Drawing;
using ei_core.Entities.UserAccountAggregate;
using ei_core.Entities.WireColorAggregate;
using Shouldly;
using Xunit;

namespace ei_unit_tests.core.Entities.WireColorTests
{
    public class WireColorCreation
    {
        private const int TestId = 21;
        private const string TestCode = "LA-B";
        private const string TestName = "Lavender-Black";
        private const string TestTranslatedName = "Lavanda-Negro";

        [Fact]
        public void Succeeds()
        {
            var rgbColors = new RgbColors(Color.Lavender, Color.Black);
            var wireColor = new WireColor(TestId, TestCode, TestName, TestTranslatedName, rgbColors);

            wireColor.ShouldNotBeNull();
        }
    }
}