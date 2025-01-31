namespace Origin.Core.Converters
{
    public static class MillimetreConverter
    {
        // https://startbigthinksmall.wordpress.com/2010/01/04/points-inches-and-emus-measuring-units-in-office-open-xml/
        // https://www.unitconverters.net/length/point-to-millimeter.htm
        // https://learn.microsoft.com/en-us/dotnet/api/documentformat.openxml.wordprocessing.pagesize?view=openxml-3.0.1
        // https://www.adobe.com/uk/creativecloud/design/discover/a4-format.html#:~:text=A%20piece%20of%20A4%20paper,is%20used%20in%20most%20countries.
        // https://en.wikipedia.org/wiki/Paper_size
        // https://www.w3.org/Style/Examples/007/units.en.html

        // The international default letter size is ISO 216 A4:
        // - 210x297mm
        // - 8.3×11.7in
        // - 11906x16838 twips (twentieths of a point)

        // MM Conversion:
        // - 1 point = 0.3527777778 mm
        // - 1 mm = 2.8346456693 point
        // - 1 mm = 56.692913386 twips

        // EMU Conversion
        // - 1 cm = 360000 EMU
        // - 1 mm = 36000 EMU

        private const int _1mm_in_EMUs = 36000;
        private const decimal _1mm_in_points = 2.8346456693m;
        private const decimal _1mm_in_twips = 56.692913386m; // 2.8346456693m * 20m

        public static decimal ToPoints(this int? value)
        {
            if (value == null)
            {
                return 0.0m;
            }
            else
            {
                return Convert.ToDecimal(value * _1mm_in_points);
            }
        }

        public static decimal ToPoints(this int value)
        {
            return Convert.ToDecimal(value * _1mm_in_points);
        }

        public static long ToEMU(this int value)
        {
            return Convert.ToInt32(value * _1mm_in_EMUs);
        }

        public static int ToTwips(this int value)
        {
            return Convert.ToInt32(value * _1mm_in_twips);
        }

        public static int ToTwips(this int? value)
        {
            if (value.HasValue)
            {
                return value.Value.ToTwips();
            }
            else
            {
                throw new ArgumentNullException(nameof(value));
            }
        }

        public static uint ToUTwips(this int value)
        {
            return Convert.ToUInt32(value * _1mm_in_twips);
        }

        public static uint ToUTwips(this int? value)
        {
            if (value.HasValue)
            {
                return value.Value.ToUTwips();
            }
            else
            {
                throw new ArgumentNullException(nameof(value));
            }
        }
    }
}
