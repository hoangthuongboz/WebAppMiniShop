using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class CartController : BaseController
    {
        public CartController(IDbConnection connection) : base(connection)
        {
        }

        public IActionResult Index()
        {
            return View(app.Cart.GetCarts(CardId));
        }

        [HttpPost]
        public IActionResult Create(Cart obj)
        {
            obj.Id = CardId;
            app.Cart.Add(obj);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            app.Cart.Delete(CardId, id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(Cart obj)
        {
            obj.Id = CardId;
            return Json(app.Cart.Edit(obj));
        }
    }
}