using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public byte[] Photo { get; set; }
        public float Price { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}