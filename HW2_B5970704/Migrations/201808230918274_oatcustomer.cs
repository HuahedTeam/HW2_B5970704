namespace HW2_B5970704.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class oatcustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "name", c => c.String());
            AddColumn("dbo.AspNetUsers", "address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "address");
            DropColumn("dbo.AspNetUsers", "name");
        }
    }
}
