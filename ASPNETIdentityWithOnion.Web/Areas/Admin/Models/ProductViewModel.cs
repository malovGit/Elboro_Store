using ASPNETIdentityWithOnion.Core.DomainModels.StoreModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPNETIdentityWithOnion.Web.Areas.Admin.Models
{
    public class ProductViewModel
    {

        //[HiddenInput(DisplayValue = false)]
        [Display(Name = "ID")]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Please enter a product name")]
        public virtual string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public virtual decimal Price { get; set; }

        [Required(ErrorMessage = "Please enter a description")]
        [Display(Name = "Description")]
        [MaxLength(500, ErrorMessage ="Max length of details must be 500 letters")]
        public virtual string ShortDescription { get; set; }

        [Display(Name = "Active")]
        [DisplayFormat(HtmlEncode = true)]
        public bool IsActive { get; set; }

        [Display(Name = "Details")]
        [DataType(DataType.MultilineText)]
        public virtual string LongDescription { get; set; }

        //[Required(ErrorMessage = "Please specify a category")]
        [Display(Name = "Subcategory")]
        public string SubCategoryName { get; set; }

        public List<SelectListItem> SubCategories { get; set; }

        public List<SelectListItem> Categories { get; set; }

        public int SelectedId { get; set; }

        public int SubSelectedId { get; set; }

        public string Path { get; set; }

        public List<string> Images { get; set; }
    }
}