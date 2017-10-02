using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPNETIdentityWithOnion.Web.HtmlHelpers
{
    public static class MyHelpers
    {
        public static MvcHtmlString BoleanHelper(this HtmlHelper html, bool check)
        {
            TagBuilder i = new TagBuilder("i");
            if(check == true)
            {
                i.AddCssClass("fa fa-toggle-on");

            }
            else
            {
                i.AddCssClass("fa fa-toggle-off");
                i.MergeAttribute("style", "color:rgb(246, 190, 21); font-size:18px;");
           
            }
            return MvcHtmlString.Create(i.ToString());
        }

    }
}