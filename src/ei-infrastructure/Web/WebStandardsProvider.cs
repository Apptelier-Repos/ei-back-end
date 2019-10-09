using System.Drawing;
using ei_core.Interfaces;

namespace ei_infrastructure.Web
{
    public class WebStandardsProvider : IWebStandardsProvider
    {
        public string ToWebColor(Color? color)
        {
            return color == null ? null : ColorTranslator.ToHtml(color.Value);
        }
    }
}