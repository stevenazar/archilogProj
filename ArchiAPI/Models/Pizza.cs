using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ArchiLibrary.Models;

namespace ArchiAPI.Models
{
    public class Pizza : BaseModel
    {
        //public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Topping { get; set; }
    }
}
