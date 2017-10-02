using ASPNETIdentityWithOnion.Core.DomainModels.StoreModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPNETIdentityWithOnion.Web.Areas.Admin.Models
{
    [Serializable]
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please enter a category name")]
        public string Name { get; set; }

        
        [Display(Name = "Active")]
        [DisplayFormat(HtmlEncode = true)]
        public bool? IsActive { get; set; }

        [Display(Name = "Picture")]
        public virtual string Path { get; set; }

        
        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}