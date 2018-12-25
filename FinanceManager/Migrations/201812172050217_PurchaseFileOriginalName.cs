namespace FinanceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PurchaseFileOriginalName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Purchases", "FileOriginalName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Purchases", "FileOriginalName");
        }
    }
}
