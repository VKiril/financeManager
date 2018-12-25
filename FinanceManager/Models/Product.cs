using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinanceManager.Models
{
    public class Product
    {
        public int ID { get; set; }

        [StringLength(30, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        [Required]
        public float Quantity { get; set; }

        [Display(Name = "Product type")]
        [Required]
        public ProductTypeEnum ProductType { get; set; }

        [Required]
        public float Cost { get; set; }

        [Display(Name = "Cost per unit")]
        public float CostPerUnit { get; set; }

        [Display(Name = "For Who")]
        [StringLength(30, MinimumLength = 3)]
        public string ForWho { get; set; }

        [Display(Name = "Minimal necessary")]
        [DefaultValue(false)]
        public bool IsMinimalNecesarry { get; set; }

        public string FileOriginalName { get; set; }

        public string FileName { get; set; }

        [NotMapped]
        public int PurchaseIdReceiver { get; set; }

        public virtual Purchase Purchase { get; set; }
    }
}