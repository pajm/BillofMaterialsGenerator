using BillofMaterialsGenerator.Core.Models;
using BillofMaterialsGenerator.Core.Services;
using BillOfMarterialsGenerator.Core;

var widgets = new List<IWidget>
{
    new Rectangle(10,10,30, 40),
    new Square(15,30,35),
    new Ellipse(100,150,300,200),
    new Circle(1,1,300),
    new TextBox(5,5, 200 ,100,"sample text"),
};

var generator = new BillOfMaterialsGenerator(new WidgetValidator(), new Logger());

var bom = generator.GenerateBOM(widgets);

Console.Write(bom);