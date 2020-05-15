using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RoleController : BaseController
    {
        public RoleController(IDbConnection connection) : base(connection)
        {
        }
        
        public IActionResult Index()
        {
            return View(app.Role.GetRoles());
        }
        //Add obj
        public IActionResult Create(Role obj)
        {
            app.Role.Add(obj);
            return RedirectToAction("Index");
        }
        //Edit obj
        public IActionResult Edit(int id)
        {
            return Json(app.Role.GetRoleById(id));
        }
        [HttpPost]
        public IActionResult Edit(Role obj)
        {
            app.Role.Edit(obj);
            return RedirectToAction("Index");
        }
        //Delete obj
        public IActionResult Delete(int id)
        {
            app.Role.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
