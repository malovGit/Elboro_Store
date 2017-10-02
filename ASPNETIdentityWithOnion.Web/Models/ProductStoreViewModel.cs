using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ASPNETIdentityWithOnion.Web.Extensions;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPNETIdentityWithOnion.Web.Models
{
    public class ProductStoreViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductID { get; set; }

        
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public virtual decimal Price { get; set; }

        [Required(ErrorMessage = "Please enter a description")]
        [Display(Name = "Description")]
        [MaxLength(500, ErrorMessage = "Max length of details must be 500 letters")]
        public virtual string ShortDescription { get; set; }

        //[Display(Name = "Active")]
        //[DisplayFormat(HtmlEncode = true)]
        //public bool IsActive { get; set; }

        [Display(Name = "Details")]
        [DataType(DataType.MultilineText)]
        public virtual string LongDescription { get; set; }

        //[Required(ErrorMessage = "Please specify a category")]
        public int SubCategoryId { get; set; }

        //public SortCategories sortList { get; set; }
        //public List<SelectListItem> SortCategories { get; set; }

        //public List<SelectListItem> Categories { get; set; }

        //public int SelectedId { get; set; }

        //public int SubSelectedId { get; set; }

        public string Path { get; set; }


        public List<string> Images { get; set; }
    }
}