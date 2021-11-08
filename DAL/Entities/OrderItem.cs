using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("OrderItems")]
    public class OrderItem
    {
        [NotMapped]
        public string TableName { get { return "OrderItems"; } }

        [Column("OrderItemId")]
        public int Id { get; set; }

        [Column("OrderId")]
        [Required()]
        public int OrderId { get; set; }

        [Column("ProductId")]
        [Required()]
        [Display(Name = "Product")]
        public int ProductId { get; set; }

        [Column("QuantityOrdered")]
        [Required()]
        [Display(Name = "Quantity Ordered")]
        public decimal QtyOrdered { get; set; }

        [Column("QuantityDelivered")]
        public decimal QtyDelivered { get; set; }

        [Column("QuantityReceived")]
        public decimal QtyReceived { get; set; }

        //---- Navigation properties ----
        [ForeignKey("OrderId")]
        public virtual POSOrder Order { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

       }
}
