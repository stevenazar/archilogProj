using System;
using ArchiLibrary.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace ArchiAPI.Models
{
    public class Customer : BaseModel
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public Guid ID { get; set; }

        [StringLength(30)]
        public string Lastname { get; set; }
        //[Column("Prenom")]
        public string Firstname { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
