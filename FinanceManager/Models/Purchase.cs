using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinanceManager.Models
{
    public class Purchase
    {
        public int ID { get; set; }
        public string Place { get; set; }
        public decimal Amount { get; set; }
        public int NumberOfProducts { get; set; }

        public CurrencyEnum UOM { get; set; }

        public string FileName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Display(Name = "Created At")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }

        public string FileOriginalName { get; set; }

        public PurchaseCategory PurchaseCategory { get; set; }

        public PurchaseEventCategory PurchaseEventCategory { get; set; }

        /// <summary>
        /// Cast usually from proxy object to original Purchase object
        /// </summary>
        /// <returns></returns>
        public Purchase CastToSelf()
        {
            return new Purchase
            {
                ID = this.ID,
                Place = this.Place,
                Amount = this.Amount,
                NumberOfProducts = this.NumberOfProducts,
                UOM = this.UOM,
                FileName = this.FileName,
                Products = this.Products,
                User = this.User,
                CreatedAt = this.CreatedAt,
                FileOriginalName = this.FileOriginalName,
                PurchaseCategory = this.PurchaseCategory,
                PurchaseEventCategory = this.PurchaseEventCategory
            };
        }
    }
}