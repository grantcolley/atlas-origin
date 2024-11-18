using MigraDoc.DocumentObjectModel;
using Origin.Core.Models;

namespace Origin.Generator.PdfSharp.Extensions
{
    public static class DocumentExtensions
    {
        public static Section CreateSectionProperties(this MigraDoc.DocumentObjectModel.Document document, DocumentConfig documentConfig)
        {
            ArgumentNullException.ThrowIfNull(documentConfig);

            var section = document.AddSection();

            section.PageSetup = document.DefaultPageSetup.Clone();

            section.PageSetup.PageFormat = PageFormat.A4;
            section.PageSetup.LeftMargin = Unit.FromMillimeter(documentConfig.PageMarginLeft);
            section.PageSetup.TopMargin = Unit.FromMillimeter(documentConfig.PageMarginTop);
            section.PageSetup.RightMargin = Unit.FromMillimeter(documentConfig.PageMarginRight);
            section.PageSetup.BottomMargin = Unit.FromMillimeter(documentConfig.PageMarginBottom);

            return section;
        }
    }
}
