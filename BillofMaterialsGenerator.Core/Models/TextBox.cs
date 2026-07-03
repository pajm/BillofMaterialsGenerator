

namespace BillofMaterialsGenerator.Core.Models
{
    public record TextBox(int X, int Y, int Width, int Height, string? Text) : IWidget;
}
