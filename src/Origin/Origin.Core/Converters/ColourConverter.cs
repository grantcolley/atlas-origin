using System.Drawing;

namespace Origin.Core.Converters
{
    public static class ColourConverter
    {
        public static Color RgbToColor(this string value)
        {
            int[] rgb = value.SplitRgb();

            return Color.FromArgb(rgb[0], rgb[1], rgb[2]);
        }

        public static string RgbToHex(this string value)
        {
            int[] rgb = value.SplitRgb();

            rgb[0] = Math.Max(0, Math.Min(255, rgb[0]));
            rgb[1] = Math.Max(0, Math.Min(255, rgb[1]));
            rgb[2] = Math.Max(0, Math.Min(255, rgb[2]));

            return rgb[0].ToString("X2") + rgb[1].ToString("X2") + rgb[2].ToString("X2");
        }

        public static byte[] RgbToByteArray(this string value)
        {
            int[] rgb = value.SplitRgb();

            byte[] rgbBytes =
            [
                BitConverter.GetBytes(Math.Max(0, Math.Min(255, rgb[0])))[0],
                BitConverter.GetBytes(Math.Max(0, Math.Min(255, rgb[1])))[0],
                BitConverter.GetBytes(Math.Max(0, Math.Min(255, rgb[2])))[0],
            ];

            return rgbBytes;
        }

        private static int[] SplitRgb(this string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);

            int[] rgb = value.Split(',').Select(int.Parse).ToArray();

            if (rgb.Length != 3) throw new InvalidCastException(nameof(rgb));

            return rgb;
        }
    }
}
