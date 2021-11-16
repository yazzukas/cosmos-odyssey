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
        [Required]
        public string TravelPricesId { get; set; }
        [Required]
        public DateTime ValidUntil { get; set; }
        [Required]
        public virtual ICollection<Leg> Legs { get; set; }
    }
}
