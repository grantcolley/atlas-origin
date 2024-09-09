using Atlas.Core.Models;
using Atlas.Data.Context;

namespace Origin.Data.Access.Interfaces
{
    public interface IOriginOptionsData : IAuthorisationData
    {
        Task<IEnumerable<OptionItem>> GetOptionsAsync(IEnumerable<OptionsArg> optionsArgs, CancellationToken cancellationToken);
        Task<string> GetGenericOptionsAsync(IEnumerable<OptionsArg> optionsArgs, CancellationToken cancellationToken);
    }
}
