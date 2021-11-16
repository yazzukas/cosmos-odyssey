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
    public class Leg
    {
        [JsonPropertyName("Id")]
        [Key]
        [Required]
        public string LegId { get; set; }
        [Required]
        public RouteInfo RouteInfo { get; set; }
        [Required]
        public virtual ICollection<Provider> Providers { get; set; }
    }
}
