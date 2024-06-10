namespace GVD2247A5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedvideotypetoedpisode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Episodes", "VideoContentType", c => c.String());
            AddColumn("dbo.Episodes", "Video", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Episodes", "Video");
            DropColumn("dbo.Episodes", "VideoContentType");
        }
    }
}
