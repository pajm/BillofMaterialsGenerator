using BillofMaterialsGenerator.Core.Interfaces;
using BillofMaterialsGenerator.Core.Models;
using BillofMaterialsGenerator.Core.Services;

namespace BillOfMarterialsGenerator.Core.Tests
{
    public class BillOfMaterialsGeneratorTests
    {
        private readonly ILogger _logger;
        private readonly IWidgetValidator _widgetValidator;
        private readonly IBillOfMaterialsGenerator _billOfMaterialsGenerator;

        public BillOfMaterialsGeneratorTests()
        {
            _logger = new Logger();
            _widgetValidator = new WidgetValidator();
            _billOfMaterialsGenerator = new BillOfMaterialsGenerator(_widgetValidator, _logger);
        }

        [Fact]
        public void GenerateBOM_NullWidgets_ReturnsAbort()
        {
            var result = _billOfMaterialsGenerator.GenerateBOM(null);
            Assert.Equal("+++++Abort+++++", result);
        }

        [Fact]
        public void GenerateBOM_EmptyList_ReturnsAbort()
        {
            var result = _billOfMaterialsGenerator.GenerateBOM(new List<IWidget>());
            Assert.Equal("+++++Abort+++++", result);
        }

        [Fact]
        public void GenerateBOM_ValidWidgets_ReturnsCorrectBOM()
        {
            var widgets = new List<IWidget>
            {
                new Rectangle(10, 20, 100, 50),
                new Square(5, 5, 30),
                new Circle(100, 100, 40),
                new Ellipse(200, 150, 60, 30),
                new TextBox(50, 80, 200, 40, "Hello World")
            };

            var result = _billOfMaterialsGenerator.GenerateBOM(widgets);

            Assert.Contains("Bill of Materials", result);
            Assert.Contains("Rectangle (10,20) width=100 height=50", result);
            Assert.Contains("Square (5,5) size=30", result);
            Assert.Contains("Circle (100,100) size=40", result);
            Assert.Contains("Ellipse (200,150) diameterH = 60 diameterV = 30", result);
            Assert.Contains("Textbox (50,80) width=200 height=40 text=\"Hello World\"", result);
            Assert.DoesNotContain("+++++Abort+++++", result);
        }

        [Fact]
        public void GenerateBOM_InvalidWidget_ReturnsAbort()
        {
            var widgets = new List<IWidget>
            {
                new Rectangle(10, 20, 100, 50),
                new FakeInvalidWidget()   // This should fail validation
            };

            var result = _billOfMaterialsGenerator.GenerateBOM(widgets);
            Assert.Equal("+++++Abort+++++", result);
        }

        [Fact]
        public void GenerateBOM_TextBoxWithNullText_HandlesCorrectly()
        {
            var widgets = new List<IWidget>
            {
                new TextBox(10, 10, 100, 30, null)
            };

            var result = _billOfMaterialsGenerator.GenerateBOM(widgets);
            Assert.Contains("text=\"\"", result);
        }
    }

    // Helper for testing invalid widget case
    public class FakeInvalidWidget : IWidget { }
}