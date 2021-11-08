using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("SpecialOrders")]
    public class SpecialOrder
    {
        [NotMapped]
        public string TableName { get { return "SpecialOrders"; } }

        [Column("SpecialOrderId")]
        public int Id { get; set; }

        [Column("CustomerId")]
        [Required()]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [Column("Descr")]
        [Required()]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Descr { get; set; }

        [Column("Quantity")]
        [Required()]
        public int Quantity { get; set; }

        [Column("OrderDate")]
        [Required()]
        [Display(Name = "Order Date")]
        [UIHint("DatePicker")]
        public DateTime OrderDate { get; set; }

        [Column("ExpectedDeliveryDate")]
        [Required()]
        [Display(Name = "Delivery Date")]
        [UIHint("DatePicker")]
        public DateTime ExpectedDeliveryDate { get; set; }

        [Column("Amount")]
        [Required()]
        [Display(Name = "Order Amount")]
        public decimal Amount { get; set; }
      
        [Column("ProductCategoryId")]
        [Required()]
        [Display(Name = "Product Type")]
        public int ProductCategoryId { get; set; }

        public int OrderStatus { get; set; }

        [Column("Sample")]
        public bool Sample { get; set; }

        [Column("CollectionPos")]
        [Required()]
        [Display(Name = "Collection POS")]
        public int CollectionPosId { get; set; }

        [Column("POSId")]
        [Required()]
        [Display(Name = "Origin POS")]
        public int POSId { get; set; }

        [Column("UserId")]
        [Required()]
        public int UserId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("ProductCategoryId")]
        public virtual ProductCategory ProductCategory { get; set; }
        
    }
}