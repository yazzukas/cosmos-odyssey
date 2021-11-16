using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CosmosOdyssey.Shared
{
    public class Provider
    {

        
        [JsonPropertyName("Id")]
        [Key]
        [Required]
        public string ProviderId { get; set; }
        [Required]
        public Company Company { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public DateTime FlightStart { get; set; }
        [Required]
        public DateTime FlightEnd { get; set; }
        [Required]
        public double TravelTime => (FlightEnd - FlightStart).TotalHours / 24;
    }
}
