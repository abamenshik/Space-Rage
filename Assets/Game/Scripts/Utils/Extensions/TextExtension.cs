using System.Text;
using UnityEngine;

namespace Scripts.Extension
{
    public enum TextColor
    {
        red,
        green,
        blue,
        yellow,
        orange,
        magenta,
        black,
        cyan,
        grey
    }
    public static class TextExtension
    {
        public static string ToHexString(Color color)
        {
            StringBuilder builder = new();

            builder.Append(((byte)(color.r * 255f)).ToString("X2"));
            builder.Append(((byte)(color.g * 255f)).ToString("X2"));
            builder.Append(((byte)(color.b * 255f)).ToString("X2"));
            builder.Append(((byte)(color.a * 255f)).ToString("X2"));

            return builder.ToString();
        }
        public static string Color(this string str, TextColor color)
        {
            return $"<color={color}>{str}</color>";
        }
        public static string Color(this string str, Color color)
        {
            return $"<color=#{ToHexString(color)}>{str}</color>";
        }
        public static string Bold(this string str)
        {
            return $"<b>{str}</b>";
        }
        public static string Italic(this string str)
        {
            return $"<i>{str}</i>";
        }
        public static string Line(this string str)
        {
            return $"<u>{str}</u>";
        }
    }
}