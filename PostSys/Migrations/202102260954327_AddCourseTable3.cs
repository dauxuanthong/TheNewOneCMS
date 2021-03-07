namespace PostSys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourseTable3 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Courses", name: "Student_Id", newName: "StudentId");
            RenameIndex(table: "dbo.Courses", name: "IX_Student_Id", newName: "IX_StudentId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Courses", name: "IX_StudentId", newName: "IX_Student_Id");
            RenameColumn(table: "dbo.Courses", name: "StudentId", newName: "Student_Id");
        }
    }
}
