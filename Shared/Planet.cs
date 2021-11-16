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
        public string PlanetId { get; set; }
        public string Name { get; set; }
    }
}
