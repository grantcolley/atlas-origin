using MigraDoc.DocumentObjectModel;
using Origin.Model;

namespace Origin.Pdf.Extensions
{
    public static class DocumentExtensions
    {
        public static Section CreateSectionProperties(this Document document, DocumentConfig documentArgs)
        {
            ArgumentNullException.ThrowIfNull(documentArgs);

            var section = document.AddSection();

            section.PageSetup = document.DefaultPageSetup.Clone();

            section.PageSetup.PageFormat = PageFormat.A4;
            section.PageSetup.LeftMargin = Unit.FromMillimeter(documentArgs.PageMarginLeft);
            section.PageSetup.TopMargin = Unit.FromMillimeter(documentArgs.PageMarginTop);
            section.PageSetup.RightMargin = Unit.FromMillimeter(documentArgs.PageMarginRight);
            section.PageSetup.BottomMargin = Unit.FromMillimeter(documentArgs.PageMarginBottom);

            return section;
        }
    }
}
