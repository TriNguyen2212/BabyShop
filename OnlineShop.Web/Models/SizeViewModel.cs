using System.ComponentModel.DataAnnotations;

namespace BabyShop.Web.Models
{
    public class SizeViewModel
    {
        public int ID { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public bool Status { get; set; }
    }
}