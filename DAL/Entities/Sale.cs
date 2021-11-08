using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("Sales")]
    public class Sale
    {

        [NotMapped]
        public string TableName { get { return "Sales"; } }

        [Column("SalesId")]
        public int Id { get; set; }

        [Column("QuoteDate")]
        [Required()]
        [DisplayFormat(DataFormatString = "{0:yyyy-MMM-dd}", ApplyFormatInEditMode = true)]
        public DateTime QuoteDate { get; set; }

        [Column("Remarks")]
        [StringLength(200)]
        public string Remarks { get; set; }

        [Column("CustomerId")]
        [Required()]
        public int CustomerId { get; set; }

        [Column("DeliveryDate")]
        public DateTime? DeliveryDate { get; set; }
        
        [Column("InvoiceNumber")]
        public string InvoiceNumber { get; set; }
        [Column("Deposit")]
        public decimal Deposit { get; set; }

        [Column("OrderDate")]
        public DateTime? OrderDate { get; set; }

        [Column("InvoiceDate")]
        public DateTime? InvoiceDate { get; set; }

        [Column("SalesRef")]
        [StringLength(14)]
        public string SalesRef { get; set; }

        [Column("MakerId")]
        public int MakerId { get; set; }

        [Column("ApproverId")]
        public int? ApproverId { get; set; }

        [Column("PaymentStatus")]
        public int PaymentStatus { get; set; }

        [Column("POSId")]
        public int POSId { get; set; }

        [Column("DiscountCustomerId")]
        public int DiscountCustomerId { get; set; }

        //---- Navigation properties ----
        [ForeignKey("MakerId")]
        public virtual User Maker { get; set; }

        [ForeignKey("ApproverId")]
        public virtual User Approver { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("POSId")]
        public virtual POSe POSe { get; set; }
    }
}