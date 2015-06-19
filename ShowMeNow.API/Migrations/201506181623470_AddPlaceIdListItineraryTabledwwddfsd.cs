namespace AngularJSAuthentication.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlaceIdListItineraryTabledwwddfsd : DbMigration
    {
        public override void Up()
        {
    
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Places", "Address", c => c.String());
            AlterColumn("dbo.Places", "Name", c => c.String());
        }
    }
}
