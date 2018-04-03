namespace Pfizer.QueueSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JumpQueueUserAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JumpQueueUser",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserEID = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.JumpQueueUser");
        }
    }
}
