using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EjercicioProductos.Entities
{
    public class Category
    {
        public Category()
        {
        }
        public Guid Id { get; set; }

        public string Name { get; set; } 

        public string Descripcion { get; set; }


    }
}