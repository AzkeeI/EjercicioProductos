using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EjercicioProductos.Models;
using EjercicioProductos.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EjercicioProductos.Controllers;

public class CategoryController : Controller
{
    private readonly ILogger<CategoryController> _logger;
    private readonly ApplicationDbContext _context; 

    public CategoryController(ILogger<CategoryController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult CategoryList()
    {
        List<CategoryModel> list = _context.Categories.Select(x => new CategoryModel // Corregir a CategoryModel
        {
            Id = x.Id,
            Name = x.Name,
            Descripcion = x.Descripcion,

        }).ToList();

        return View(list); // Devolver la vista con la lista de productos
    }
    public IActionResult CategoryAdd()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CategoryAdd(CategoryModel model)
    {
        if (ModelState.IsValid)
        {
            Category category = new Category
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Descripcion = model.Descripcion,
            };

            _context.Categories.Add(category); // Usa el DbSet de Categories
            _context.SaveChanges();

            return RedirectToAction("CategoryList");
        }

        return View(model);

    }


    [HttpGet]

    public IActionResult CategoryForDelete(Guid id)
    {
        // Buscar la categoría por su Id
        Category? category = this._context.Categories.FirstOrDefault(c => c.Id == id);

        if (category == null)
        {
            // Si no se encuentra la categoría, redirigir a una acción o vista adecuada
            return RedirectToAction("CategoryList"); // Asumiendo que tienes una acción CategoryList para listar categorías
        }

        // Mapear la categoría encontrada a un modelo de vista CategoryModel
        var categoryModel = new CategoryModel
        {
            Id = category.Id,
            Name = category.Name,
            Descripcion = category.Descripcion
            // Añadir aquí otras propiedades si es necesario
        };

        // Devolver la vista con el modelo de la categoría
        return View(categoryModel);
    }


    [HttpPost]

    public IActionResult CategoryDelete(Guid id)
    {
        // Buscar la categoría por su Id
        var category = _context.Categories.FirstOrDefault(c => c.Id == id);

        if (category != null)
        {
            // Eliminar la categoría del contexto y guardar los cambios
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }

        // Redirigir a la acción que lista las categorías (assumiendo que tienes una acción CategoryList)
        return RedirectToAction("CategoryList");
    }

    public IActionResult CategoryEdit(Guid id)
    {
        var category = _context.Categories.FirstOrDefault(c => c.Id == id);

        if (category == null)
        {
            // Si no se encuentra la categoría, redirigir a la acción "Categoria"
            return RedirectToAction("CategoryList");
        }

        // Crear el modelo para mostrar la categoría en la vista
        var model = new CategoryModel
        {
            Id = category.Id,
            Name = category.Name,
            Descripcion = category.Descripcion,
            // Aquí adaptarías según los atributos de tu modelo CategoryModel
        };

        // Devolver la vista con el modelo de la categoría encontrada
        return View(model);
    }

    [HttpPost]

    public IActionResult CategoryEdit(CategoryModel model2)
    {
        if (ModelState.IsValid)
        {
            var proo = _context.Categories.FirstOrDefault(c => c.Id == model2.Id);
            if (proo != null)
            {
                proo.Name = model2.Name;
                proo.Descripcion = model2.Descripcion;

                _context.SaveChanges();
                return RedirectToAction("CategoryList");
            }
        }

        return View(model2);
    }


}