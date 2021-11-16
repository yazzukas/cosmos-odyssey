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
        public string RouteId { get; set; }
        public Planet From { get; set; }

        public Planet To { get; set; }

        public double Distance { get; set; }
        /*
        [ForeignKey("LegId")]
        public virtual Leg Leg { get; set; }
        public string LegId { get; set; }*/
    }
}
