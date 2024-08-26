using Atlas.Core.Models;
using Origin.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Origin.Model
{
    public abstract class DocumentContentPropertiesBase : ModelBase, IDocumentContentProperties
    {
        public int? FontSize { get; set; }
        public bool? Bold { get; set; }
        public bool? Italic { get; set; }
        public bool? Underscore { get; set; }

        [StringLength(100)]
        public string? Font { get; set; }

        /// <summary>
        /// RGB 0-255 e.g. 0,176,240
        /// </summary>
        [StringLength(11)]
        public string? Colour { get; set; }
    }
}
