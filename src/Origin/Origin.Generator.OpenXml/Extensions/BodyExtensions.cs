using DocumentFormat.OpenXml.Wordprocessing;
using Origin.Core.Models;

namespace Origin.Generator.OpenXml.Extensions
{
    public static class BodyExtensions
    {
        public static void AddTable(this Body body, DocumentParagraph documentParagraph)
        {
            ArgumentNullException.ThrowIfNull(documentParagraph);

            Table tbl = body.AppendChild(new Table());
            tbl.ConfigureTable(documentParagraph);
        }

        public static void AddParagraph(this Body body, DocumentParagraph documentParagraph)
        {
            ArgumentNullException.ThrowIfNull(documentParagraph);

            if (documentParagraph.DocumentParagraphType == DocumentParagraphType.Table)
            {
                body.AddTable(documentParagraph);
            }
            else
            {
                Paragraph p = body.AppendChild(new Paragraph());

                p.AddContent(documentParagraph);
            }
        }
    }
}
