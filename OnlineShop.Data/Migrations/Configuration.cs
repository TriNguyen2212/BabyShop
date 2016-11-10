namespace BabyShop.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BabyShop.Data.BabyShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BabyShop.Data.BabyShopDbContext context)
        {
            CreateProductCategorySample(context);
            CreateSlides(context);
            CreateContachdetail(context);
        }

        private void CreateProductCategorySample(BabyShop.Data.BabyShopDbContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listProductCategory = new List<ProductCategory>()
            {
                new ProductCategory() {Name="Điện lạnh",Alias="dien-lanh" },
                new ProductCategory() {Name="Viễn thông",Alias="vien-thong" },
                new ProductCategory() {Name="Mỹ phẩm",Alias="my-pham" }
            };
                context.ProductCategories.AddRange(listProductCategory);
                context.SaveChanges();
            }
        }

        private void CreateUserManager()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new BabyShopDbContext()));
            var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(new BabyShopDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "tri",
                Email = "nguyenvantri.cntt@gmail.com",
                EmailConfirmed = true,
                BirthDay = DateTime.Now,
                FullName = "nguyễn văn trí"
            };

            manager.Create(user, "123456");

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new ApplicationRole { Name = "Admin", Description = "Admin" });
                roleManager.Create(new ApplicationRole { Name = "User", Description = "User" });
            }
            var adminUser = manager.FindByEmail("nguyenvantri.cntt@gmail.com");
            manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
        }

        private void CreateSlides(BabyShop.Data.BabyShopDbContext context)
        {
            if (context.Slides.Count() == 0)
            {
                List<Slide> listSlide = new List<Slide>()
                {
                    new Slide() {
                        Name ="Slide 1",
                        DisplayOrder =1,
                        Status =true,
                        URL ="#",
                        Image ="/Assets/client/images/bag.jpg",
                        Content =@"	<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                                <p>Lorem ipsum dolor sit amet, consectetur
                            adipisicing elit, sed do eiusmod tempor incididunt ut labore et </ p >
                        <span class=""on-get"">GET NOW</span>" },
                    new Slide() {
                        Name ="Slide 2",
                        DisplayOrder =2,
                        Status =true,
                        URL ="#",
                        Image ="/Assets/client/images/bag1.jpg",
                    Content=@"<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </ p >
                                <span class=""on-get"">GET NOW</span>"},
                };
                context.Slides.AddRange(listSlide);
                context.SaveChanges();
            }
        }

        private void CreateContachdetail(BabyShop.Data.BabyShopDbContext context)
        {
            if (context.ContactDetails.Count() == 0)
            {
                var ContactDetail = new BabyShop.Model.Models.ContactDetail()
                {
                    Name = "Online Shop",
                    Address = "Hoài thanh",
                    Email = "nguyenvantri.cntt@gmail.com",
                    Lat = 10.7385484,
                    Lng = 106.6480975,
                    Other = "",
                    Phone = "01678921425",
                    Website = "OnlineShop.vn"
                };
                context.ContactDetails.Add(ContactDetail);
                context.SaveChanges();
            }
        }
    }
}