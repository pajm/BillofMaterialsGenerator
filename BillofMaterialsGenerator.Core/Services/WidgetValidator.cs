using BillofMaterialsGenerator.Core.Interfaces;
using BillofMaterialsGenerator.Core.Models;

namespace BillofMaterialsGenerator.Core.Services
{
    public class WidgetValidator:IWidgetValidator
    {

        public bool IsValid(object widget)
        {
            if(widget == null) return false;

            switch (widget)
            {
                case Rectangle r:
                    return IsValidPosition(r.X, r.Y) && r.Width > 0 && r.Height > 0;
                case Square s:
                    return IsValidPosition(s.X, s.Y) && s.Width> 0;
                case Circle c:
                    return IsValidPosition(c.X, c.Y) && c.Diameter > 0;
                case Ellipse e:
                    return IsValidPosition(e.X, e.Y) && e.HorizontalDiameter >0 && e.VerticalDiameter > 0;
                case TextBox t:
                    return IsValidPosition(t.X, t.Y) && t.Height > 0 && t.Width > 0;
            }
            return false;
        }

        private bool IsValidPosition(int x, int y)
        {
            if (x < 0 || x > 1000) {  return false; }
            if (y < 0 || y > 1000) {  return false; }
            return true;
        }
    }
}
