using Origin.Model;

namespace Origin.Service.Interface
{
    public interface IOriginService
    {
        bool TryCreate(DocumentConfig documentArgs, out string fullFilename);
    }
}