namespace ClientesWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agregadoGustos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Gustos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.GustoClientes",
                c => new
                    {
                        Gusto_ID = c.Int(nullable: false),
                        Cliente_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Gusto_ID, t.Cliente_ID })
                .ForeignKey("dbo.Gustos", t => t.Gusto_ID, cascadeDelete: true)
                .ForeignKey("dbo.Clientes", t => t.Cliente_ID, cascadeDelete: true)
                .Index(t => t.Gusto_ID)
                .Index(t => t.Cliente_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GustoClientes", "Cliente_ID", "dbo.Clientes");
            DropForeignKey("dbo.GustoClientes", "Gusto_ID", "dbo.Gustos");
            DropIndex("dbo.GustoClientes", new[] { "Cliente_ID" });
            DropIndex("dbo.GustoClientes", new[] { "Gusto_ID" });
            DropTable("dbo.GustoClientes");
            DropTable("dbo.Gustos");
        }
    }
}
