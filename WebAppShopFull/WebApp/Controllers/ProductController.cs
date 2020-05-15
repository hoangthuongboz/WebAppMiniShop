using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProductController : BaseController
    {
        public ProductController(IDbConnection connection) : base(connection) { }
        public IActionResult Index()
        {
            List<Product> list = app.Product.GetProducts();
            return View(list);
        }
        public IActionResult Create()
        {
            return View(app.Category.GetCategories());
        }
        [HttpPost]
        public IActionResult Create(Product obj, IFormFile f)
        {
            //them anh vao obj
            if (f != null)
            {
                obj.ImageUrl = Upload(f);
            }
            app.Product.Add(obj);
            return RedirectToAction("Index");
        }
        static string Upload(IFormFile f)
        {
            string fileName = Guid.NewGuid() + f.FileName.Replace(" ", string.Empty);
            string path = Directory.GetCurrentDirectory() + "/wwwroot/upload/" + fileName;
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                f.CopyTo(stream);
            }
            return fileName;
        }
        public IActionResult Edit(int id)
        {
            Product obj = app.Product.GetProductById(id);
            List<Category> list = app.Category.GetCategories();
            ViewBag.categories = list;//dung Viewbag de load ds Name category de select
            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(Product obj, IFormFile f)
        {
            if (f != null)
            {
                obj.ImageUrl = Upload(f);
            }
            app.Product.Edit(obj);
            return RedirectToAction("Index");
        }
        //delete
        [HttpPost]
        public IActionResult DeleteAll(int[] ids)
        {
            app.Product.Delete(ids);
            return RedirectToAction("Index");
        }
    }
}