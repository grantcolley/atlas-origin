using Origin.Core.Models;
using Origin.Service.Base;
using Origin.Service.Interface;

namespace Origin.Service.Services
{
    public class DocumentHttpService(IDocumentGeneratorProvider documentGeneratorProvider) 
        : DocumentServiceBase<bool>(documentGeneratorProvider)
    {
        public override Task<bool> ExecuteAsync(Document document, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
