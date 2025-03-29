using Atlas.API.Extensions;
using Atlas.API.Interfaces;
using Atlas.API.Services;
using Atlas.Core.Constants;
using Atlas.Core.Models;
using Atlas.Core.Validation.Extensions;
using Atlas.Data.Access.EF.Context;
using Atlas.Data.Access.EF.Data;
using Atlas.Data.Access.Interfaces;
using Atlas.Logging.Interfaces;
using Atlas.Logging.Serilog.Services;
using Commercial.Data.Access.Data;
using Commercial.Data.Access.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Origin.Core.Validation.Extensions;
using Origin.Data.Access.Data;
using Origin.Data.Access.Interfaces;
using Origin.Generator.PdfSharp.Services;
using Origin.Service.Extensions;
using Origin.Service.Interface;
using Origin.Service.Providers;
using Origin.Service.Services;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Data;
using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

string? connectionString = builder.Configuration.GetConnectionString(Config.CONNECTION_STRING) ?? throw new NullReferenceException(Config.CONNECTION_STRING);
string? domain = builder.Configuration[Config.AUTH_DOMAIN] ?? throw new NullReferenceException(Config.AUTH_DOMAIN);
string? audience = builder.Configuration[Config.AUTH_AUDIENCE] ?? throw new NullReferenceException(Config.AUTH_AUDIENCE);
string? corsPolicy = builder.Configuration[Config.CORS_POLICY] ?? throw new NullReferenceException(Config.CORS_POLICY);
string? originUrls = builder.Configuration[Config.ORIGINS_URLS] ?? throw new NullReferenceException(Config.ORIGINS_URLS);
bool createDatabase = bool.Parse(builder.Configuration[Config.DATABASE_CREATE] ?? "false");
bool seedData = bool.Parse(builder.Configuration[Config.DATABASE_SEED_DATA] ?? "false");
bool seedLogs = bool.Parse(builder.Configuration[Config.DATABASE_SEED_LOGS] ?? "false");

AtlasConfig atlasConfig = new()
{
    DatabaseCreate = createDatabase,
    DatabaseSeedData = seedData,
    DatabaseSeedLogs = seedLogs
};

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
              loggerConfiguration
              .MinimumLevel.Information()
              .Enrich.FromLogContext()
              .WriteTo.MSSqlServer(
                  connectionString: connectionString,
                  sinkOptions: new MSSqlServerSinkOptions
                  {
                      TableName = "Logs",
                      AutoCreateSqlDatabase = false
                  },
                  columnOptions: new ColumnOptions
                  {
                      AdditionalColumns =
                      [
                          new SqlColumn {ColumnName = "User", PropertyName = "User", DataType = SqlDbType.NVarChar, DataLength = 450},
                          new SqlColumn {ColumnName = "Context", PropertyName = "Context", DataType = SqlDbType.NVarChar, DataLength = 450},
                      ]
                  }));

builder.Services.AddAtlasValidators();
builder.Services.AddOriginValidators();
builder.Services.AddOriginAdditionalAssemblies([typeof(PdfDocumentGenerator).Assembly]);

builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Atlas.API", Version = "v1" });
    });

builder.Services.AddHealthChecks();

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.IgnoreReadOnlyFields = true;
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (connectionString.Contains(DataMigrations.SQLITE_DATABASE))
    {
        options.EnableSensitiveDataLogging()
                .UseSqlite(connectionString, x => x.MigrationsAssembly(DataMigrations.SQLITE_MIGRATIONS));
    }
    else
    {
        options.EnableSensitiveDataLogging()
                .UseSqlServer(connectionString, x => x.MigrationsAssembly(DataMigrations.SQLSERVER_MIGRATIONS));
    }
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton(atlasConfig);
builder.Services.AddScoped<IClaimData, ClaimData>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<IClaimService, ClaimService>();
builder.Services.AddScoped<IDeveloperData, DeveloperData>();
builder.Services.AddScoped<ISupportData, SupportData>();
builder.Services.AddScoped<IOptionsData, OptionsData>();
builder.Services.AddScoped<IApplicationData, ApplicationData>();
builder.Services.AddScoped<IAdministrationData, AdministrationData>();
builder.Services.AddScoped<IUserAuthorisationData, UserAuthorisationData>();
builder.Services.AddScoped<IDocumentData, DocumentData>();
builder.Services.AddScoped<IDocumentPropertiesData, DocumentPropertiesData>();
builder.Services.AddScoped<IOriginOptionsData, OriginOptionsData>();
builder.Services.AddScoped<IDocumentGeneratorProvider, DocumentGeneratorProvider>();
builder.Services.AddScoped<IDocumentService<byte[]>, DocumentMemoryService>();
builder.Services.AddScoped<ICommercialData, CommercialData>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = $"https://{domain}";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = domain,
            ValidAudience = audience
        };
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy(Auth.ATLAS_USER_CLAIM, policy =>
    {
        policy.RequireAuthenticatedUser().RequireRole(Auth.ATLAS_USER_CLAIM);
    })
    .AddPolicy(Auth.ATLAS_DEVELOPER_CLAIM, policy =>
    {
        policy.RequireAuthenticatedUser().RequireRole(Auth.ATLAS_DEVELOPER_CLAIM);
    });

if (!string.IsNullOrWhiteSpace(corsPolicy)
    && !string.IsNullOrWhiteSpace(originUrls))
{
    builder.Services.AddCors(options =>
    {
        string[] urls = originUrls.Split(';');

        options.AddPolicy(corsPolicy,
            builder =>
                builder.WithOrigins(urls)
                .AllowAnyHeader());
    });
}

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();

if (!string.IsNullOrWhiteSpace(corsPolicy))
{
    app.UseCors(corsPolicy);
}

app.UseAuthentication();

app.UseAuthorization();

app.MapEndpoints();

app.Run();