using Origin.Core.Models;
using Origin.Service.Interface;
using System.Reflection;

namespace Origin.Service.Providers
{
    public class DocumentGeneratorProvider : IDocumentGeneratorProvider
    {
        private readonly Dictionary<DocumentGeneratorType, IDocumentGenerator> _documentGeneratorCache = [];

        public DocumentGeneratorProvider()
        {
            IEnumerable<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !string.IsNullOrWhiteSpace(a.FullName) && !a.FullName.StartsWith("Microsoft") && !a.FullName.StartsWith("System"))
                .ToList();

            foreach (Assembly assembly in assemblies)
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
                List<IDocumentGenerator> documentGenerators = assembly.GetTypes()
                    .Where(type => typeof(IDocumentGenerator).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                    .Select(type => (IDocumentGenerator)Activator.CreateInstance(type))
                    .ToList();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                foreach (IDocumentGenerator documentGenerator in documentGenerators)
                {
                    _ = _documentGeneratorCache.TryAdd(documentGenerator.DocumentGeneratorType, documentGenerator);
                }
            }
        }

        public IDocumentGenerator GetDocumentGenerator(DocumentGeneratorType documentServiceType)
        {
            if (documentServiceType == DocumentGeneratorType.None) throw new NotSupportedException($"{DocumentGeneratorType.None}");

            return _documentGeneratorCache[documentServiceType];
        }
    }
}
