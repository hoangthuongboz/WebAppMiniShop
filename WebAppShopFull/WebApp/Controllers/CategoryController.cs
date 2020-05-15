using System.Collections.Generic;
using System.Data;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CategoryController : BaseController
    {
        public CategoryController(IDbConnection connection) : base(connection)
        {
        }

        public IActionResult Index()
        {
            return View(app.Category.GetCategories());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            app.Category.Add(obj);
            return RedirectToAction("Index");
        }
        //Delete
        public IActionResult Delete(int id)
        {
            app.Category.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DeleteAll(int[] ids)
        {
            app.Category.Delete(ids);
            return RedirectToAction("Index");
        }
        //Edit
        public IActionResult Edit(int id)
        {
            return View(app.Category.GetCategoryById(id));
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            app.Category.Edit(obj);
            return RedirectToAction("Index");
        }
    }
}