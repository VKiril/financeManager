using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinanceManager.Models.RequestReceivers
{
    [NotMapped]
    public class PurchaseCreateReceiver
    {
        public string Place { get; set; }
        public string Amount { get; set; }
        public int NumberOfProducts { get; set; }
        public CurrencyEnum UOM { get; set; }

        [Display(Name = "Created At")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }

        public string FileOriginalName { get; set; }
        public int PurchaseCategory { get; set; }
        public int PurchaseEventCategory { get; set; }

        public Purchase CastToPurchase()
        {
            return new Purchase
            {
                Place = this.Place,
                Amount = decimal.Parse(this.Amount.Replace(".", ",")),
                NumberOfProducts = this.NumberOfProducts,
                UOM = this.UOM,
                CreatedAt = this.CreatedAt,
            };
        }
    }
}