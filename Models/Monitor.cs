using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace GamingStoreProject.Models
{
    public class Monitor
    {
        public int Id { get; set; }
        public string Image { get; set; }
        [Required, MinLength(20, ErrorMessage = "Minimum length is 20")]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        public bool Warranty { get; set; }
        [Required, Range(-100, 0, ErrorMessage = "Set range from -1 to -100, 0 if no discount")]
        public int Discount { get; set; }
    }
}
