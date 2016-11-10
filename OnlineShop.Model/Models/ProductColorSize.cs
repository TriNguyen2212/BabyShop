using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyShop.Model.Models
{
    public class ProductColorSize
    {
        [Key]
        [Column(Order = 1)]
        public int ProductID { set; get; }

        [Key]
        [Column(Order = 2)]
        public int SizeID { set; get; }

        [Key]
        [Column(Order = 3)]
        public int ColorID { set; get; }

        [ForeignKey("ProductID")]
        public virtual Product Product { set; get; }

        [ForeignKey("SizeID")]
        public virtual Size Size { set; get; }

        [ForeignKey("ColorID")]
        public virtual Color Color { set; get; }
    }
}