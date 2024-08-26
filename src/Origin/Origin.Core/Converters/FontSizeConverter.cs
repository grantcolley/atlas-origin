namespace Origin.Converters
{
    public static class FontSizeConverter
    {
        public static int ToHalfPointEquivalent(this int? points)
        {
            if (points.HasValue)
            {
                return points.Value * 2;
            }
            else
            {
                throw new ArgumentNullException(nameof(points));
            }
        }
    }
}
