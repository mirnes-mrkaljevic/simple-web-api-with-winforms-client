using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DBAccess
{
    public interface IProductRepository : IDisposable
    {
        DbSet<Product> Products { get; set; }
        int SaveChanges();
    }
}
