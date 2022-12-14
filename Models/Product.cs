using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GamingStoreProject.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Image { get; set; }
        [Required, MinLength(20, ErrorMessage = "Minimum length is 20")]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
