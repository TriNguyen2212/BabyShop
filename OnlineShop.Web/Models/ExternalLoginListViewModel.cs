using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BabyShop.Web.Models
{
    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}