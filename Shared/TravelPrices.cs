using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CosmosOdyssey.Shared
{
    public class TravelPrices
    {
        [JsonPropertyName("Id")]
        [Key]
        public string TravelPricesId { get; set; }

        public DateTime ValidUntil { get; set; }
        
        public virtual ICollection<Leg> Legs { get; set; }
    }
}
