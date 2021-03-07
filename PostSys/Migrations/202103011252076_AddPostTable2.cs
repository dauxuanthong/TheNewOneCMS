namespace PostSys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostTable2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "PostDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "PostDate");
        }
    }
}
