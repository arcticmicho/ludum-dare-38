﻿using UnityEngine;
using System.Collections;
//to see the colors: http://www.foszor.com/blog/xna-color-chart/

public class Colors
{
    public static string ColorToHex(Color32 color)
    {
        return color.a.ToString("X2") + color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
    }

    public static Color HexToColor(string hex)
    {
        if (hex != null)
        {
            byte a = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte r = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);

            return new Color32(r, g, b, a);
        }
        return Color.white;
    }

    // Colors
    public static Color AliceBlue = new Color32(240, 248, 255, 255);
    public static Color AntiqueWhite = new Color32(250, 235, 215, 255);
    public static Color Aquamarine = new Color32(127, 255, 212, 255);
    public static Color Azure = new Color32(240, 255, 255, 255);
    public static Color Beige = new Color32(245, 245, 220, 255);
    public static Color Bisque = new Color32(255, 228, 196, 255);
    public static Color Black = new Color32(0, 0, 0, 255);
    public static Color BlanchedAlmond = new Color32(255, 235, 205, 255);
    public static Color Blue = new Color32(0, 0, 255, 255);
    public static Color BlueViolet = new Color32(138, 43, 226, 255);
    public static Color Brown = new Color32(165, 42, 42, 255);
    public static Color BurlyWood = new Color32(222, 184, 135, 255);
    public static Color CadetBlue = new Color32(95, 158, 160, 255);
    public static Color Chartreuse = new Color32(127, 255, 0, 255);
    public static Color Chocolate = new Color32(210, 105, 30, 255);
    public static Color Coral = new Color32(255, 127, 80, 255);
    public static Color CornflowerBlue = new Color32(100, 149, 237, 255);
    public static Color Cornsilk = new Color32(255, 248, 220, 255);
    public static Color Crimson = new Color32(220, 20, 60, 255);
    public static Color Cyan = new Color32(0, 255, 255, 255);
    public static Color DarkBlue = new Color32(0, 0, 139, 255);
    public static Color DarkCyan = new Color32(0, 139, 139, 255);
    public static Color DarkGoldenrod = new Color32(184, 134, 11, 255);
    public static Color DarkGray = new Color32(169, 169, 169, 255);
    public static Color DarkGreen = new Color32(0, 100, 0, 255);
    public static Color DarkKhaki = new Color32(189, 183, 107, 255);
    public static Color DarkMagenta = new Color32(139, 0, 139, 255);
    public static Color DarkOliveGreen = new Color32(85, 107, 47, 255);
    public static Color DarkOrange = new Color32(255, 140, 0, 255);
    public static Color DarkOrchid = new Color32(153, 50, 204, 255);
    public static Color DarkRed = new Color32(139, 0, 0, 255);
    public static Color DarkSalmon = new Color32(233, 150, 122, 255);
    public static Color DarkSeaGreen = new Color32(143, 188, 139, 255);
    public static Color DarkSlateBlue = new Color32(72, 61, 139, 255);
    public static Color DarkSlateGray = new Color32(47, 79, 79, 255);
    public static Color DarkTurquoise = new Color32(0, 206, 209, 255);
    public static Color DarkViolet = new Color32(148, 0, 211, 255);
    public static Color DeepPink = new Color32(255, 20, 147, 255);
    public static Color DeepSkyBlue = new Color32(0, 191, 255, 255);
    public static Color DimGray = new Color32(105, 105, 105, 255);
    public static Color DodgerBlue = new Color32(30, 144, 255, 255);
    public static Color Firebrick = new Color32(178, 34, 34, 255);
    public static Color FloralWhite = new Color32(255, 250, 240, 255);
    public static Color ForestGreen = new Color32(34, 139, 34, 255);
    public static Color Gainsboro = new Color32(220, 220, 220, 255);
    public static Color GhostWhite = new Color32(248, 248, 255, 255);
    public static Color Gold = new Color32(255, 215, 0, 255);
    public static Color Goldenrod = new Color32(218, 165, 32, 255);
    public static Color Gray = new Color32(128, 128, 128, 255);
    public static Color Green = new Color32(0, 128, 0, 255);
    public static Color GreenYellow = new Color32(173, 255, 47, 255);
    public static Color Honeydew = new Color32(240, 255, 240, 255);
    public static Color HotPink = new Color32(255, 105, 180, 255);
    public static Color IndianRed = new Color32(205, 92, 92, 255);
    public static Color Indigo = new Color32(75, 0, 130, 255);
    public static Color Ivory = new Color32(255, 255, 240, 255);
    public static Color Khaki = new Color32(240, 230, 140, 255);
    public static Color Lavender = new Color32(230, 230, 250, 255);
    public static Color LavenderBlush = new Color32(255, 240, 245, 255);
    public static Color LawnGreen = new Color32(124, 252, 0, 255);
    public static Color LemonChiffon = new Color32(255, 250, 205, 255);
    public static Color LightBlue = new Color32(173, 216, 230, 255);
    public static Color LightCoral = new Color32(240, 128, 128, 255);
    public static Color LightCyan = new Color32(224, 255, 255, 255);
    public static Color LightGoldenrodYellow = new Color32(250, 250, 210, 255);
    public static Color LightGreen = new Color32(144, 238, 144, 255);
    public static Color LightGray = new Color32(211, 211, 211, 255);
    public static Color LightPink = new Color32(255, 182, 193, 255);
    public static Color LightSalmo = new Color32(255, 160, 122, 255);
    public static Color LightSeaGreen = new Color32(32, 178, 170, 255);
    public static Color LightSkyBlue = new Color32(135, 206, 250, 255);
    public static Color LightSlateGray = new Color32(119, 136, 153, 255);
    public static Color LightSteelBlue = new Color32(176, 196, 222, 255);
    public static Color LightYellow = new Color32(255, 255, 224, 255);
    public static Color Lime = new Color32(0, 255, 0, 255);
    public static Color LimeGreen = new Color32(50, 205, 50, 255);
    public static Color Linen = new Color32(250, 240, 230, 255);
    public static Color Magenta = new Color32(255, 0, 255, 255);
    public static Color Maroon = new Color32(128, 0, 0, 255);
    public static Color MediumAquamarine = new Color32(102, 205, 170, 255);
    public static Color MediumBlue = new Color32(0, 0, 205, 255);
    public static Color MediumOrchid = new Color32(186, 85, 211, 255);
    public static Color MediumPurple = new Color32(147, 112, 219, 255);
    public static Color MediumSeaGreen = new Color32(60, 179, 113, 255);
    public static Color MediumSlateBlue = new Color32(123, 104, 238, 255);
    public static Color MediumSpringGreen = new Color32(0, 250, 154, 255);
    public static Color MediumTurquoise = new Color32(72, 209, 204, 255);
    public static Color MediumVioletRed = new Color32(199, 21, 133, 255);
    public static Color MidnightBlue = new Color32(25, 25, 112, 255);
    public static Color MintCream = new Color32(245, 255, 250, 255);
    public static Color MistyRose = new Color32(255, 228, 225, 255);
    public static Color Moccasin = new Color32(255, 228, 181, 255);
    public static Color NavajoWhite = new Color32(255, 222, 173, 255);
    public static Color Navy = new Color32(0, 0, 128, 255);
    public static Color OldLace = new Color32(253, 245, 230, 255);
    public static Color Olive = new Color32(128, 128, 0, 255);
    public static Color OliveDrab = new Color32(107, 142, 35, 255);
    public static Color Orange = new Color32(255, 165, 0, 255);
    public static Color OrangeRed = new Color32(255, 69, 0, 255);
    public static Color Orchid = new Color32(218, 112, 214, 255);
    public static Color PaleGoldenrod = new Color32(238, 232, 170, 255);
    public static Color PaleGreen = new Color32(152, 251, 152, 255);
    public static Color PaleTurquoise = new Color32(175, 238, 238, 255);
    public static Color PaleVioletRed = new Color32(219, 112, 147, 255);
    public static Color PapayaWhip = new Color32(255, 239, 213, 255);
    public static Color PeachPuff = new Color32(255, 218, 185, 255);
    public static Color Peru = new Color32(205, 133, 63, 255);
    public static Color Pink = new Color32(255, 192, 203, 255);
    public static Color Plum = new Color32(221, 160, 221, 255);
    public static Color PowderBlue = new Color32(176, 224, 230, 255);
    public static Color Purple = new Color32(128, 0, 128, 255);
    public static Color Red = new Color32(255, 0, 0, 255);
    public static Color RosyBrown = new Color32(188, 143, 143, 255);
    public static Color RoyalBlue = new Color32(65, 105, 225, 255);
    public static Color SaddleBrown = new Color32(139, 69, 19, 255);
    public static Color Salmon = new Color32(250, 128, 114, 255);
    public static Color SandyBrown = new Color32(244, 164, 96, 255);
    public static Color SeaGreen = new Color32(46, 139, 87, 255);
    public static Color SeaShell = new Color32(255, 245, 238, 255);
    public static Color Sienna = new Color32(160, 82, 45, 255);
    public static Color Silver = new Color32(192, 192, 192, 255);
    public static Color SkyBlue = new Color32(135, 206, 235, 255);
    public static Color SlateBlue = new Color32(106, 90, 205, 255);
    public static Color SlateGray = new Color32(112, 128, 144, 255);
    public static Color Snow = new Color32(255, 250, 250, 255);
    public static Color SpringGreen = new Color32(0, 255, 127, 255);
    public static Color SteelBlue = new Color32(70, 130, 180, 255);
    public static Color Tan = new Color32(210, 180, 140, 255);
    public static Color Teal = new Color32(0, 128, 128, 255);
    public static Color Thistle = new Color32(216, 191, 216, 255);
    public static Color Tomato = new Color32(255, 99, 71, 255);
    public static Color Turquoise = new Color32(64, 224, 208, 255);
    public static Color Violet = new Color32(238, 130, 238, 255);
    public static Color Wheat = new Color32(245, 222, 179, 255);
    public static Color White = new Color32(255, 255, 255, 255);
    public static Color WhiteSmoke = new Color32(245, 245, 245, 255);
    public static Color Yellow = new Color32(255, 255, 0, 255);
    public static Color YellowGreen = new Color32(154, 205, 50, 255);

}
