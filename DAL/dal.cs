using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CF__MVC_Project.DAL
{

    public class categoriesTB
    {

        [Key]
        public string category { get; set; }
        public string categoryInfo { get; set; }

        public virtual ICollection<book> books { get; set; }
    }
    public class book
    {

        public int id { get; set; }//auto increament
        public string name { get; set; }
        public string author { get; set; }
        public string publisher { get; set; }
        public string categoriesTBcategory { get; set; }//foreign key link+primary key
        public Nullable<int> stockQuantity { get; set; }
        public Nullable<decimal> price { get; set; }
        public string cover { get; set; }

        public virtual ICollection<bookPurchase> bookPurchases { get; set; }
        public virtual categoriesTB categoriesTB { get; set; }
        public virtual ICollection<bookSale> bookSales { get; set; }
    }
    public class bookPurchase
    {
        [Key]
        public int purchaseID { get; set; }
        public Nullable<int> bookid { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<System.DateTime> purchaseDate { get; set; }

        public virtual book book { get; set; }
    }
    public class bookSale
    {
        [Key]
        public int saleID { get; set; }
        public Nullable<int> bookid { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<System.DateTime> saleDate { get; set; }

        public virtual book book { get; set; }
    }

    public class dept2
    {

        [Key]
        public int deptid { get; set; }
        public string deptname { get; set; }
        public string location { get; set; }

        public virtual ICollection<items2> items2 { get; set; }
    }
    public class items2
    {
        [Key]
        public int itemcode { get; set; }
        public string itemname { get; set; }
        [ForeignKey("dept2")]
        public Nullable<int> deptid { get; set; }
        public Nullable<decimal> cost { get; set; }
        public Nullable<decimal> rate { get; set; }

        public virtual dept2 dept2 { get; set; }
    }
    public class student2
    {
        public int id { get; set; }
        public string name { get; set; }
        public string @class { get; set; }
        public Nullable<decimal> fee { get; set; }
    }
}