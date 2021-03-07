namespace PostSys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourseTable2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Courses", new[] { "Student_Id1" });
            DropColumn("dbo.Courses", "Student_Id");
            RenameColumn(table: "dbo.Courses", name: "Student_Id1", newName: "Student_Id");
            AlterColumn("dbo.Courses", "Student_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Courses", "Student_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Courses", new[] { "Student_Id" });
            AlterColumn("dbo.Courses", "Student_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Courses", name: "Student_Id", newName: "Student_Id1");
            AddColumn("dbo.Courses", "Student_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Courses", "Student_Id1");
        }
    }
}
