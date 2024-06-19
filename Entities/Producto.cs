using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EjercicioProductos.Entities
{
    public class Producto
    {
        public Producto()
        {
        }
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string BrandName { get; set; }
        public int Amount { get; set; }
    }
}