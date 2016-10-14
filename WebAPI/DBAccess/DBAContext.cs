using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebAPI.DBAccess
{
    public class DBAContext : DbContext , IProductRepository
    {
        private const string connectionStrig = @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;";
        
        private DBAContext(string connectionStrig) : base(connectionStrig){  }

        public static IProductRepository GetContext()
        {
            return new DBAContext(connectionStrig);
        }

        public DbSet<Product> Products { get; set; }
    }
}