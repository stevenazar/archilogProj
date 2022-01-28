using System;
using System.Collections.Generic;
using System.Text;

namespace ArchiLibrary.Models
{
    public abstract class BaseModel 
    {
        public int ID { get; set; }
        public bool Active { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
