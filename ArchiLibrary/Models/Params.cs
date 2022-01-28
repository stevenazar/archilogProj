using System;
using System.Collections.Generic;
using System.Text;

namespace ArchiLibrary.Models
{
    public class Params
    {
        public string Asc { get; set; }
        public string Desc { get; set; }
        // pagination 
        public string Range { get; set; }
        public bool HasOrder()
        {
            return !string.IsNullOrWhiteSpace(Asc) || !string.IsNullOrWhiteSpace(Desc);
        }
        public bool HasRange()
        {
            return !string.IsNullOrWhiteSpace(Range);
        }
        public bool HasAscOrder()
        {
            return !string.IsNullOrWhiteSpace(Asc);
        }
        public bool HasDescOrder()
        {
            return !string.IsNullOrWhiteSpace(Desc);
        }

    }
}

