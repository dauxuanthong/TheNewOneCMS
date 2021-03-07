namespace PostSys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostTable1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Description", c => c.String());
            AddColumn("dbo.Posts", "File", c => c.Binary());
            AddColumn("dbo.Posts", "UrlFile", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "UrlFile");
            DropColumn("dbo.Posts", "File");
            DropColumn("dbo.Posts", "Description");
        }
    }
}
