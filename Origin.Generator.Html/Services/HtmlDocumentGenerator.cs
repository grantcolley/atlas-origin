using Origin.Core.Models;
using Origin.Generator.Html.Extensions;
using Origin.Service.Base;
using System.Text;

namespace Origin.Generator.Html.Services
{
    public class HtmlDocumentGenerator : DocumentGeneratorBase
    {
        public override DocumentFileExtension DocumentFileExtension => DocumentFileExtension.html;

        public override DocumentGeneratorType DocumentGeneratorType => DocumentGeneratorType.Html;

        protected override byte[] GenerateBytes(DocumentConfig documentConfig)
        {
            ArgumentNullException.ThrowIfNull(documentConfig);

            StringBuilder sb = new("<html><head><meta http-equiv=Content-Type content=\"text/html; charset=UTF-8\">");

            //sb.AddStyles(documentConfig);

            sb.Append("</head><body><div>");
            sb.Append("<table align=\"center\" bgcolor=\"#f1f1f1\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"table-layout:fixed;margin:0 auto;\" width=\"100%\"><tr><td align=\"center\">");
            sb.Append("<table bgcolor=\"#ffffff\" width=\"600\" align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width:600px;margin:20px auto;padding: .5cm .5pt .5cm .5pt\"><tr><td align=\"left\" valign=\"top\" style=\"padding:20px 20px 20px 20;\">");

            foreach (DocumentParagraph? documentParagraph in documentConfig.ConfigParagraphs.Select(cp => cp.DocumentParagraph).Where(p => p != null && p.DocumentParagraphType != DocumentParagraphType.Footer))
            {
                if (documentParagraph != null)
                {
                    sb.AddParagraph(documentParagraph);
                }
            }

            sb.AddFooter(documentConfig);

            sb.Append("</td></tr></table>");
            sb.Append("</td></tr></table>");
            sb.Append("</div></body></html>");

            string html = sb.ToString();

            byte[] bytes = Encoding.ASCII.GetBytes(html);

            return bytes;
        }
    }
}
