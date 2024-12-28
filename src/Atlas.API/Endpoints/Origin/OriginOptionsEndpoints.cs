using Atlas.API.Interfaces;
using Atlas.Core.Exceptions;
using Atlas.Core.Models;
using Atlas.Logging.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Origin.Data.Access.Interfaces;
using System.Text;

namespace Atlas.API.Endpoints.Origin
{
    internal static class OriginOptionsEndpoints
    {
        internal static async Task<IResult> GetOriginOptions([FromBody] IEnumerable<OptionsArg> optionsArgs, IOriginOptionsData originOptionsData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
        {
            Authorisation? authorisation = null;

            try
            {
                authorisation = await originOptionsData.GetAuthorisationAsync(claimService.GetClaim(), cancellationToken)
                    .ConfigureAwait(false);

                if (authorisation == null)
                {
                    return Results.Unauthorized();
                }

                IEnumerable<OptionItem> optionItems = await originOptionsData.GetOptionsAsync(optionsArgs, cancellationToken)
                    .ConfigureAwait(false);

                return Results.Ok(optionItems);
            }
            catch (AtlasException ex)
            {
                logService.Log(Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        internal static async Task<IResult> GetGenericOriginOptions([FromBody] IEnumerable<OptionsArg> optionsArgs, IOriginOptionsData originOptionsData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
        {
            Authorisation? authorisation = null;

            try
            {
                authorisation = await originOptionsData.GetAuthorisationAsync(claimService.GetClaim(), cancellationToken)
                    .ConfigureAwait(false);

                if (authorisation == null)
                {
                    return Results.Unauthorized();
                }

                string genericOptions = await originOptionsData.GetGenericOptionsAsync(optionsArgs, cancellationToken)
                    .ConfigureAwait(false);

                return Results.Text(genericOptions, "application/json", Encoding.UTF8);
            }
            catch (AtlasException ex)
            {
                logService.Log(Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
