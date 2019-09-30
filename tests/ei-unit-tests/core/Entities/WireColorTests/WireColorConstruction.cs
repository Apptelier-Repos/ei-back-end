using System;
using System.Drawing;
using ei_core.Entities.WireColorAggregate;
using Shouldly;
using Xunit;

namespace ei_unit_tests.core.Entities.WireColorTests
{
    public class WireColorConstruction
    {
        private const int Id = 21;
        private const string LavenderBlackCode = "LA-B";
        private const string LavenderBlackName = "Lavender-Black";
        private const string LavenderBlackTranslatedName = "Lavanda-Negro";
        private readonly Color _lavenderColor = Color.Lavender;
        private readonly Color _blackColor = Color.Black;
        private const string BlueCode = "L";
        private const string BlueName = "Blue";
        private const string BlueNameTranslatedName = "Azul";
        private readonly Color _blueColor = Color.Blue;

        [Fact]
        public void SucceedsWithOneColor()
        {
            var wireColor = new WireColor(Id, BlueCode, BlueName, BlueNameTranslatedName, _blueColor);

            wireColor.ShouldNotBeNull();
            wireColor.Id.ShouldBe(Id);
            wireColor.Code.ShouldBe(BlueCode);
            wireColor.Name.ShouldBe(BlueName);
            wireColor.TranslatedName.ShouldBe(BlueNameTranslatedName);
            wireColor.BaseColor.ShouldBe(_blueColor);
            wireColor.StripeColor.ShouldBeNull();
        }

        [Fact]
        public void SucceedsWithTwoColors()
        {
            var wireColor = new WireColor(Id, LavenderBlackCode, LavenderBlackName, LavenderBlackTranslatedName,
                _lavenderColor, _blackColor);

            wireColor.ShouldNotBeNull();
            wireColor.Id.ShouldBe(Id);
            wireColor.Code.ShouldBe(LavenderBlackCode);
            wireColor.Name.ShouldBe(LavenderBlackName);
            wireColor.TranslatedName.ShouldBe(LavenderBlackTranslatedName);
            wireColor.BaseColor.ShouldBe(_lavenderColor);
            wireColor.StripeColor.ShouldBe(_blackColor);
        }

        [Fact]
        public void ThrowsAnExceptionWhenCodeIsEmpty()
        {
            Exception ex = Assert.Throws<ArgumentException>(() => new WireColor(Id, string.Empty, BlueName,
                BlueNameTranslatedName, _blueColor));
            ex.Message.ShouldContain(nameof(WireColor.Code));
        }

        [Fact]
        public void ThrowsAnExceptionWhenCodeIsNull()
        {
            Exception ex = Assert.Throws<ArgumentNullException>(() => new WireColor(Id, null, BlueName,
                BlueNameTranslatedName, _blueColor));
            ex.Message.ShouldContain(nameof(WireColor.Code));
        }

        [Fact]
        public void ThrowsAnExceptionWhenNameIsEmpty()
        {
            Exception ex = Assert.Throws<ArgumentException>(() => new WireColor(Id, BlueCode, string.Empty,
                BlueNameTranslatedName, _blueColor));
            ex.Message.ShouldContain(nameof(WireColor.Name));
        }

        [Fact]
        public void ThrowsAnExceptionWhenTranslatedNameIsEmpty()
        {
            Exception ex =
                Assert.Throws<ArgumentException>(() => new WireColor(Id, BlueCode, BlueName, string.Empty, _blueColor));
            ex.Message.ShouldContain(nameof(WireColor.TranslatedName));
        }
    }
}