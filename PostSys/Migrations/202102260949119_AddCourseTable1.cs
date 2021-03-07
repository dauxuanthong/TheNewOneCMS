namespace PostSys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourseTable1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "Student_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Courses", new[] { "Student_Id" });
            AddColumn("dbo.Courses", "Student_Id1", c => c.String(maxLength: 128));
            AlterColumn("dbo.Courses", "Student_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Courses", "Student_Id1");
            AddForeignKey("dbo.Courses", "Student_Id1", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Courses", "StudentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "StudentId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Courses", "Student_Id1", "dbo.AspNetUsers");
            DropIndex("dbo.Courses", new[] { "Student_Id1" });
            AlterColumn("dbo.Courses", "Student_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Courses", "Student_Id1");
            CreateIndex("dbo.Courses", "Student_Id");
            AddForeignKey("dbo.Courses", "Student_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
