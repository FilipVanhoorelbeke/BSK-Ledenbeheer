namespace BSKLedenManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class extensionmembers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LogTime = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                        Message = c.String(),
                        User = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Members", "Verzekering", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "Verzekering");
            DropTable("dbo.Logs");
        }
    }
}
