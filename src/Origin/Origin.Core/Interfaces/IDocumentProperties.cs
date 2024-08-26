namespace Origin.Interfaces
{
    public interface IDocumentProperties
    {
        /// <summary>
        /// PDF in inches and OpenXml in twips, where 1 inch = 1440 twips
        /// 
        /// Standard A4 size
        ///  - 8.3 x 11.7 inches (PDF)
        ///  - 21.082 x 29.718 cm (PDF)
        ///  - 11907 x 16839 twips - twentieths of a point (OpenXml)
        /// </summary>

        int PageMarginLeft { get; set; }
        int PageMarginTop { get; set; }
        int PageMarginRight { get; set; }
        int PageMarginBottom { get; set; }
        int ParagraphSpacingBetweenLinesAfter { get; set; }
        int ParagraphSpacingBetweenLinesBefore { get; set; }
    }
}
