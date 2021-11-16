using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace CosmosOdyssey.Shared
{
    public class Company
    {
        [Key]
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
