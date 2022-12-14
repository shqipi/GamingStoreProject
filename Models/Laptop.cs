using GamingStoreProject.Data.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GamingStoreProject.Models
{
    public class Laptop:IEntityBase
    {
        public int Id { get; set; }
        [Required]
        public string Image { get; set; }
        [Required, MinLength(20, ErrorMessage = "Minimum length is 20")]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        public bool Warranty { get; set; }
        [Required, Range(-100, 0, ErrorMessage ="Set range from -1 to -100, 0 if no discount")]
        public int Discount { get; set; }
    }
}
