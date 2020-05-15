using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected AppRepository app;
        public BaseController(IDbConnection connection)
        {
            connection.Open();
            app = new AppRepository(connection);
        }
        public BaseController()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            IDbConnection connection = new SqlConnection(configuration.GetConnectionString("shop"));
            connection.Open();
            app = new AppRepository(connection);
        }
        protected long CardId
        {
            get
            {
                long cartId;
                string obj = Request.Cookies["cart"];
                if (string.IsNullOrEmpty(obj))
                {
                    cartId = Helper.RandomLong();
                    CookieOptions options = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(30),
                        Path = "/"
                    };
                    Response.Cookies.Append("cart", cartId.ToString(), options);
                }
                else
                {
                    cartId = long.Parse(obj);
                }
                return cartId;
            }
        }
    }
}
