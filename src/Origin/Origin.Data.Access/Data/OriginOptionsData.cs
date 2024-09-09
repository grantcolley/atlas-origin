using Atlas.Core.Constants;
using Atlas.Core.Exceptions;
using Atlas.Core.Models;
using Atlas.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Origin.Core;
using Atlas.Core.Extensions;
using Origin.Core.Models;
using Origin.Data.Access.Interfaces;
using System.Text.Json;

namespace Origin.Data.Access.Data
{
    public class OriginOptionsData : AuthorisationData<OriginOptionsData>, IOriginOptionsData
    {
        private readonly Dictionary<string, Func<IEnumerable<OptionsArg>, CancellationToken, Task<IEnumerable<OptionItem>>>> optionItems = [];
        private readonly Dictionary<string, Func<IEnumerable<OptionsArg>, CancellationToken, Task<string>>> genericOptionItems = [];

        public OriginOptionsData(ApplicationDbContext applicationDbContext, ILogger<OriginOptionsData> logger)
            : base(applicationDbContext, logger)
        {
            // Add methods returing IEnumerable<OptionItem> here...
            // optionItems[OriginOptions.MY_OPTION_ITEMS] = new Func<IEnumerable<OptionsArg>, CancellationToken, Task<IEnumerable<OptionItem>>>(GetMyOptionItemsAsync);

            // Add methods returing Json here...
            genericOptionItems[OriginOptions.FONT_OPTIONS] = new Func<IEnumerable<OptionsArg>, CancellationToken, Task<string>>(GetDocumentFontsAsync);
        }

        public async Task<IEnumerable<OptionItem>> GetOptionsAsync(IEnumerable<OptionsArg> optionsArgs, CancellationToken cancellationToken)
        {
            string? optionsCode = optionsArgs.FirstOptionsArgValue(Options.OPTIONS_CODE);

            if (string.IsNullOrWhiteSpace(optionsCode)) throw new AtlasException($"{nameof(optionsCode)} is null", new NullReferenceException(nameof(optionsCode)));

            if (optionItems.TryGetValue(optionsCode, out Func<IEnumerable<OptionsArg>, CancellationToken, Task<IEnumerable<OptionItem>>>? value))
            {
                try
                {
                    return await value.Invoke(optionsArgs, cancellationToken)
                        .ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    throw new AtlasException(ex.Message, ex, $"OptionsCode={optionsCode}");
                }
            }

            throw new AtlasException($"{optionsCode} not found", new NotImplementedException(optionsCode), $"OptionsCode={optionsCode}");
        }

        public async Task<string> GetGenericOptionsAsync(IEnumerable<OptionsArg> optionsArgs, CancellationToken cancellationToken)
        {
            string? optionsCode = optionsArgs.FirstOptionsArgValue(Options.OPTIONS_CODE);

            if (string.IsNullOrWhiteSpace(optionsCode)) throw new AtlasException($"{nameof(optionsCode)} is null", new NullReferenceException(nameof(optionsCode)));

            if (genericOptionItems.TryGetValue(optionsCode, out Func<IEnumerable<OptionsArg>, CancellationToken, Task<string>>? value))
            {
                try
                {
                    return await value.Invoke(optionsArgs, cancellationToken)
                        .ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    throw new AtlasException(ex.Message, ex, $"OptionsCode={optionsCode}");
                }
            }

            throw new AtlasException($"{optionsCode} not found", new NotImplementedException(optionsCode), $"OptionsCode={optionsCode}");
        }

        private async Task<string> GetDocumentFontsAsync(IEnumerable<OptionsArg> optionsArgs, CancellationToken cancellationToken)
        {
            List<DocumentFont> documentFonts = await _applicationDbContext.DocumentFonts
                .AsNoTracking()
                .OrderBy(f => f.Font)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            if (documentFonts.Count > 0)
            {
                documentFonts.Insert(0, new DocumentFont { DocumentFontId = -1 });
            }

            return JsonSerializer.Serialize(documentFonts);
        }
    }
}
