using Atlas.API.Interfaces;
using Atlas.Core.Constants;
using Atlas.Core.Exceptions;
using Atlas.Core.Models;
using Atlas.Logging.Interfaces;
using Commercial.Core.Models;
using Commercial.Data.Access.Interfaces;

namespace Atlas.API.Endpoints.Commercial
{
    internal static class CommercialEndpoints
    {
        internal static async Task<IResult> GetCustomers(ICommercialData commercialData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
        {
            Authorisation? authorisation = null;

            try
            {
                authorisation = await commercialData.GetAuthorisationAsync(claimService.GetClaim(), cancellationToken)
                    .ConfigureAwait(false);

                if (authorisation == null
                    || !authorisation.HasPermission(Auth.COMMERCIAL_READ))
                {
                    return Results.Unauthorized();
                }

                IEnumerable<Customer>? customers = await commercialData.GetCustomersAsync(cancellationToken)
                    .ConfigureAwait(false);

                return Results.Ok(new AuthResult<IEnumerable<Customer>?> { Authorisation = authorisation, Result = customers });
            }
            catch (AtlasException ex)
            {
                logService.Log(Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        internal static async Task<IResult> GetCustomer(int id, ICommercialData commercialData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
        {
            Authorisation? authorisation = null;

            try
            {
                authorisation = await commercialData.GetAuthorisationAsync(claimService.GetClaim(), cancellationToken)
                    .ConfigureAwait(false);

                if (authorisation == null
                    || !authorisation.HasPermission(Auth.COMMERCIAL_READ))
                {
                    return Results.Unauthorized();
                }

                Customer? customer = await commercialData.GetCustomerAsync(id, cancellationToken)
                    .ConfigureAwait(false);

                return Results.Ok(customer);
            }
            catch (AtlasException ex)
            {
                logService.Log(Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        internal static async Task<IResult> GetCompanies(ICommercialData commercialData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
        {
            Authorisation? authorisation = null;

            try
            {
                authorisation = await commercialData.GetAuthorisationAsync(claimService.GetClaim(), cancellationToken)
                    .ConfigureAwait(false);

                if (authorisation == null
                    || !authorisation.HasPermission(Auth.COMMERCIAL_READ))
                {
                    return Results.Unauthorized();
                }

                IEnumerable<Company>? companies = await commercialData.GetCompaniesAsync(cancellationToken)
                    .ConfigureAwait(false);

                return Results.Ok(new AuthResult<IEnumerable<Company>?> { Authorisation = authorisation, Result = companies });
            }
            catch (AtlasException ex)
            {
                logService.Log(Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        internal static async Task<IResult> GetCompany(int id, ICommercialData commercialData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
        {
            Authorisation? authorisation = null;

            try
            {
                authorisation = await commercialData.GetAuthorisationAsync(claimService.GetClaim(), cancellationToken)
                    .ConfigureAwait(false);

                if (authorisation == null
                    || !authorisation.HasPermission(Auth.COMMERCIAL_READ))
                {
                    return Results.Unauthorized();
                }

                Company? company = await commercialData.GetCompanyAsync(id, cancellationToken)
                    .ConfigureAwait(false);

                return Results.Ok(company);
            }
            catch (AtlasException ex)
            {
                logService.Log(Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
