namespace CF__MVC_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbmigration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.bookPurchases",
                c => new
                    {
                        purchaseID = c.Int(nullable: false, identity: true),
                        bookid = c.Int(),
                        quantity = c.Int(),
                        purchaseDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.purchaseID)
                .ForeignKey("dbo.books", t => t.bookid)
                .Index(t => t.bookid);
            
            CreateTable(
                "dbo.books",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        author = c.String(),
                        publisher = c.String(),
                        categoriesTBcategory = c.String(maxLength: 128),
                        stockQuantity = c.Int(),
                        price = c.Decimal(precision: 18, scale: 2),
                        cover = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.categoriesTBs", t => t.categoriesTBcategory)
                .Index(t => t.categoriesTBcategory);
            
            CreateTable(
                "dbo.bookSales",
                c => new
                    {
                        saleID = c.Int(nullable: false, identity: true),
                        bookid = c.Int(),
                        quantity = c.Int(),
                        saleDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.saleID)
                .ForeignKey("dbo.books", t => t.bookid)
                .Index(t => t.bookid);
            
            CreateTable(
                "dbo.categoriesTBs",
                c => new
                    {
                        category = c.String(nullable: false, maxLength: 128),
                        categoryInfo = c.String(),
                    })
                .PrimaryKey(t => t.category);
            
            CreateTable(
                "dbo.dept2",
                c => new
                    {
                        deptid = c.Int(nullable: false, identity: true),
                        deptname = c.String(),
                        location = c.String(),
                    })
                .PrimaryKey(t => t.deptid);
            
            CreateTable(
                "dbo.items2",
                c => new
                    {
                        itemcode = c.Int(nullable: false, identity: true),
                        itemname = c.String(),
                        deptid = c.Int(),
                        cost = c.Decimal(precision: 18, scale: 2),
                        rate = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.itemcode)
                .ForeignKey("dbo.dept2", t => t.deptid)
                .Index(t => t.deptid);
            
            CreateTable(
                "dbo.student2",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        _class = c.String(name: "class"),
                        fee = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.items2", "deptid", "dbo.dept2");
            DropForeignKey("dbo.books", "categoriesTBcategory", "dbo.categoriesTBs");
            DropForeignKey("dbo.bookSales", "bookid", "dbo.books");
            DropForeignKey("dbo.bookPurchases", "bookid", "dbo.books");
            DropIndex("dbo.items2", new[] { "deptid" });
            DropIndex("dbo.bookSales", new[] { "bookid" });
            DropIndex("dbo.books", new[] { "categoriesTBcategory" });
            DropIndex("dbo.bookPurchases", new[] { "bookid" });
            DropTable("dbo.student2");
            DropTable("dbo.items2");
            DropTable("dbo.dept2");
            DropTable("dbo.categoriesTBs");
            DropTable("dbo.bookSales");
            DropTable("dbo.books");
            DropTable("dbo.bookPurchases");
        }
    }
}
