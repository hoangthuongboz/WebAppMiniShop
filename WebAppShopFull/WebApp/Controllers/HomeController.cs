using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Filters;

namespace WebApp.Controllers
{
    [ServiceFilter(typeof( MenuFilter))]
    public class HomeController : BaseController
    {
        public IActionResult Search(string q,int id = 1)
        {
            int total;
            List<Product> list = app.Product.Search(q, out total, id);
            ViewBag.total = total;
            return View(list);
        }
        public IActionResult Show()
        {
            //ViewBag.categories = new SelectList(app.Category.GetCategories(), "Id", "Name");
            return View(app.Category.GetCategoriesAndProducts());
        }
        public IActionResult Index(int id = 1)
        {
            int total;
            List<Product> list = app.Product.GetProductsPagination(out total, id, 9);
            //tinh so trang
            ViewBag.total = total;
            ViewBag.page = (total - 1) / 9 + 1;
            return View(list);
        }
        public IActionResult Browse(int id)
        {
            return View(app.Category.GetCategoryDetail(id));
        }
        public IActionResult Detail(int id)
        {
            return View(app.Product.GetProductsDetail(id));
        }
        public IActionResult SearchAdvance(int? cid, int? price, string name)
        {
            int total = 0;
            List<Product> list = app.Product.SearchAdvance(cid, price, name);
            foreach (Product item in list)
            {
                total += 1;
            }
            ViewBag.total = total;
            return View(list);
        }
    }
}