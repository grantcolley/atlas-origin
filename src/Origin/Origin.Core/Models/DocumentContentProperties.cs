using Atlas.Core.Models;
using Origin.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Origin.Core.Models
{
    public abstract class DocumentContentProperties : ModelBase, IDocumentContentProperties
    {
        public int? FontSize { get; set; }

        [StringLength(100)]
        public string? Font { get; set; }

        /// <summary>
        /// RGB 0-255 e.g. 0,176,240
        /// </summary>
        [StringLength(11)]
        public string? Colour { get; set; }

        [StringLength(1)]
        public string? SubstituteStart { get; set; }

        [StringLength(1)]
        public string? SubstituteEnd { get; set; }
    }
}
