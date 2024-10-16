using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Origin.Core.Models;
using Origin.Core.Validation.Validators;

namespace Origin.Core.Validation.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOriginValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<DocumentColour>, DocumentColourValidator>();
            services.AddScoped<IValidator<DocumentFont>, DocumentFontValidator>();
            services.AddScoped<IValidator<DocumentParagraphProperties>, DocumentParagraphPropertiesValidator>();
            services.AddScoped<IValidator<DocumentContentProperties>, DocumentContentPropertiesValidator>();
            services.AddScoped<IValidator<DocumentSubstitute>, DocumentSubstituteValidator>();
            services.AddScoped<IValidator<DocumentTableCell>, DocumentTableCellValidator>();
            services.AddScoped<IValidator<DocumentContent>, DocumentContentValidator>();
            services.AddScoped<IValidator<DocumentConfig>, DocumentConfigValidator>();
            services.AddScoped<IValidator<DocumentParagraph>, DocumentParagraphValidator>();
            return services;
        }
    }
}
