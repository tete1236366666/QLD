namespace QLD.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initdb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SINHVIEN",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HOTEN = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SINHVIEN");
        }
    }
}
