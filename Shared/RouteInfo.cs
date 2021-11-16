using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CosmosOdyssey.Shared
{
    public class RouteInfo
    {
        [JsonPropertyName("Id")]
        [Key]
        [Required]
        public string RouteId { get; set; }
        [Required]
        public Planet From { get; set; }
        [Required]
        public Planet To { get; set; }
        [Required]
        public double Distance { get; set; }
    }
}
