using System;
using System.Drawing;
using System.Globalization;
using System.Web.Mvc;

namespace Project.Website.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static string ColorYIQ(this HtmlHelper helper, string hexColor)
        {

            // The yiq lightness value that determines when the lightness of
            // color changes from "dark" to "light".
            // Acceptable values are between 0 and 255.
            int yiqContrastedThreshold = 150;

            if (hexColor.IndexOf('#') != -1)
                hexColor = hexColor.Replace("#", "");

            int red = 0;
            int green = 0;
            int blue = 0;

            if (hexColor.Length == 6)
            {
                //#RRGGBB
                red = int.Parse(hexColor.Substring(0, 2), NumberStyles.AllowHexSpecifier);
                green = int.Parse(hexColor.Substring(2, 2), NumberStyles.AllowHexSpecifier);
                blue = int.Parse(hexColor.Substring(4, 2), NumberStyles.AllowHexSpecifier);
            }
            else if (hexColor.Length == 3)
            {
                //#RGB
                red = int.Parse(hexColor[0].ToString() + hexColor[0].ToString(), NumberStyles.AllowHexSpecifier);
                green = int.Parse(hexColor[1].ToString() + hexColor[1].ToString(), NumberStyles.AllowHexSpecifier);
                blue = int.Parse(hexColor[2].ToString() + hexColor[2].ToString(), NumberStyles.AllowHexSpecifier);
            }

            int yiq = ((red * 299) + (green * 587) + (blue * 114)) / 1000;

            if (yiq >= yiqContrastedThreshold)
            {
                return "#000";
            }
            else
            {
                return "#fff";
            }

        }

        public static string ColorHexToRgba(this HtmlHelper helper, string hexColor, decimal alpha)
        {
            Color color = ColorTranslator.FromHtml(hexColor);
            int r = Convert.ToInt16(color.R);
            int g = Convert.ToInt16(color.G);
            int b = Convert.ToInt16(color.B);

            return string.Format("rgba({0}, {1}, {2}, {3})", r, g, b, alpha);
        }

        public static string ColorDarken(this HtmlHelper helper, string hexColor, double amount = 0.8)
        {

            Color c1 = ColorTranslator.FromHtml(hexColor);

            // correct value if it is below 0 or above 255
            int red = Math.Max(Math.Min((int)(c1.R * amount), 255), 0);
            int green = Math.Max(Math.Min((int)(c1.G * amount), 255), 0);
            int blue = Math.Max(Math.Min((int)(c1.B * amount), 255), 0);

            Color c2 = Color.FromArgb(c1.A, red, green, blue);

            int r = Convert.ToInt16(c2.R);
            int g = Convert.ToInt16(c2.G);
            int b = Convert.ToInt16(c2.B);

            //return string.Format("rgb({0}, {1}, {2})", r, g, b);
            return string.Format("#{0}{1}{2}", r.ToString("X2"), g.ToString("X2"), b.ToString("X2"));
        }
    }
}