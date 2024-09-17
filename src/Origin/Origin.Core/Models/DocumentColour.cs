using Atlas.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Text.Json.Serialization;

namespace Origin.Core.Models
{
    public class DocumentColour : ModelBase
    {
        public int DocumentColourId { get; set; }

        [StringLength(100)]
        public string? Name { get; set; }

        /// <summary>
        /// RGB 0-255 e.g. 0,176,240
        /// </summary>
        [StringLength(11)]
        public string? Rgb { get; set; }
    }
}
