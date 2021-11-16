using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CosmosOdyssey.Shared
{
    public class Reservation
    {

        public Reservation(string from, string to, decimal price, double travelTime, string travelCompanyName, string travelPriceId)
        {
            From = from;
            To = to;
            Price = price;
            TravelTime = travelTime;
            TravelCompanyName = travelCompanyName;
            TravelPriceId = travelPriceId;
        }

        [Key]
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string FirstName { get; set; }
        [Required, MaxLength(20)]
        public string LastName { get; set; }
        [Required]
        public DateTime CreatedTime { get; set; }
        [Required]
        public string From { get; set; }
        [Required]
        public string To { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public double TravelTime { get; set; }
        [Required]
        public string TravelCompanyName { get; set; }
        [Required]
        public string TravelPriceId { get; set; }
    }
}
