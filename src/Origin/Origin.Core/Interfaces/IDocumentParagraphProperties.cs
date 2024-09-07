namespace Origin.Core.Interfaces
{
    public interface IDocumentParagraphProperties : IDocumentContentProperties
    {
        /// <summary>
        /// PDF in inches and OpenXml in twips, where 1 inch = 1440 twips
        /// 
        /// Standard A4 size
        ///  - 8.3 x 11.7 inches (PDF)
        ///  - 21.082 x 29.718 cm (PDF)
        ///  - 11907 x 16839 twips - twentieths of a point (OpenXml)
        /// </summary>

        bool? IgnoreParapgraphSpacing { get; set; }
        int ParagraphSpacingBetweenLinesAfter { get; set; }
        int ParagraphSpacingBetweenLinesBefore { get; set; }
    }
}
