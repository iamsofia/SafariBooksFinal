using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityTemplate.Models
{
    public class Book
    
    {
        [Key]
        public int SKU { get; set; }

        public string Title { get; set; }


        [Display(Name = "Author First")]
        public string AuthorFirst { get; set; }

        [Display(Name = "Author Last")]
        public string AuthorLast { get; set; }

        public string Genre { get; set; }


        [Display(Name = "Publication Date ")]
        public DateTime PublicationDate { get; set; }

        public decimal Price { get; set; }

        [Display(Name = "Price Last Paid")]
        public decimal PriceLastPaid { get; set; }

        public int Inventory { get; set; }


        [Display(Name = "Reorder Point")]
        public int ReorderPoint { get; set; }

        public bool Discontinued { get; set; }

        
        

        //public bool Active { get; set; }


        public virtual List<Review> Reviews { get; set; }
        
    }
}