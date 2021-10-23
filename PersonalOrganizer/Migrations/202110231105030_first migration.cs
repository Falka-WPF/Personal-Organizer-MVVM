namespace PersonalOrganizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MyTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        About = c.String(),
                        CategoryId = c.Int(nullable: false),
                        PriorityId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Priorities", t => t.PriorityId, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.PriorityId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.Priorities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MyTasks", "StatusId", "dbo.Status");
            DropForeignKey("dbo.MyTasks", "PriorityId", "dbo.Priorities");
            DropForeignKey("dbo.MyTasks", "CategoryId", "dbo.Categories");
            DropIndex("dbo.MyTasks", new[] { "StatusId" });
            DropIndex("dbo.MyTasks", new[] { "PriorityId" });
            DropIndex("dbo.MyTasks", new[] { "CategoryId" });
            DropTable("dbo.Status");
            DropTable("dbo.Priorities");
            DropTable("dbo.MyTasks");
            DropTable("dbo.Categories");
        }
    }
}
