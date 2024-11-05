using Origin.Core.Models;

namespace Origin.Service.Interface
{
    public interface IOriginService
    {
        void CreateFile(DocumentConfig documentConfig, out string fullFilename);
    }
}