using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PRS_web.Models
{
    public class Product
    {
        public int ID { get; set; }
        public int VendorId { get; set; } //
        public Vendor vendor { get; set; } //
        [StringLength(30)]
        [Required]
        public string PartNumber { get; set; }
        [StringLength(30)]
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [StringLength(30)]
        [Required]
        public string Unit { get; set; }
        [StringLength(50)]
        [Required]
        public string PhotoPath { get; set; }

    }
}