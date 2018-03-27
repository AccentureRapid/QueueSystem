namespace Pfizer.QueueSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateAndTimeSpanIdadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FastToken", "Date", c => c.String());
            AddColumn("dbo.FastToken", "TimeSpanId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FastToken", "TimeSpanId");
            DropColumn("dbo.FastToken", "Date");
        }
    }
}
