using BillofMaterialsGenerator.Core.Interfaces;

namespace BillofMaterialsGenerator.Core.Services
{
    public class Logger:ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"[ERROR] {message}");
        }
    }
}
