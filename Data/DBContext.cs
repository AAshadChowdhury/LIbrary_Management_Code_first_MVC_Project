using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using CF__MVC_Project.DAL;

namespace CF__MVC_Project.Data
{

    public class DBContext : DbContext
    {
        public DBContext() : base("name=Model1")
        {
        }
        public virtual DbSet<bookPurchase> bookPurchases { get; set; }
        public virtual DbSet<book> books { get; set; }
        public virtual DbSet<bookSale> bookSales { get; set; }
        public virtual DbSet<categoriesTB> categoriesTBs { get; set; }
        public virtual DbSet<dept2> dept2 { get; set; }
        public virtual DbSet<items2> items2 { get; set; }
        public virtual DbSet<student2> student2 { get; set; }

    }
}