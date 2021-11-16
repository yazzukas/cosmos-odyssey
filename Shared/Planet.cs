using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CosmosOdyssey.Shared
{
    public class Planet
    {
        [JsonPropertyName("Id")]
        [Key]
        [Required]
        public string PlanetId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
