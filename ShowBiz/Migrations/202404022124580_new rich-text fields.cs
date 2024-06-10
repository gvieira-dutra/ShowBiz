namespace GVD2247A5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newrichtextfields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Actors", "Biography", c => c.String());
            AddColumn("dbo.Shows", "Premise", c => c.String());
            AddColumn("dbo.Episodes", "Premise", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Episodes", "Premise");
            DropColumn("dbo.Shows", "Premise");
            DropColumn("dbo.Actors", "Biography");
        }
    }
}
