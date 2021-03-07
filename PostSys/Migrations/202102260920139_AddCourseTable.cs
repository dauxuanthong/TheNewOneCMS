namespace PostSys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourseTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ClassId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Student_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classes", t => t.ClassId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Student_Id)
                .Index(t => t.ClassId)
                .Index(t => t.Student_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "Student_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Courses", "ClassId", "dbo.Classes");
            DropIndex("dbo.Courses", new[] { "Student_Id" });
            DropIndex("dbo.Courses", new[] { "ClassId" });
            DropTable("dbo.Courses");
        }
    }
}
