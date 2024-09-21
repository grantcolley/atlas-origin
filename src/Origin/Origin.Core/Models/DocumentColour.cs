using Atlas.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Origin.Core.Models
{
    public class DocumentColour : ModelBase
    {
        public int DocumentColourId { get; set; }

        [Required]
        [StringLength(100)]
        public string? Colour { get; set; }

        /// <summary>
        /// RGB 0-255 e.g. 0,176,240
        /// </summary>
        [Required]
        [StringLength(11)]
        public string? Rgb { get; set; }
    }
}
