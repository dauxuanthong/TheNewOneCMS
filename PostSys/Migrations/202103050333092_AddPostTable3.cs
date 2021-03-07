namespace PostSys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostTable3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "NameOfFile", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "NameOfFile");
        }
    }
}
