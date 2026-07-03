using BillofMaterialsGenerator.Core.Interfaces;
using BillofMaterialsGenerator.Core.Models;
using BillofMaterialsGenerator.Core.Services;
using BillOfMarterialsGenerator.Core;
using Xunit;

namespace BillOfMarterialsGenerator.Core.Tests
{
    public class WidgetValidatorTests
    {
        private readonly IWidgetValidator _widgetValidator;

        public WidgetValidatorTests()
        {
            _widgetValidator = new WidgetValidator();
        }

        [Fact]
        public void IsValid_NullWidget_ReturnsFalse()
        {
            Assert.False(_widgetValidator.IsValid(null));
        }

        [Fact]
        public void IsValid_UnknownType_ReturnsFalse()
        {
            Assert.False(_widgetValidator.IsValid(new FakeInvalidWidget()));
        }

        #region Rectangle

        [Theory]
        [InlineData(0, 0, 10, 10, true)]
        [InlineData(500, 500, 100, 50, true)]
        [InlineData(-1, 0, 10, 10, false)]
        [InlineData(0, -1, 10, 10, false)]
        [InlineData(1001, 0, 10, 10, false)]
        [InlineData(0, 0, 0, 10, false)]
        [InlineData(0, 0, 10, 0, false)]
        public void IsValid_Rectangle_ReturnsExpected(int x, int y, int width, int height, bool expected)
        {
            var rect = new Rectangle(x, y, width, height);
            Assert.Equal(expected, _widgetValidator.IsValid(rect));
        }

        #endregion

        #region Square

        [Theory]
        [InlineData(0, 0, true)]
        [InlineData(1000, 1000, true)]
        [InlineData(-1, 0, false)]
        [InlineData(0, 1001, false)]
        public void IsValid_Square_ReturnsExpected(int x, int y, bool expected)
        {
            var square = new Square(x, y, 50); // size is not validated in current code
            Assert.Equal(expected, _widgetValidator.IsValid(square));
        }

        #endregion

        #region Circle

        [Theory]
        [InlineData(100, 100, 20, true)]
        [InlineData(100, 100, 0, false)]
        [InlineData(100, 100, -5, false)]
        public void IsValid_Circle_ReturnsExpected(int x, int y, int diameter, bool expected)
        {
            var circle = new Circle(x, y, diameter);
            Assert.Equal(expected, _widgetValidator.IsValid(circle));
        }

        #endregion

        #region Ellipse

        [Theory]
        [InlineData(200, 200, 30, 40, true)]
        [InlineData(200, 200, 0, 40, false)]
        [InlineData(200, 200, 30, 0, false)]
        [InlineData(200, 200, -10, 40, false)]
        public void IsValid_Ellipse_ReturnsExpected(int x, int y, int h, int v, bool expected)
        {
            var ellipse = new Ellipse(x, y, h, v);
            Assert.Equal(expected, _widgetValidator.IsValid(ellipse));
        }

        #endregion

        #region TextBox

        [Theory]
        [InlineData(50, 60, 100, 30, true)]
        [InlineData(50, 60, 0, 30, false)]
        [InlineData(50, 60, 100, 0, false)]
        public void IsValid_TextBox_ReturnsExpected(int x, int y, int width, int height, bool expected)
        {
            var textBox = new TextBox(x, y, width, height, "Test");
            Assert.Equal(expected, _widgetValidator.IsValid(textBox));
        }

        #endregion

        [Fact]
        public void IsValid_TextBoxWithNullText_StillValidIfPositionAndSizeOk()
        {
            var textBox = new TextBox(100, 100, 150, 50, null);
            Assert.True(_widgetValidator.IsValid(textBox));
        }
    }
}