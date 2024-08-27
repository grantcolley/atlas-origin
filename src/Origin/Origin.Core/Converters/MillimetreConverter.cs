namespace Origin.Core.Converters
{
    public static class MillimetreConverter
    {
        // https://startbigthinksmall.wordpress.com/2010/01/04/points-inches-and-emus-measuring-units-in-office-open-xml/
        // https://www.unitconverters.net/length/point-to-millimeter.htm
        // https://learn.microsoft.com/en-us/dotnet/api/documentformat.openxml.wordprocessing.pagesize?view=openxml-3.0.1
        // https://www.adobe.com/uk/creativecloud/design/discover/a4-format.html#:~:text=A%20piece%20of%20A4%20paper,is%20used%20in%20most%20countries.
        // https://en.wikipedia.org/wiki/Paper_size

        // The international default letter size is ISO 216 A4:
        // - 210x297mm
        // - 8.3×11.7in
        // - 11906x16838 twips (twentieths of a point)

        // MM Conversion:
        // - 1 point = 0.3527777778 mm
        // - 1 mm = 2.8346456693 point

        // EMU Conversion
        // - 1 cm = 360000 EMU
        // - 1 mm = 36000 EMU

        public static long ToEMU(this int value)
        {
            return Convert.ToInt32(value * 36000m);
        }

        public static int ToTwips(this int value)
        {
            return Convert.ToInt32(value * 2.8346456693m * 20m);
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
            return Convert.ToUInt32(value * 2.8346456693m * 20m);
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
