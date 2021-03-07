namespace PostSys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFacultyTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Faculties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Faculties");
        }
    }
}
