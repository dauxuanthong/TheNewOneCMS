namespace PostSys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostTable4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Ischecked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Ischecked");
        }
    }
}
