using System.Drawing;

namespace ei_core.Interfaces
{
    public interface IWebStandardsProvider
    {
        /// <summary>
        ///     Translates the specified Color structure to a standard Web color string representation.
        ///     https://en.wikipedia.org/wiki/Web_color
        /// </summary>
        /// <param name="color">The Color structure to translate.</param>
        /// <returns>
        ///     If the input color has a known color name, then it returns the HTML color name, or the hex triplet
        ///     representation otherwise.
        /// </returns>
        string ToWebColor(Color? color);
    }
}