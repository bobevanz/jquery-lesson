using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PRS_web.Models
{
    public class PurchaseRequest
    {
        public int Id { get; set; }
        public virtual User UserId { get; set; }
        public virtual User User { get; set; }
        [StringLength(30)]
        [Required]
        public string Description { get; set; }
        [StringLength(30)]
        [Required]
        public string Justification { get; set; }
        public DateTime DateNeeded { get; set; }
        [StringLength(30)]
        [Required]
        public string DeliveryMode { get; set; }
        [StringLength(30)]
        [Required]
        public string Status { get; set; }
        [Required]
        public double Total { get; set; }
        [Required]
        public  DateTime SubmittedDate { get; set; }
}
}