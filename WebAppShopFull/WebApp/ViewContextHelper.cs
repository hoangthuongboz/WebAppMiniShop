using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
{
    public static class ViewContextHelper
    {
        public static int GetId(this ViewContext context, string key)
        {
            object obj = context.RouteData.Values[key];
            if (obj != null)
            {
                return int.Parse(obj.ToString());
            }
            return 1;
        }
    }
}
