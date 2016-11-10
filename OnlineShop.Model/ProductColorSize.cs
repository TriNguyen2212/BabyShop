using BabyShop.Model.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BabyShop.Model
{
    [Table("ProductColorSizes")]
    public class ProductColorSize
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { set; get; }

        [ForeignKey("ColorID")]
        public virtual Color Color { set; get; }

        [ForeignKey("SizeID")]
        public virtual Size Size { set; get; }
    }
}