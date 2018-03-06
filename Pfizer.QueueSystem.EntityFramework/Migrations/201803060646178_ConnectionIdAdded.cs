namespace Pfizer.QueueSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConnectionIdAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QueueHistory", "ConnectionId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.QueueHistory", "ConnectionId");
        }
    }
}
