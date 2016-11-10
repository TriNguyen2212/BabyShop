namespace BabyShop.Web.Models
{
    public class MenuViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public int? DisplayOrder { get; set; }
        public int GroupID { set; get; }
        public virtual MenuGroupViewModel MenuGroup { get; set; }
        public string Target { get; set; }
        public bool Status { get; set; }
    }
}