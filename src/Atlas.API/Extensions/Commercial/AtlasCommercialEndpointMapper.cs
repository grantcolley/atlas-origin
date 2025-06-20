﻿using Atlas.API.Endpoints.Commercial;
using Atlas.Core.Constants;
using Commercial.Core.Constants;
using Commercial.Core.Models;

namespace Atlas.API.Extensions.Commercial
{
    internal static class AtlasCommercialEndpointMapper
    {
        internal static WebApplication? MapAtlasCommercialEndpoints(this WebApplication app)
        {
            app.MapGet($"/{CommercialAPIEndpoints.GET_CUSTOMERS}", CommercialEndpoints.GetCustomers)
                .WithOpenApi()
                .WithName(CommercialAPIEndpoints.GET_CUSTOMERS)
                .WithDescription("Gets a list of customers.")
                .Produces<IEnumerable<Customer>?>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapGet($"/{CommercialAPIEndpoints.GET_CUSTOMER}/{{id:int}}", CommercialEndpoints.GetCustomer)
                .WithOpenApi()
                .WithName(CommercialAPIEndpoints.GET_CUSTOMER)
                .WithDescription("Gets a customer for the given id.")
                .Produces<Customer>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapGet($"/{CommercialAPIEndpoints.GET_COMPANIES}", CommercialEndpoints.GetCompanies)
                .WithOpenApi()
                .WithName(CommercialAPIEndpoints.GET_COMPANIES)
                .WithDescription("Gets a list of companies.")
                .Produces<IEnumerable<Company>?>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            app.MapGet($"/{CommercialAPIEndpoints.GET_COMPANY}/{{id:int}}", CommercialEndpoints.GetCompany)
                .WithOpenApi()
                .WithName(CommercialAPIEndpoints.GET_COMPANY)
                .WithDescription("Gets a company for the given id.")
                .Produces<Company>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            return app;
        }
    }
}
