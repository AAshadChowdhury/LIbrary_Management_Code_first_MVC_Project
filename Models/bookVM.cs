using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CF__MVC_Project.Models
{
    public class bookVM
    {
        [Key]
        [Required(ErrorMessage = "Please enter id")]
        [Display(Name = "Identity")]
        public int id { get; set; }
        [Required(ErrorMessage = "Please enter Name")]
        [MaxLength(50)]
        [CustomNameValidator]
        public string name { get; set; }
        [Required(ErrorMessage = "Please enter author Name")]
        public string author { get; set; }
        [Required(ErrorMessage = "Please enter Publisher publisher Name")]
        [StringLength(40)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
        ErrorMessage = "Number are not allowed.")]
        public string publisher { get; set; }
        [Required(ErrorMessage = "Please Select Suitable Category Name")]
        public string category { get; set; }
        [Required(ErrorMessage = "Please enter QUantity")]

        public int? stockQuantity { get; set; }
        public decimal? price { get; set; }
        public string cover { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
    }

}