using BillofMaterialsGenerator.Core.Models;

namespace BillofMaterialsGenerator.Core.Interfaces
{
    public interface IBillOfMaterialsGenerator
    {
        public string GenerateBOM(List<IWidget> widgets);
    }
}
