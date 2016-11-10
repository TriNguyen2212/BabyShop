namespace BabyShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class add_productSizeColor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Colors",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                    Status = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.ProductColorSizes",
                c => new
                {
                    ProductID = c.Int(nullable: false),
                    SizeID = c.Int(nullable: false),
                    ColorID = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.ProductID, t.SizeID, t.ColorID })
                .ForeignKey("dbo.Colors", t => t.ColorID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Sizes", t => t.SizeID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.SizeID)
                .Index(t => t.ColorID);

            CreateTable(
                "dbo.Sizes",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                    Status = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.ID);
        }

        public override void Down()
        {
            DropForeignKey("dbo.ProductColorSizes", "SizeID", "dbo.Sizes");
            DropForeignKey("dbo.ProductColorSizes", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductColorSizes", "ColorID", "dbo.Colors");
            DropIndex("dbo.ProductColorSizes", new[] { "ColorID" });
            DropIndex("dbo.ProductColorSizes", new[] { "SizeID" });
            DropIndex("dbo.ProductColorSizes", new[] { "ProductID" });
            DropTable("dbo.Sizes");
            DropTable("dbo.ProductColorSizes");
            DropTable("dbo.Colors");
        }
    }
}