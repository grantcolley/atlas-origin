using DocumentFormat.OpenXml.Wordprocessing;
using Origin.Core.Interfaces;
using Origin.Core.Models;

namespace Origin.OpenXml.Extensions
{
    public static class BodyExtensions
    {
        public static void AddTable(this Body body, DocumentParagraph documentParagraph, IDocumentProperties documentProperties)
        {
            ArgumentNullException.ThrowIfNull(documentParagraph);

            Table tbl = body.AppendChild(new Table());
            tbl.ConfigureTable(documentParagraph, documentProperties);
        }

        public static void AddParagraph(this Body body, DocumentParagraph documentParagraph, IDocumentProperties documentProperties)
        {
            ArgumentNullException.ThrowIfNull(documentParagraph);
            ArgumentNullException.ThrowIfNull(documentProperties);

            if (documentParagraph.DocumentParagraphType == DocumentParagraphType.Table)
            {
                body.AddTable(documentParagraph, documentProperties);
            }
            else
            {
                Paragraph p = body.AppendChild(new Paragraph());

                p.AddContent(documentParagraph, documentProperties);
            }
        }
    }
}
