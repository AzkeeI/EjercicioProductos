using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EjercicioProductos.Models;
using EjercicioProductos.Entities;

namespace EjercicioProductos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Producto()
        {
            List<ProductoModel> list = _context.Products.Select(x => new ProductoModel
            {
                Id = x.Id,
                ProductName = x.ProductName,
                Description = x.Description,
                Price = x.Price,
                BrandName = x.BrandName,
                Amount = x.Amount,
            }).ToList();

            return View(list);
        }

        public IActionResult ProductAdd()
        {
            return View();
        }


        [HttpPost]
        public IActionResult ProductAdd(ProductoModel model)
        {
            // //PARA INSERTAR DATOS EN LA BD
            // Producto producto = new Producto();
            // producto.Id = Guid.NewGuid();
            // producto.ProductName = "Gansito";
            // producto.Description = "Gansito de 100g";
            // producto.Price = 22.99;
            // producto.BrandName = "Marinela";
            // producto.Amount = 10;

            // // Añadir el producto a la base de datos
            // this._context.Products.Add(producto);
            // this._context.SaveChanges();

            // // Obtener todos los productos de la base de datos
            // var productos = this._context.Products.ToList();

            // // Pasar la lista de productos a la vista
            // return View(productos);


            if (ModelState.IsValid)
            {
                Producto producto = new Producto
                {
                    Id = Guid.NewGuid(),
                    ProductName = model.ProductName,
                    Description = model.Description,
                    Price = model.Price,
                    BrandName = model.BrandName,
                    Amount = model.Amount,
                };

                _context.Products.Add(producto);
                _context.SaveChanges();

                return RedirectToAction("Producto");
            }
            return View(model);

        }

        [HttpGet]
        public IActionResult ProductForDelete(Guid id)
        {
            Producto? pro = this._context.Products.FirstOrDefault(p => p.Id == id);
            if (pro == null)
            {
                return RedirectToAction("Producto");
            }

            var prro = new ProductoModel
            {
                Id = pro.Id,
                ProductName = pro.ProductName,
                Description = pro.Description,
                Price = pro.Price,
                BrandName = pro.BrandName,
                Amount = pro.Amount,
            };

            return View(prro);
        }
        [HttpPost]
        public IActionResult ProductDelete(Guid id)
        {
            var pro = _context.Products.FirstOrDefault(p => p.Id == id);
            if (pro != null)
            {
                _context.Products.Remove(pro);
                _context.SaveChanges();
            }

            return RedirectToAction("Producto");


        }
        public IActionResult ProductEdit(Guid id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return RedirectToAction("Producto");
            }

            var model = new ProductoModel
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                BrandName = product.BrandName,
                Amount = product.Amount,
            };

            return View(model);

        }

        [HttpPost]
        public IActionResult ProductEdit(ProductoModel prro2)
        {
            if (ModelState.IsValid)
            {
                var pro2 = _context.Products.FirstOrDefault(p => p.Id == prro2.Id);
                if (pro2 != null)
                {
                    pro2.ProductName = prro2.ProductName;
                    pro2.Description = prro2.Description;
                    pro2.Price = prro2.Price;
                    pro2.BrandName = prro2.BrandName;
                    pro2.Amount = prro2.Amount;

                    _context.SaveChanges();
                    return RedirectToAction("Producto");
                }
            }

            return View(prro2);

        }

    }

}
