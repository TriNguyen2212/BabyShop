namespace BabyShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumContentToSlide : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Slides", "Content", c => c.String());
            AlterColumn("dbo.Slides", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Slides", "Status", c => c.Boolean());
            DropColumn("dbo.Slides", "Content");
        }
    }
}
