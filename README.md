![Alt text](/readme-images/Atlas.png?raw=true "Atlas") 

###### .NET 9.0, Blazor, ASP.NET Core Web API, Azure, Auth0, FluentUI, FluentValidation, Backend for Frontend (BFF), Entity Framework Core, MS SQL Server, SQLite

A .NET 9.0 Blazor framework for hosting and building Blazor applications using the Backend for Frontend (BFF) pattern. It comes with authentication, authorisation, change tracking, and persisting structured logs to the database. Follow the instructions for publishing to Azure.

See the [Worked Examples](#worked-examples) for step-by-step guidance on how to introduce new modules into the **Atlas** framework.

\
![Alt text](/readme-images/Atlas_Architecture.png?raw=true "Atlas Architecture")

## Table of Contents
* [Setup the Solution](#setup-the-solution)
    * [Multiple Startup Projects](#multiple-startup-projects)
    * [Atlas.API Configuration](#atlasapi-configuration)
    * [Atlas.Blazor.Web.App Configuration](#atlasblazorwebapp-configuration)
    * [Create the Database](#create-the-database)
    * [Insert Seed Data](#insert-seed-data)
* [Authentication](#authentication)
    * [Create an Auth0 Role](#create-an-auth0-role)
    * [Create Auth0 Users](#create-auth0-users)
    * [Securing Atlas.API](#securing-atlasapi)
    * [Securing Atlas.Blazor.Web.App](#securing-atlasblazorwebapp)
    * [Log In](#log-in)
* [Authorization](#authorization)
    * [Users, Roles and Permissions](#users-roles-and-permissions)
* [Support Role](#support-role)
    * [Logging](#logging)
* [Navigation](#navigation)
    * [Modules, Categories and Pages](#modules-categories-and-pages)
* [Audit](#audit)
* [Publish Atlas to Azure](#publish-atlas-to-azure)
  * [Steps to Publish Atlas to Azure](#steps-to-publish-atlas-to-azure)
    * [Resource Group](#resource-group)   
    * [Web App + Database](#web-app--database)
    * [Web App](#web-app)
    * [Environment Variables](#environment-variables)
    * [Auth0](#auth0)
    * [Update Web API Configuration and Publish to Azure](#update-web-api-configuration-and-publish-to-azure)
    * [Update Blazor Web App Configuration and Publish to Azure](#update-blazor-web-app-configuration-and-publish-to-azure)
* [Worked Examples](#worked-examples)
    * [Blazor Template](#blazor-template) 
* [Notes](#notes)
    * [FluentDesignTheme Dark/Light](#fluentdesigntheme-darklight)
    * [Backend for frontend](#backend-for-frontend)

# Setup the Solution

### Multiple Startup Projects
In the _Solution Properties_, specify multiple startup projects and set the action for both **Atlas.API** Web API and **Atlas.Blazor.Web.App** Blazor application, to *Start*.

![Alt text](/readme-images/Solution_Property_Pages.png?raw=true "Solution Properties") 

### Atlas.API Configuration
In the **Atlas.API** [appsettings.json](https://github.com/grantcolley/atlas-origin/blob/main/src/Atlas.API/appsettings.json) set the connection strings, configure [Auth0](https://auth0.com/) settings and generating seed data.

> [!NOTE]  
> Read the next section on [Authentication](#authentication) for how to configure [Auth0](https://auth0.com/) as the identity provider. 

```json
{
  "ConnectionStrings": {
    "DefaultConnection": ""   👈 set the AtlasOrigin database connection string
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore.Database.Command": "Information"
    }
  },
  "Serilog": {
    "Using": [ "Serilog.Sinks.MSSqlServer" ],
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Error",
        "Microsoft.EntityFrameworkCore.Database.Command": "Information"
      }
    },
    "WriteTo": [
      {
        "Name": "MSSqlServer",
        "Args": {
          "connectionString": "",   👈set the AtlasOrigin database connection string for Serilogs MS SqlServer
          "tableName": "Logs",
          "autoCreateSqlTable": true,
          "columnOptionsSection": {
            "customColumns": [
              {
                "ColumnName": "User",
                "DataType": "nvarchar",
                "DataLength": 450
              },
              {
                "ColumnName": "Context",
                "DataType": "nvarchar",
                "DataLength": 450
              }
            ]
          }
        }
      }
    ]
  },
  "AllowedHosts": "*",
  "Auth0": {
    "Domain": "",               👈specify the Auth0 domain
    "Audience": ""              👈specify the audience
  },
  "SeedData": {
    "GenerateSeedData": "true", 👈 set to true to create seed data including modules, categories, pages, users, permissions and roles.
    "GenerateSeedLogs":  "true" 👈 set to true to generate mock logs
  }
}
```

### Atlas.Blazor.Web.App Configuration
In the **Atlas.Blazor.Web.App** [appsettings.json](https://github.com/grantcolley/atlas-origin/blob/main/src/Atlas.Blazor.Web.App/appsettings.json) configure [Auth0](https://auth0.com/) settings and specify the **Atlas.API** url.

> [!NOTE]  
> Read the next section on [Authentication](#authentication) for how to configure [Auth0](https://auth0.com/) as the identity provider.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore.Database.Command": "Warning"
    }
  },
  "Serilog": {
    "Using": [ "Serilog.Sinks.MSSqlServer" ],
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Error",
        "Microsoft.EntityFrameworkCore.Database.Command": "Information"
      }
    },
    "WriteTo": [
      {
        "Name": "MSSqlServer",
        "Args": {
          "connectionString": "",    👈set the AtlasOrigin database connection string for Serilogs MS SqlServer
          "tableName": "Logs",
          "autoCreateSqlTable": true,
          "columnOptionsSection": {
            "customColumns": [
              {
                "ColumnName": "User",
                "DataType": "nvarchar",
                "DataLength": 450
              },
              {
                "ColumnName": "Context",
                "DataType": "nvarchar",
                "DataLength": 450
              }
            ]
          }
        }
      }
    ]
  },
  "AllowedHosts": "*",
  "Auth0": {
    "Domain": "",                           👈specify the Auth0 domain
    "ClientId": "",                         👈specify the Auth0 ClientId
    "ClientSecret": "",                     👈specify the Auth0 ClientSecret
    "Audience": ""                          👈specify the audience
  },
  "AtlasAPI": "https://localhost:44420"     👈specify the AtlasAPI url
}
```

### Create the Database
Use the `.NET CLI` for Entity Framework to create your database and create your schema from the migration. In the `Developer Powershell` or similar, navigate to the **Atlas.API** folder and run the following command.

```
dotnet ef database update --project ..\..\data\Atlas.Migrations.SQLServer
```

### Insert Seed Data
In the **Atlas.API** [appsettings.json](https://github.com/grantcolley/atlas-origin/blob/dbd3270b3eb0374e8e4de1976051e9d35276bca5/src/Atlas.API/appsettings.json#L51-L54) configuration file set `GenerateSeedData` and `GenerateSeedLogs` to true. This will populate the database with seed data at startup.

> [!WARNING]  
> If `"GenerateSeedData": "true"` the tables in the AtlasOrigin database will be truncated and repopulated with seed data every time the application starts. Existing data will be permanently lost.

```json
  "SeedData": {
    "GenerateSeedData": "true", 👈 set to true to create seed data including modules, categories, pages, users, permissions and roles.
    "GenerateSeedLogs":  "true" 👈 set to true to generate mock logs
  }
```

# Authentication
Atlas is setup to use [Auth0](https://auth0.com/) as its authentication provider, although this can be swapped out for any provider supporting **OAuth 2.0**. With [Auth0](https://auth0.com/signup?place=header&type=button&text=sign%20up) you can create a free account and it has a easy to use dashboard for registering applications, and creating and managing roles and users.

Using the [Auth0](https://auth0.com/), register the **Atlas.API** Web API and **Atlas.Blazor.Web.App** Blazor application, and  create a `atlas-user` role and users.

### Create an Auth0 Role
In the [Auth0](https://auth0.com/) dashboard create a role called `atlas-user`. This role must be assigned to all users wishing to access the Atlas application.

> [!IMPORTANT]  
> Atlas users must be assigned the `atlas-user` role in [Auth0](https://auth0.com/) to access the Atlas application.

![Alt text](/readme-images/Auth0_Role.png?raw=true "Auth0 Role") 

### Create Auth0 Users
Create Auth0 users. The user's [Auth0](https://auth0.com/) email claim is mapped to the email of an authorised user in the Atlas database.

> [!IMPORTANT]  
> Atlas users must be assigned the `atlas-user` role to access the Atlas application.

![Alt text](/readme-images/Auth0_User.png?raw=true "Auth0 User") 

![Alt text](/readme-images/Auth0_User_Role.png?raw=true "Auth0 User Role") 

> [!TIP]
> [SeedData.cs](https://github.com/grantcolley/atlas-origin/blob/dbd3270b3eb0374e8e4de1976051e9d35276bca5/data/Atlas.Seed.Data/SeedData.cs#L98-L113) already contains some pre-defined sample users with roles and permissions. Either create these users in [Auth0](https://auth0.com/), or amend the sample users in [SeedData.cs](https://github.com/grantcolley/atlas-origin/blob/dbd3270b3eb0374e8e4de1976051e9d35276bca5/data/Atlas.Seed.Data/SeedData.cs#L98-L113](https://github.com/grantcolley/atlas-origin/blob/dbd3270b3eb0374e8e4de1976051e9d35276bca5/data/Atlas.Seed.Data/SeedData.cs#L98-L113)) to reflect those created in [Auth0](https://auth0.com/).

> [!WARNING]  
> If `"GenerateSeedData": "true"` the tables in the Atlas database will be truncated and repopulated with seed data every time the application starts. Existing data will be permanently lost.

```C#
        private static void CreateUsers()
        {
            if (dbContext == null) throw new NullReferenceException(nameof(dbContext));

            users.Add("alice", new User { Name = "alice", Email = "alice@email.com" });
            users.Add("jane", new User { Name = "jane", Email = "jane@email.com" });
            users.Add("bob", new User { Name = "bob", Email = "bob@email.com" });
            users.Add("grant", new User { Name = "grant", Email = "grant@email.com" });

            foreach (User user in users.Values)
            {
                dbContext.Users.Add(user);
            }

            dbContext.SaveChanges();
        }
```



![Alt text](/readme-images/Auth0_User.png?raw=true "Auth0 Users") 

### Securing Atlas.API
The following article explains how to register and [secure a minimal WebAPI with Auth0](https://auth0.com/blog/securing-aspnet-minimal-webapis-with-auth0/) with the relevant parts in the **Atlas.API** [Program.cs](https://github.com/grantcolley/atlas-origin/blob/main/src/Atlas.API/Program.cs).

```C#

//....existing code removed for brevity

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Auth0:Domain"],
            ValidAudience = builder.Configuration["Auth0:Audience"]
        };
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy(Auth.ATLAS_USER_CLAIM, policy =>
    {
        policy.RequireAuthenticatedUser().RequireRole(Auth.ATLAS_USER_CLAIM);
    });

//....existing code removed for brevity

app.UseAuthentication();

app.UseAuthorization();

//....existing code removed for brevity

```

When mapping the minimal Web API methods add `RequireAuthorization(Auth.ATLAS_USER_CLAIM)`, as can be seen here in [AtlasEndpointMapper.cs](https://github.com/grantcolley/atlas-origin/blob/dbd3270b3eb0374e8e4de1976051e9d35276bca5/src/Atlas.API/Extensions/AtlasEndpointMapper.cs#L17).

```C#

//....existing code removed for brevity

app.MapGet($"/{AtlasAPIEndpoints.GET_CLAIM_MODULES}", ClaimEndpoint.GetClaimModules)
            .WithOpenApi()
            .WithName(AtlasAPIEndpoints.GET_CLAIM_MODULES)
            .WithDescription("Gets the user's authorized modules")
            .Produces<IEnumerable<Module>?>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization(Auth.ATLAS_USER_CLAIM);  // 👈 add RequireAuthorization to endpoints

//....existing code removed for brevity

```

### Securing Atlas.Blazor.Web.App
The following article explains how to register and [add Auth0 Authentication to Blazor Web Apps](https://auth0.com/blog/auth0-authentication-blazor-web-apps/) in .NET 8.0.

> [!WARNING]  
> .NET 9.0 simplifies this approach by providing services to serialize the authentication state on the server and deserialize the authentication state in the WebAssembly client.
>
> See this article for details explaining [Authentication State Serialization for Blazor Web Apps](https://auth0.com/blog/authentication-authorization-enhancements-in-dotnet-9/#Authentication-State-Serialization-for-Blazor-Web-Apps).
> 
> See Microsoft's documentation to [Manage authentication state in Blazor Web Apps](https://learn.microsoft.com/en-us/aspnet/core/blazor/security/?view=aspnetcore-9.0&tabs=visual-studio#manage-authentication-state-in-blazor-web-apps).

Here are the relevant parts in the **Atlas.Blazor.Web.App** [Program.cs](https://github.com/grantcolley/atlas-origin/blob/main/src/Atlas.Blazor.Web.App/Program.cs).

```C#

//....existing code removed for brevity

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization();  // 👈 adds the services to serialize the authentication state on the server

//....existing code removed for brevity

builder.Services
    .AddAuth0WebAppAuthentication(Auth0Constants.AuthenticationScheme, options =>
    {
        options.Domain = builder.Configuration["Auth0:Domain"] ?? throw new NullReferenceException("Auth0:Domain");
        options.ClientId = builder.Configuration["Auth0:ClientId"] ?? throw new NullReferenceException("Auth0:ClientId");
        options.ClientSecret = builder.Configuration["Auth0:ClientSecret"] ?? throw new NullReferenceException("Auth0:ClientSecret");
        options.ResponseType = "code";
    }).WithAccessToken(options =>
    {
        options.Audience = builder.Configuration["Auth0:Audience"] ?? throw new NullReferenceException("Auth0:Audience");
    });

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<TokenHandler>();

//....existing code removed for brevity

app.MapGet("login", async (HttpContext httpContext, string redirectUri = @"/") =>
{
    AuthenticationProperties authenticationProperties = new LoginAuthenticationPropertiesBuilder()
            .WithRedirectUri(redirectUri)
            .Build();

    await httpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
});

app.MapGet("logout", async (HttpContext httpContext, string redirectUri = @"/") =>
{
    AuthenticationProperties authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
            .WithRedirectUri(redirectUri)
            .Build();

    await httpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
    await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
});

//....existing code removed for brevity

```

Here is the relevant part in the **Atlas.Blazor.Web.Client** [Program.cs](https://github.com/grantcolley/atlas-origin/blob/main/src/Atlas.Blazor.Web.App/Program.cs).

```C#
WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddFluentUIComponents();

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization(); // 👈 adds the services to deserialize the authentication state in the WebAssembly client

await builder.Build().RunAsync();
```

Finally, the following article describes how to [call protected APIs from a Blazor Web App](https://auth0.com/blog/call-protected-api-from-blazor-web-app/), including [calling external APIs](https://auth0.com/blog/call-protected-api-from-blazor-web-app/#Call-an-External-API) which requires injecting the access token into HTTP requests. This is handled by the [TokenHandler.cs](https://github.com/grantcolley/atlas-origin/blob/main/src/Atlas.Blazor.Web.App/Authentication/TokenHandler.cs).

```C#
    public class TokenHandler(IHttpContextAccessor httpContextAccessor) : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if(_httpContextAccessor.HttpContext == null)  throw new NullReferenceException(nameof(_httpContextAccessor.HttpContext));

            string? accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token").ConfigureAwait(false);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            return await base.SendAsync(request, cancellationToken);
        }
    }
```

### Log In
Clicking the `Login` button on the top right corner of the application will re-direct the user to the [Auth0](https://auth0.com/) login page. Once authenticated, the user is directed back to the application, and the navigation panel will display the Modules, Categories and Pages the user has permission to access.

Click the `Login` button on the top right corner to be redirected to the [Auth0](https://auth0.com/) login page.

![Alt text](/readme-images/Login.png?raw=true "Login") 

Authenticate in [Auth0](https://auth0.com/).

![Alt text](/readme-images/Auth0_Login.png?raw=true "Login") 

The [Auth0](https://auth0.com/) callback redirects the authenticated user back to **Atlas**, and the navigation panel will display the Modules, Categories and Pages the user has permission to access.

![Alt text](/readme-images/Logged_in.png?raw=true "Logged in") 

# Authorization
### Users, Roles and Permissions
**Atlas** users are maintained in the Atlas database. The user's email in the **Atlas** database corresponds to the email claim provided by [Auth0](https://auth0.com/) to authenticated users. When a user is authenticated, a lookup is done in the **Atlas** database to get the users roles and permissions. This will determine which modules, categories and pages the user has access to in the navigation panel. It will also provide more granular permissions in each rendered page e.g. read / write.

Creating, updating and deleting **Atlas** users, roles and permissions, is done in the `Authorisation` category of the `Administration` module.

> [!TIP]
> The `Authorisation` category of the `Administration` module is only accessible to users who are members of the `Admin-Read Role` and `Admin-Write Role`.
> The `Admin-Read Role` gives read-only view of users, roles and permissions, while the `Admin-Write Role` permit creating, updating and deleting them.

![Alt text](/readme-images/Users.png?raw=true "Users") 

Here we see user Bob is assignd the roles `Support Role` and `User Role`.

![Alt text](/readme-images/User_Bob.png?raw=true "Bob the Support user") 

Here we see the role `Support Role`, the list of permissions it has been granted, and we can see Bob is a member of the role.   

![Alt text](/readme-images/Role_Support.png?raw=true "The Support role") 

Here we see the permission `Support`, and the roles that have been granted the `Support` permission.

![Alt text](/readme-images/Permission_Support.png?raw=true "The Support permission") 

# Navigation
### Modules, Categories and Pages
Modules are applications, and can be related or unrelated to each other. Each module consists of one or more categories. Each category groups related pages. A page is a routable razor `@page`.

Creating, updating and deleting modules, categories and pages, is done in the `Applications` category of the `Administration` module.

> [!TIP]
> Because each page must point to a routable razor `@page`, the `Applications` category of the `Administration` module is only accessible to users who are members of the `Developer Role`.
> i.e. creating, updating and deleting modules, categories and pages is a developer concern.

Each module, category and page in the Navigation panel has a permission, and are only accessible to users who have been assigned that permission via role membership.

![Alt text](/readme-images/Modules.png?raw=true "Modules") 

Here we see the `Support` module, the order it appears in the navigation panel, the permission required for it to appear in the navigation panel, and the icon that is displayed with it in the navigation panel. We see it has an `Events` category. We can also see highlighted in yellow how it appears in the navigation panel.

![Alt text](/readme-images/Module_Support.png?raw=true "Support module") 

Here we see the `Events` category, the module it belongs to, the order it appears under the module in the navigation panel, the permission required for it to appear in the navigation panel, and the icon that is displayed with it in the navigation panel. We also see it has a page called `Logs`.

![Alt text](/readme-images/Category_Events.png?raw=true "Events category") 

Here we see the `Logs` page, the category it belongs to, the order it appears under the category in the navigation panel, the permission required for it to appear in the navigation panel, and the icon that is displayed with it in the navigation panel. Crucially, we also see the route, which is the routable razor `@page` that it navigates to when the user clicks the page in the navigation panel.

![Alt text](/readme-images/Page_Logs.png?raw=true "Logs page") 

Here we can see the [Logs.razor](https://github.com/grantcolley/atlas-origin/blob/e29333f96d3ea12e16f193b71913863001596c14/src/Atlas.Blazor.Web/Components/Pages/Support/Logs.razor#L1) component, with its routable `@page`  attribute.

> [!IMPORTANT]  
> The route specified in the page must map to a valid `@page` attribute on a routable component.

```HTML+Razor
@page "/Logs"
@using System.Text.Json
@rendermode @(new InteractiveServerRenderMode(prerender: false))
@attribute [StreamRendering]

<PageTitle>Logs</PageTitle>

@if (_alert == null)
{
    <FluentCard>
        <FluentHeader>
            Logs
        </FluentHeader>

<!-- code removed for brevity -->

```

# Support

> [!TIP]
> The Support module, its categories and routable pages, are only accessible to users who are members of the `Support Role`.

> [!NOTE]
> Members of the `Support Role` also have `Admin-Read` and `Admin-Write` permissions, permitting them to add, update and delete users.

### Logging
Logs are persisted to the `Logs` table in the **Atlas** database and are viewable to members of the `Support Role`. 

Here we can see mock logs created at startup when `"GenerateSeedLogs":  "true"` is set in the **Atlas.API**'s [appsettings.json](https://github.com/grantcolley/atlas-origin/blob/e29333f96d3ea12e16f193b71913863001596c14/src/Atlas.API/appsettings.json#L53).

![Alt text](/readme-images/Logs.png?raw=true "Logs") 

Clicking on the log entry will display the full log details in a popup box. 

![Alt text](/readme-images/Log_Dialog.png?raw=true "Log dialog") 

# Audit
The [ApplicationDbContext.cs](https://github.com/grantcolley/atlas-origin/blob/056f5f9154943ed837dcd1f3f912281228828f04/src/Atlas.Data.Access/Context/ApplicationDbContext.cs#L79-L226) uses EF Change Tracking to capture `OldValue` and `NewValues` from `INSERT`'s, `UPDATE`'s and `DELETE`'s, for entities where their poco model class inherits from [ModelBase.cs](https://github.com/grantcolley/atlas-origin/blob/main/src/Atlas.Core/Models/ModelBase.cs). Tracked changes can be queried in the **Audit** table of the **Atlas** database.

![Alt text](/readme-images/Audits.png?raw=true "Audits") 

More can be read here about change tracking in Entity Framework:
- [Change Tracking in EF Core](https://learn.microsoft.com/en-us/ef/core/change-tracking/)
- [Change Detection and Notifications](https://learn.microsoft.com/en-us/ef/core/change-tracking/change-detection)
- [Tracking Changes of Entities in EF Core](https://www.entityframeworktutorial.net/efcore/changetracker-in-ef-core.aspx)

## Publish Atlas to Azure
First, create an Azure account.

#### Steps to Publish Atlas to Azure
#### Resource Group
Create a **Resource Group** called `atlas-rg` and select an appropriate **Region**.

![Alt text](/readme-images/Azure/Create_ResourceGroup.png?raw=true "Create a Resource Group")

#### Web App + Database
In the **Resource Group**, create a **Web App + Database** called `atlas-web-api`.
- Select the same **Region** as the **Resource Group**.
- Select the **Runtime stack**.
- Stick with the `SQLAzure` **Engine**, and default server and database names provided. 
- For the **Hosting plan**, select `Basic - For hobby or research purposes`.

![Alt text](/readme-images/Azure/Create_WebAppAndDatabase.png?raw=true "Create WebApp + Database")

#### Web App
In the **Resource Group**, create a **Web App** called `atlas-blazor`.
- Select the **Runtime stack**.
- For **Operating system** select `Linux`.
- Select the same **Region** as the **Resource Group**.
- Skip **Database**, in the **Deployment** section ensure **Basic authentication** is selected, and keep the rest of the default settings.

![Alt text](/readme-images/Azure/Create_WebApp_Blazor.png?raw=true "Create WebApp - Blazor")

#### Environment Variables
Go to `atlas-web-api` resource.
For this demo we will use the config in the `appsettings.Production.json` file for the **Atlas.API** and **Atlas.Blazor.Web.App** projects.
- In **Environment variables** -> **Connection strings**, copy the connection string from the environment variable `AZURE_SQL_CONNECTIONSTRING`, and paste it into `AtlasConnection` in the `appsettings.Production.json` file for both projects.  
- Delete environment variable `AZURE_SQL_CONNECTIONSTRING`. 
- Click **Apply** -> **Confirm**

#### Auth0
Login to `Auth0` and:
- add the `atlas-blazor` url to `Allowed Callback URLs` e.g. `{atlas-blazor url here...}/callback`
- add the `atlas-blazor` url to `Allowed Logout URLs` e.g. `{atlas-blazor url here...}/`

![Alt text](/readme-images/Azure/auth0-application-uris.png?raw=true "Auth0 Application URIs") 

#### Update Web API Configuration and Publish to Azure
In the **Atlas.API** project's `appsettings.Production.json` update the following:
```json
{
  "ConnectionStrings": {
    "AtlasConnection": "" // 👈 connection string to atlas-web-api-database
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore.Database.Command": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Auth0": {
    "Domain": "", // 👈 Auth0 domain
    "Audience": "https://Atlas.API.com"
  },
  "CorsOrigins": {
    "Policy": "atlas-cors-policy",
    "Urls": "" // 👈 url to atlas-blazor app 
  },
  "Database": {
    "MigrateDatabase": "true",
    "CreateDatabase": "true",
    "GenerateSeedData": "true",
    "GenerateSeedLogs": "false"
  }
}
```

- Right click the project and select **Publish**.
- For **Target** select `Azure`. 
- For **Specific target** select `Azure App Service (Linux)`.
- For **App Service** select `atlas-web-api`.
- For **API Management** tick `Skip this step`.
- For **Deployment type** select `Publish (generates pubxml file)`.
- In the projects **Properties** -> **PublishProfiles** folder, add the following to `atlas-web-api - Zip Deploy.pubxml`.
```xml
  <ItemGroup>
    <Content Update="appsettings.json">
     <CopyToPublishDirectory>Never</CopyToPublishDirectory>
     <CopyToOutputDirectory>Never</CopyToOutputDirectory>
    </Content>	  
    <Content Update="appsettings.Development.json">
     <CopyToPublishDirectory>Never</CopyToPublishDirectory>
     <CopyToOutputDirectory>Never</CopyToOutputDirectory>
    </Content>
  </ItemGroup>
```

In the Publish tab click the publish button. This will build and publish **Atlas.API** to `atlas-web-api`. A new tab will open in your default browser for the running `atlas-web-api`. Append `/swagger/index.html` to the url and hit enter.

![Alt text](/readme-images/Azure/atlas-web-api-swagger.png?raw=true "Atlas Web API Swagger")

#### Update Blazor Web App Configuration and Publish to Azure
7. In the **Atlas.Blazor.Web.App** project's `appsettings.Production.json` update the following:
```json
{
  "ConnectionStrings": {
    "AtlasConnection": "" // 👈 connection string to atlas-web-api-database
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore.Database.Command": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Auth0": {
    "Domain": "", // 👈 Auth0 domain
    "ClientId": "", // 👈 Auth0 ClientId
    "ClientSecret": "", // 👈 Auth0 ClientSecret
    "Audience": "https://Atlas.API.com"
  },
  "AtlasAPI": "" // 👈 url to atlas-web-api 
}
```
- Right click the project and select **Publish**.
- For **Target** select `Azure`. 
- For **Specific target** select `Azure App Service (Linux)`.
- For **App Service** select `atlas-blazor`.
- For **Deployment type** select `Publish (generates pubxml file)`.
- In the projects **Properties** -> **PublishProfiles** folder, add the following to `atlas-blazor - Zip Deploy.pubxml`.
```xml
  <ItemGroup>
    <Content Update="appsettings.json">
     <CopyToPublishDirectory>Never</CopyToPublishDirectory>
     <CopyToOutputDirectory>Never</CopyToOutputDirectory>
    </Content>	  
    <Content Update="appsettings.Development.json">
     <CopyToPublishDirectory>Never</CopyToPublishDirectory>
     <CopyToOutputDirectory>Never</CopyToOutputDirectory>
    </Content>
  </ItemGroup>
```

In the Publish tab click the publish button. This will build and publish **Atlas.Blazor.Web.App** to `atlas-blazor`. A new tab will open in your default browser for the running `atlas-blazor`.

![Alt text](/readme-images/Azure/atlas-blazor-login.png?raw=true "Atlas Blazor Login")

Click `login` to be re-directed to `Auth0` for authentication.

![Alt text](/readme-images/Azure/Auth0_Login_Blank.png?raw=true "Auth0 Login")

`Auth0` will re-direct bck to the `callback` page.

![Alt text](/readme-images/Azure/atlas-blazor-loggedin.png?raw=true "Atlas Web API Swagger")

# Worked Examples
## Blazor Template
Create a Blazor Template module for the standard template WeatherForecast and Counter pages.

1. Add a new permission constant in **Atlas.Core**'s `Auth` class.
```C#
    public static class Auth
    {
        public const string ATLAS_USER_CLAIM = "atlas-user";
        public const string ADMIN_READ = "Admin-Read";
        public const string ADMIN_WRITE = "Admin-Write";
        public const string DEVELOPER = "Developer";
        public const string SUPPORT = "Support";
        public const string BLAZOR_TEMPLATE = "Blazor-Template"; // 👈 new Blazor-Template permission 
    }
```

2. Create a new `WeatherForecast` class in **Atlas.Core**'s `Models` folder.
```C#
    public class WeatherForecast
    {
        public DateOnly Date { get; set; }
        public int TemperatureC { get; set; }
        public string? Summary { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
```

3. Create a new `WeatherEndpoints` class in **Atlas.API**'s `Endpoints` folder.
```C#
    public class WeatherEndpoints
    {
        internal static async Task<IResult> GetWeatherForecast(IClaimData claimData, IClaimService claimService, ILogService logService, CancellationToken cancellationToken)
        {
            Authorisation? authorisation = null;

            try
            {
                authorisation = await claimData.GetAuthorisationAsync(claimService.GetClaim(), cancellationToken)
                    .ConfigureAwait(false);

                if (authorisation == null
                    || !authorisation.HasPermission(Auth.BLAZOR_TEMPLATE))
                {
                    return Results.Unauthorized();
                }

                var startDate = DateOnly.FromDateTime(DateTime.Now);
                var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
                IEnumerable<WeatherForecast> forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = startDate.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = summaries[Random.Shared.Next(summaries.Length)]
                });

                return Results.Ok(forecasts);
            }
            catch (AtlasException ex)
            {
                logService.Log(Core.Logging.Enums.LogLevel.Error, ex.Message, ex, authorisation?.User);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
```

4. Add a new constant for the weather forecast endpoint in **Atlas.Core**'s `AtlasAPIEndpoints` class.
```C#
    public static class AtlasAPIEndpoints
    {
        // existing code removed for brevity

        public const string GET_WEATHER_FORECAST = "getweatherforecast"; // 👈 new getweatherforecast endpoint constant 
    }
```

5. Map the weather forecast endpoint in **Atlas.API**'s `ModulesEndpointMapper` class.
```C#
    internal static class ModulesEndpointMapper
    {
        internal static WebApplication? MapAtlasModulesEndpoints(this WebApplication app)
        {
            // Additional module API's mapped here...

            app.MapGet($"/{AtlasAPIEndpoints.GET_WEATHER_FORECAST}", WeatherEndpoints.GetWeatherForecast)
                .WithOpenApi()
                .WithName(AtlasAPIEndpoints.GET_WEATHER_FORECAST)
                .WithDescription("Gets the weather forecast")
                .Produces<IEnumerable<WeatherForecast>?>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization(Auth.ATLAS_USER_CLAIM);

            return app;
        }
    }
```

6. Create a new interface `IWeatherForecastRequests` in **Atlas.Requests**'s `Interfaces` folder.
```C#
    public interface IWeatherForecastRequests
    {
        Task<IEnumerable<WeatherForecast>?> GetWeatherForecastAsync();
    }
```

7. Create a new class `WeatherForecastRequests` in **Atlas.Requests**'s `API` folder.
```C#
    public class WeatherForecastRequests(HttpClient httpClient) : IWeatherForecastRequests
    {
        protected readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        protected readonly JsonSerializerOptions _jsonSerializerOptions = new(JsonSerializerDefaults.Web);

        public async Task<IEnumerable<WeatherForecast>?> GetWeatherForecastAsync()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<WeatherForecast>?>
                (await _httpClient.GetStreamAsync(AtlasAPIEndpoints.GET_WEATHER_FORECAST)
                .ConfigureAwait(false), _jsonSerializerOptions).ConfigureAwait(false);
        }
    }
```

8. Register the `WeatherForecastRequests` service in **Atlas.Blazor.Web.App**'s `Program` file.
```C#
   // existing code removed for brevity
   
   builder.Services.AddTransient<IWeatherForecastRequests, WeatherForecastRequests>(sp =>
   {
       IHttpClientFactory httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
       HttpClient httpClient = httpClientFactory.CreateClient(AtlasWebConstants.ATLAS_API);
       return new WeatherForecastRequests(httpClient);
   });

   WebApplication app = builder.Build();

   // existing code removed for brevity
```

9. Create a new Blazor `Weather` component in **Atlas.Blazor.Web**'s `/Components/Pages` folder
```HTML+Razor
@page "/Weather"
@attribute [StreamRendering]
@rendermode @(new InteractiveServerRenderMode(prerender: false))

<PageTitle>Weather</PageTitle>

<FluentLabel Typo="Typography.PageTitle">Weather</FluentLabel>

<br>

<FluentLabel Typo="Typography.PaneHeader">This component demonstrates showing data.</FluentLabel>

<br>

@if (Forecasts == null)
{
    <FluentLabel Typo="Typography.Subject">Loading...</FluentLabel>
}
else
{
    <FluentDataGrid TGridItem=Atlas.Core.Models.WeatherForecast Items="@Forecasts"
                     Style="height: 600px;overflow:auto;" GridTemplateColumns="0.25fr 0.25fr 0.25fr 0.25fr"
                     ResizableColumns=true GenerateHeader="GenerateHeaderOption.Sticky">
        <PropertyColumn Property="@(f => f.Date.ToShortDateString())" Title="Date" Sortable="true" />
        <PropertyColumn Property="@(f => f.TemperatureC)" Sortable="true" />
        <PropertyColumn Property="@(f => f.TemperatureF)" Sortable="true" />
        <PropertyColumn Property="@(f => f.Summary)" Sortable="true" />
    </FluentDataGrid>
}

@code {
    [Inject]
    public IWeatherForecastRequests? WeatherForecastRequests { get; set; }

    private IEnumerable<WeatherForecast>? _forecasts;

    public IQueryable<WeatherForecast>? Forecasts
    {
        get
        {
            return _forecasts?.AsQueryable();
        }
    }

    protected override async Task OnInitializedAsync()
    {
        if (WeatherForecastRequests == null) throw new NullReferenceException(nameof(WeatherForecastRequests));

        _forecasts = await WeatherForecastRequests.GetWeatherForecastAsync().ConfigureAwait(false);
    }
}
```

10. Create a new Blazor `Counter` component in **Atlas.Blazor.Web**'s `/Components/Pages` folder.
```HTML+Razor
@page "/Counter"
@rendermode InteractiveAuto

<PageTitle>Counter</PageTitle>

<FluentLabel Typo="Typography.PageTitle">Counter</FluentLabel>

<br>

<FluentLabel Typo="Typography.PaneHeader">Current count: @currentCount</FluentLabel>

<br>

<FluentButton Appearance="Appearance.Accent" OnClick="@IncrementCount">Click me</FluentButton>

@code {
    private int currentCount = 0;

    private void IncrementCount()
    {
        currentCount++;
    }
}
```

11. Create a new user `will@email.com` in Auth0 and assign the user the `atlas-user` role.
![Alt text](/readme-images/BlazorTemplate/Blazor_Template_Auth0_User.png?raw=true "Blazor Template Auth0 New User")

12. Create the permission `Blazor-Template`.
![Alt text](/readme-images/BlazorTemplate/Blazor_Template_Permission.png?raw=true "Blazor-Template permission")

13. Create the role `Blazor Template Role` and assign it the `Blazor-Template` permission.
![Alt text](/readme-images/BlazorTemplate/Blazor_Template_Role.png?raw=true "Blazor Template role")

14. Create the user `will@email.com` and assign the `Blazor Template Role` role.
![Alt text](/readme-images/BlazorTemplate/Blazor_Template_User.png?raw=true "Blazor Template user")

15. Create the module `Blazor Template`.
![Alt text](/readme-images/BlazorTemplate/Blazor_Template_Module.png?raw=true "Blazor Template module")

16. Create the category `Templates` and set the Module to `Blazor Template`.
![Alt text](/readme-images/BlazorTemplate/Blazor_Template_Category.png?raw=true "Blazor Template category")

17. Create the page `Weather` and set the Category to `Templates`.
![Alt text](/readme-images/BlazorTemplate/Blazor_Template_Page_Weather.png?raw=true "Blazor Template weather")

18. Create the page `Counter` and set the Category to `Templates`.
![Alt text](/readme-images/BlazorTemplate/Blazor_Template_Page_Counter.png?raw=true "Blazor Template counter")

19. Log out, then log back in as user `will@email.com`
![Alt text](/readme-images/BlazorTemplate/Blazor_Template_Auth0_Login.png?raw=true "Blazor Template Auth0 login")

20. In the navigation panel, user `will@email.com` only has permission to the Blazor Template module.
![Alt text](/readme-images/BlazorTemplate/Blazor_Template_Home.png?raw=true "Blazor Template Home")

21. Click on the Weather navigation link.
![Alt text](/readme-images/BlazorTemplate/Blazor_Template_Component_Weather.png?raw=true "Blazor Template Weather")

22. Click on the Counter navigation link.
![Alt text](/readme-images/BlazorTemplate/Blazor_Template_Component_Counter.png?raw=true "Blazor Template Counter")

# Notes
### FluentDesignTheme Dark/Light
What the [Fluent UI quick guide](https://fluentui-blazor.net/DesignTheme) doesn't tell you is you must also add a reference to `/_content/Microsoft.FluentUI.AspNetCore.Components/css/reboot.css`.

For the Blazor Web App project, add the reference to the top of the `app.css` file in `wwwroot`:
```C#
@import '/_content/Microsoft.FluentUI.AspNetCore.Components/css/reboot.css';
```

For the Blazor WebAssembly stand alone project, add the reference to the `index.html` file in `wwwroot`.
```C#
<Link href="_content/Microsoft.FluentUI.AspNetCore.Components/css/reboot.css" rel="stylesheet" />
```

# Notes
### FluentDesignTheme Dark/Light
What the [Fluent UI quick guide](https://fluentui-blazor.net/DesignTheme) doesn't tell you is you must also add a reference to `/_content/Microsoft.FluentUI.AspNetCore.Components/css/reboot.css`.

For the Blazor Web App project, add the reference to the top of the `app.css` file in `wwwroot`:
```C#
@import '/_content/Microsoft.FluentUI.AspNetCore.Components/css/reboot.css';
```

For the Blazor WebAssembly stand alone project, add the reference to the `index.html` file in `wwwroot`.
```C#
<Link href="_content/Microsoft.FluentUI.AspNetCore.Components/css/reboot.css" rel="stylesheet" />
```

### Backend for frontend
- [Calling a Protected API from a Blazor Web App](https://auth0.com/blog/call-protected-api-from-blazor-web-app/)
- [Backend for Frontend (BFF)](https://datatracker.ietf.org/doc/html/draft-ietf-oauth-browser-based-apps#name-backend-for-frontend-bff)
- [Microsoft Architecture Patterns - Backends for Frontends](https://learn.microsoft.com/en-us/azure/architecture/patterns/backends-for-frontends)


