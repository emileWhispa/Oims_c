using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("Orders")]
    public class Order
    {
        [NotMapped]
        public string TableName { get { return "Orders"; } }

        [Column("OrderId")]
        public int Id { get; set; }

        [Column("OrderDate")]
        [Required()]
        [Display(Name="Order Date")]
        [UIHint("DatePicker")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }

        [Column("OrderNo")]
        ////[Required()]
        [StringLength(20)]
        [Display(Name = "Order No")]
        public string OrderNo { get; set; }

        [Column("SupplierId")]
        [Required()]
        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }

        [Column("Authorized")]
        public int Authorized { get; set; }

        [Column("ProviderId")]
        [Required()]
        [Display(Name = "Transporter")]
        public int ProviderId { get; set; }

        [Column("TransportCharge")]
        public decimal TransportCharge { get; set; }
        [Column("OrderStatus")]
        [Required()]
        [Display(Name = "Order Status")]
        public int OrderStatus { get; set; }

        [Column("TruckId")]
        [Required()]
        [Display(Name = "Truck")]
        public int TruckId { get; set; }

        [Column("OrderPrice")]
        public decimal OrderPrice { get; set; }

        [Column("DriverId")]
        [Required()]
        [Display(Name = "Driver")]
        public int DriverId { get; set; }

        [Column("ProductId")]
        [Required()]
        [Display(Name = "Product")]
        public int ProductId { get; set; }

        [Column("Quantity")]
        [Required()]
        public Decimal Quantity{ get; set; }

        [Column("Status")]
        public int Status { get; set; }

        [Column("AddInfo")]
        [StringLength(130)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Remarks")]
        public string AddInfo { get; set; }

        [Column("MakerId")]
        public int MakerId { get; set; }

        [Column("ApproverId")]
        public int ApproverId { get; set; }

        [Column("CustomerId")]
        public int CustomerId { get; set; }
        //---- Navigation properties ----
        [ForeignKey("MakerId")]
        public virtual User Maker { get; set; }
        [ForeignKey("ApproverId")]
        public virtual User Approver { get; set; }
       
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
       

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

       
    }
}
