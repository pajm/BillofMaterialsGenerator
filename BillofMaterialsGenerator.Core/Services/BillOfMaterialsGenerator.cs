using BillofMaterialsGenerator.Core.Interfaces;
using BillofMaterialsGenerator.Core.Models;

namespace BillOfMarterialsGenerator.Core;

public class BillOfMaterialsGenerator:IBillOfMaterialsGenerator
{
    private readonly ILogger _logger;
    private readonly IWidgetValidator _widgetValidator;
    public BillOfMaterialsGenerator(IWidgetValidator widgetValidator, ILogger logger)
    {
        _widgetValidator = widgetValidator;
        _logger = logger;
    }
    public string GenerateBOM(List<IWidget> widgets)
    {
        if (widgets == null || widgets.Count == 0)   
        {
            _logger.Log("Widget list was null or empty.");
            return "+++++Abort+++++";
        }

        try
        {
            var sb = new System.Text.StringBuilder();

            sb.AppendLine("----------------------------------------------------------------");
            sb.AppendLine("Bill of Materials");
            sb.AppendLine("----------------------------------------------------------------");

            foreach (var widget in widgets)
            {
                if (!_widgetValidator.IsValid(widget)) { _logger.Log($"{widget.GetType()} was invalid"); return "+++++Abort+++++"; }
                
                switch (widget)
                {
                    case Rectangle r:
                        sb.AppendLine($"Rectangle ({r.X},{r.Y}) width={r.Width} height={r.Height}");
                        break;
                    case Square s:
                        sb.AppendLine($"Square ({s.X},{s.Y}) size={s.Width}");
                        break;
                    case Circle c:
                        sb.AppendLine($"Circle ({c.X},{c.Y}) size={c.Diameter}");
                        break;
                    case Ellipse e:
                        sb.AppendLine($"Ellipse ({e.X},{e.Y}) diameterH = {e.HorizontalDiameter} diameterV = {e.VerticalDiameter}");
                        break;
                    case TextBox t:
                        string text = t.Text != null ? t.Text : string.Empty;
                        sb.AppendLine($"Textbox ({t.X},{t.Y}) width={t.Width} height={t.Height} text=\"{text}\"");
                        break;
                }
            }

            sb.AppendLine("----------------------------------------------------------------");
            return sb.ToString();
        }
        catch(Exception ex)
        {
            _logger.Log(ex.Message);
            return "+++++Abort+++++";
        }   
    }
}
