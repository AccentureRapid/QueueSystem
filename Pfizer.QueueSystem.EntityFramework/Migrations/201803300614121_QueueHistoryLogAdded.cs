namespace Pfizer.QueueSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QueueHistoryLogAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QueueHistoryLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserEID = c.String(nullable: false, maxLength: 50),
                        UserName = c.String(maxLength: 50),
                        ConnectionId = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        ConnectedTime = c.DateTime(nullable: false),
                        DisconnectedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.QueueHistoryLog");
        }
    }
}
