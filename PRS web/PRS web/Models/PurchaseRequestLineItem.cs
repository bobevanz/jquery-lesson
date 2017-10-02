﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRS_web.Models
{
    public class PurchaseRequestLineItem{
        public int Id { get; set; }
        public int PurchaseRequestId { get; set; }
        public PurchaseRequest purchaserequest { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

    }
}