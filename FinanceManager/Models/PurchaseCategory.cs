using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinanceManager.Models
{
    public class PurchaseCategory
    {
        public int ID { get; set; }

        [Display(Name = "Name")]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}