namespace ClientesWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rutAgregado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clientes", "Rut", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clientes", "Rut");
        }
    }
}
