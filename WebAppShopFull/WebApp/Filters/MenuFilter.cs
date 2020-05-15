using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;


namespace WebApp.Filters
{
    public class MenuFilter : ActionFilterAttribute
    {
        //cai nay sai o d
        //protected IDbConnection connection;  
        AppRepository app;
        public MenuFilter(IDbConnection connection)
        {
            connection.Open();
            app = new AppRepository(connection);
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            
            Controller controller = (Controller)context.Controller;
            
            controller.ViewBag.categories = app.Category.GetCategories();
            controller.ViewBag.selectcategories = new SelectList(app.Category.GetCategories(), "Id", "Name");
        }
    }
}
