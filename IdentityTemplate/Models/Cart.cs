using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityTemplate.Models
{
    public class Cart
    {

        public int SKU { get; set; }
        [Index(IsUnique = true)]
        [StringLength(255)]
        public string SessionSKU { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
