using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("ItemDelivery")]
    public class ItemDelivery
    {
        [NotMapped]
        public string TableName { get { return "ItemDelivery"; } }
        [Column("DeliveryId")]
        public int Id { get; set; }
        [Column("BatchNo")]
        [Required()]
        [StringLength(20)]
        public string BatchNo { get; set; }
        [Column("SalesDetailsId")]
        [Required()]
        public int SalesDetailsId { get; set; }
        [Column("QuantityDelivered")]
        [Required()]
        public int QuantityDelivered { get; set; }
        [Column("QuantityRemaining")]
        [Required()]
        public int QuantityRemaining { get; set; }
        [Column("QuantityPicked")]
        [Required()]
        public int QuantityPicked { get; set; }
        [Column("Status")]
        [Required()]
        public int Status { get; set; }
        [Column("UserId")]
        [Required()]
        public int UserId { get; set; }
        [Column("DeliveryDate")]
        public DateTime DeliveryDate { get; set; }

        [ForeignKey("SalesDetailsId")]
        public virtual SalesDetail saleDetail { get; set; }


        [ForeignKey("UserId")]
        public virtual User usr { get; set; }


    }
}
