namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtablelogo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logoes",
                c => new
                    {
                        LogoID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        ImageName = c.String(),
                    })
                .PrimaryKey(t => t.LogoID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Logoes");
        }
    }
}
