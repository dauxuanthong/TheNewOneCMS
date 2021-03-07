namespace PostSys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClassTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CoordinatorId = c.String(maxLength: 128),
                        FacultyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CoordinatorId)
                .ForeignKey("dbo.Faculties", t => t.FacultyId, cascadeDelete: true)
                .Index(t => t.CoordinatorId)
                .Index(t => t.FacultyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Classes", "FacultyId", "dbo.Faculties");
            DropForeignKey("dbo.Classes", "CoordinatorId", "dbo.AspNetUsers");
            DropIndex("dbo.Classes", new[] { "FacultyId" });
            DropIndex("dbo.Classes", new[] { "CoordinatorId" });
            DropTable("dbo.Classes");
        }
    }
}
