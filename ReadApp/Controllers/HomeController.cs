using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReadApp.Models;

namespace ReadApp.Controllers;

public class HomeController : Controller
{
    public HomeController()
    {

    }

    public IActionResult Index(string searchString, string category)
    {
        var products = Repository.Products;

        if (!String.IsNullOrEmpty(searchString))
        {
            ViewBag.searchString = searchString;
            products = products.Where(p => p.Name.ToLower().Contains(searchString)).ToList();
        }

        if (!String.IsNullOrEmpty(category) && category != "0")
        {
            products = products.Where(p => p.CategoryID == int.Parse(category)).ToList();
        }

        var model = new ProductViewModel
        {
            Products = products,
            Categories = Repository.Categories,
            SelectedCategory = category
        };
        return View(model);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryID", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product model, IFormFile imageFile)
    {

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

        if (imageFile != null)
        {

            var extensions = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extensions))
            {
                ModelState.AddModelError("", "Lütfen sadece jpg, jpeg ve png formatında dosya yükleyiniz.");
            }
            else
            {
                var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extensions}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);

                try
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    model.Image = randomFileName;
                }
                catch
                {
                    ModelState.AddModelError("", "Dosya yüklenirken bir hata oluştu.");
                }
            }
        }
        else
        {
            ModelState.AddModelError("", "Lütfen bir dosya seçiniz.");
        }

        if (ModelState.IsValid)
        {
            model.ProductID = Repository.Products.Count + 1;
            Repository.CreateProduct(model);
            return RedirectToAction("Index");
        }
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryID", "Name");
        return View(model);
    }
    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = Repository.Products.FirstOrDefault(p => p.ProductID == id);
        if (entity == null)
        {
            return NotFound();
        }
        else
        {
            ViewBag.Categories = new SelectList(Repository.Categories, "CategoryID", "Name");
            return View(entity);
        }
    }
    [HttpPost]
    public async Task<IActionResult> Edit(int id, Product model, IFormFile? imageFile)
    {
        if (id != model.ProductID)
        {
            return NotFound();
        }
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

        if (imageFile != null)
        {

            var extensions = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extensions))
            {
                ModelState.AddModelError("", "Lütfen sadece jpg, jpeg ve png formatında dosya yükleyiniz.");
            }
            else
            {
                var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extensions}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);

                try
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    model.Image = randomFileName;
                }
                catch
                {
                    ModelState.AddModelError("", "Dosya yüklenirken bir hata oluştu.");
                }
            }
        }

        if (ModelState.IsValid)
        {
            Repository.EditProduct(model);
            return RedirectToAction("Index");
        }
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryID", "Name");
        return View(model);
    }

    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = Repository.Products.FirstOrDefault(p => p.ProductID == id);
        if (entity == null)
        {
            return NotFound();
        }
        else
        {
            Repository.DeleteProduct(entity);
            return RedirectToAction("Index");
        }
    }
}