using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EjercicioProductos.Entities;
using Microsoft.EntityFrameworkCore;

namespace EjercicioProductos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Producto> Products { get; set; }

         public DbSet<Category> Categories {get; set; }
    }
}