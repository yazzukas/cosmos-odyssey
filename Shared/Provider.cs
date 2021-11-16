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
        public string ProviderId { get; set; }

        public Company Company { get; set; }

        public decimal Price { get; set; }

        public DateTime FlightStart { get; set; }

        public DateTime FlightEnd { get; set; }

        public double TravelTime => (FlightEnd - FlightStart).TotalHours / 24;
    }
}
