using DAL;
using DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class AuthController : BaseController
    {
        public IActionResult Denied()
        {
            return View();
        }
        [Authorize]
        public IActionResult Index()
        {
            long id = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Member member = app.Member.GetMemberById(id);
            return View(member);
        }
        [Authorize]
        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignIn(SignInModel obj)
        {
            if (ModelState.IsValid)
            {
                Member member = app.Member.SignIn(obj.Usr, obj.Pwd);
                if (member != null)
                {
                    ClaimsIdentity claims = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, member.Id.ToString()));
                    claims.AddClaim(new Claim(ClaimTypes.Name, member.Username));
                    claims.AddClaim(new Claim(ClaimTypes.Email, member.Email));
                    //load roles
                    if (member.Roles != null)
                    {
                        foreach (Role item in member.Roles)
                        {
                            claims.AddClaim(new Claim(ClaimTypes.Role, item.Name));
                        }
                    }
                    AuthenticationProperties properties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        IsPersistent = obj.Remember,
                    };
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claims), properties);
                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError("error", "Đăng nhập bị lỗi!");
                }
            }
            return View(obj);
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(Member obj)
        {
            if (ModelState.IsValid)
            {
                obj.Id = Helper.RandomLong();
                int ret = app.Member.Add(obj);
                string[] error =
                {
                    "Username Đã tồn tại!",
                    "Đăng kí bị lỗi"
                };
                if (ret >= 2)
                {
                    return RedirectToAction("Signin");
                }
                else
                {
                    ModelState.AddModelError("error", error[ret]);
                }
            }
            return View(obj);
        }
        public IActionResult Change()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Change(Member obj)
        {
            obj.Username = User.Identity.Name;
            if (obj.OldPassword.Equals(obj.Password))
            {
                ModelState.AddModelError("error", "Mật khẩu không thể trùng nhau!");
                return View(obj);
            }
            else
            {
                app.Member.Update(obj);
                return RedirectToAction("Index");
            }          
        }
    }
}
