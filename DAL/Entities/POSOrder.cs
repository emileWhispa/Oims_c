using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("POSOrders")]
    public class POSOrder
    {
        [NotMapped]
        public string TableName { get { return "POSOrders"; } }

        [Column("OrderId")]
        public int Id { get; set; }

        [Column("POSId")]
        [Display(Name = "POS")]
        public int POSId { get; set; }

        [Column("OrderDate")]
        [Required()]
        [Display(Name = "Order Date")]
        [UIHint("DatePicker")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }

        [Column("DeliveryDate")]
        [Display(Name = "Delivery Date")]
        [UIHint("DatePicker")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DeliveryDate { get; set; }

        [Column("ReceptionDate")]
        [Display(Name = "Receive Date")]
        [UIHint("DatePicker")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReceptionDate { get; set; }


        [Column("DispatchDate")]
        [Display(Name = "Dispatch Date")]
        [UIHint("DatePicker")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DispatchDate { get; set; }

        [Column("OrderNo")]
        [StringLength(20)]
        public string OrderNo { get; set; }

        [Column("AddInfo")]
        [StringLength(130)]
        public string AddInfo { get; set; }

        [Column("Status")]
        public int Status { get; set; }

        [Column("OrderStatus")]
        public OrderStatus OrderStatus { get; set; }

        [Column("UserId")]
        public int UserId { get; set; }

        [Column("DispatcherId")]
        public int DispatcherId { get; set; }

        [Column("ReceiverId")]
        public int ReceiverId { get; set; }

        [ForeignKey("UserId")]
        public virtual User Maker { get; set; }

        [ForeignKey("DispatcherId")]
        public virtual User Dispatcher { get; set; }

        [ForeignKey("ReceiverId")]
        public virtual User Receiver { get; set; }

        [ForeignKey("POSId")]
        public virtual POSe Pose { get; set; }

        public POSOrder()
        {
            this.OrderDate = DateTime.Now;
            this.DeliveryDate = DateTime.Now;
            this.ReceptionDate = DateTime.Now;
            this.DispatchDate = DateTime.Now;
        }

    }
}
