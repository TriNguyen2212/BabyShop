using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BabyShop.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Bạn cần nhập email")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}