using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class MemberController : BaseController
    {
        public MemberController(IDbConnection connection) : base(connection)
        {
        }
        //load list member
        public IActionResult Index()
        {
            return View(app.Member.GetMembers());
        }
        //Get YourProfile and list role
        public IActionResult Role(long id)
        {
            return View(app.Member.GetMemberAndRoles(id));
        }
        public IActionResult Edit(MemberInRole obj)
        {
            return Json(app.MemberInRole.Save(obj));
        }
    }
}