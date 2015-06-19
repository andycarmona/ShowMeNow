namespace AngularJSAuthentication.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateItineraryTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Itineraries",
                c => new
                    {
                        ItineraryId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.ItineraryId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Itineraries");
        }
    }
}
