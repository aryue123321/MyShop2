using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUi.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRespository context;
        ProductCategoryRespository categories;
        public ProductManagerController()
        {
            context = new ProductRespository();
            categories = new ProductCategoryRespository();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();

            viewModel.Product = new Product();
            viewModel.Categories = categories.Collection();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);
            context.Insert(product);
            context.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
                return HttpNotFound();

            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = product;
            viewModel.Categories = categories.Collection();
            return View(viewModel);

        }

        [HttpPost]
        public ActionResult Edit(Product product, string Id)
        {
            Product productToEdit = context.Find(Id);
            if (product == null)
                return HttpNotFound();
            if (!ModelState.IsValid)
                return View(product);
            productToEdit.Category = product.Category;
            productToEdit.Description = product.Description;
            productToEdit.Image = product.Image;
            productToEdit.Name = product.Name;
            productToEdit.Price = product.Price;

            context.Commit();

            return RedirectToAction("Index");
        }
        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
                return HttpNotFound();
            return View(productToDelete);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
                return HttpNotFound();
            context.Delete(Id);
            context.Commit();
            return RedirectToAction("Index");
        }
    }
}