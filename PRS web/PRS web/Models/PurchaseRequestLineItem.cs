using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRS_web.Models
{
    public class PurchaseRequestLineItem{
        public int Id { get; set; }
        public virtual PurchaseRequestLineItem PurchaseRequestId { get; set; }
        public virtual PurchaseRequest purchaserequest { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }

    }
}