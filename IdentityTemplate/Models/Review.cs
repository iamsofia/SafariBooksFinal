﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IdentityTemplate.Models
{
    public class Review
    {
        public int ReviewID { get; set; }

        //  1:N 
        //SKU: Review

        public int SKU { get; set; }
        [ForeignKey("SKU")]
        public virtual Book Book { get; set; }

        [Display(Name = "Customer Review")]
        [StringLength(100, ErrorMessage = "The book review cannot exceed 100 characters.")]
        public string CustomerReview { get; set; }


        public virtual AppUser User { get; set; }
        // [ForeignKey("User")]

        [Display(Name = "Approve/Reject")]
        public bool IsApproved { get; set; }


        [Display(Name = "Customer Rating")]
        public int Rating { get; set; }

    }
}