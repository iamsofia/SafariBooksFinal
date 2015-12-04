using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace IdentityTemplate.Models
{
    public class CartItem
    {
        
            [Key]
            public int SKU { get; set; }
            public int CartId { get; set; }
           // public int BookId { get; set; }
            public int Quantity { get; set; }

            public virtual Cart Cart { get; set; }
            public virtual Book Book { get; set; }
        


    }
}