using ASPNETIdentityWithOnion.Core.DomainModels;
using ASPNETIdentityWithOnion.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ASPNETIdentityWithOnion.Web.HtmlHelpers
{
    public static class PagingHelper
    {

        public static MvcHtmlString PageLinks(this HtmlHelper html,
                                                PaginatedList<ProductStoreViewModel> pagingInfo,
                                                Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();

            TagBuilder tagNav = new TagBuilder("nav");
            TagBuilder tagUl = new TagBuilder("ul");
            TagBuilder tagLiLeft = new TagBuilder("li");
            TagBuilder tagLiRight = new TagBuilder("li");
            TagBuilder tagSpanLeft = new TagBuilder("span");
            TagBuilder tagSpanRight = new TagBuilder("span");
            TagBuilder tagALeft = new TagBuilder("a");
            TagBuilder tagARight = new TagBuilder("a");
            

            tagNav.MergeAttribute("aria-label", "...");
            tagUl.AddCssClass("pager");
            tagLiLeft.AddCssClass("previous");
            tagLiRight.AddCssClass("next");
           // tagALeft.MergeAttribute("href", pageUrl(1));
            tagSpanLeft.MergeAttribute("aria-hidden", "true");
            tagSpanLeft.InnerHtml = "&larr;";
             
           // tagARight.MergeAttribute("href", pageUrl(2));
            tagSpanRight.MergeAttribute("aria-hidden", "true");
            tagSpanRight.InnerHtml = "&rarr;";
            
    
            if (pagingInfo.TotalPageCount <= 1)
            {
                tagLiLeft.AddCssClass("disabled");
                tagLiRight.AddCssClass("disabled");
                tagALeft.MergeAttribute("href", "#");
                tagARight.MergeAttribute("href", "#");
            }
            else if(pagingInfo.TotalPageCount == pagingInfo.PageIndex && pagingInfo.PageIndex > 1)
            {
                tagLiLeft.AddCssClass("enabled");
                tagALeft.MergeAttribute("href", pageUrl(pagingInfo.PageIndex - 1));
                tagLiRight.AddCssClass("disabled");
            }
            else if(pagingInfo.PageIndex == 1)
            {
                tagLiLeft.AddCssClass("disabled");
                tagALeft.MergeAttribute("href", pageUrl(pagingInfo.PageIndex - 1));
                tagLiRight.AddCssClass("enabled");
                tagARight.MergeAttribute("href", pageUrl(pagingInfo.PageIndex + 1));
            }
            else
            {
                tagLiLeft.AddCssClass("enabled");
                tagALeft.MergeAttribute("href", pageUrl(pagingInfo.PageIndex - 1));
                tagLiRight.AddCssClass("enabled");
                tagARight.MergeAttribute("href", pageUrl(pagingInfo.PageIndex + 1));
            }
            tagALeft.InnerHtml = tagSpanLeft.ToString() + "Older";
            tagLiLeft.InnerHtml = tagALeft.ToString();
            tagARight.InnerHtml = "Never" + tagSpanRight.ToString();
            tagLiRight.InnerHtml = tagARight.ToString();
            tagUl.InnerHtml = tagLiLeft.ToString() + tagLiRight.ToString();
            tagNav.InnerHtml = tagUl.ToString();
            result.Append(tagNav.ToString());

            //for (int i = 1; i <= pagingInfo.TotalPageCount; i++)
            //{
            //    TagBuilder tag = new TagBuilder("a");
            //    tag.MergeAttribute("href", pageUrl(i));
            //    tag.InnerHtml = i.ToString();
            //    if(i == pagingInfo.PageIndex)
            //    {
            //        tag.AddCssClass("selected");
            //    }
            //    result.Append(tag.ToString());
            //}
            return MvcHtmlString.Create(result.ToString());
        }
        
    }
}