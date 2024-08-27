using Origin.Core.Model;

namespace Origin.Service.Interface
{
    public interface IOriginService
    {
        bool TryCreate(DocumentConfig documentConfig, out string fullFilename);
    }
}