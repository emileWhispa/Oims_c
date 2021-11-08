using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("Discounts")]
    public class Discount
    {
        [NotMapped]
        public string TableName { get { return "Discounts"; } }
        [Column("DiscountId")]
        public int Id { get; set; }
        [Column("CustomerId")]
        public int CustomerId { get; set; }
        [Column("DiscountCode")]
        [StringLength(15)]
        public string DiscountCode { get; set; }
        [Column("DiscountRate")]
        public decimal DiscountRate { get; set; }
        [Column("DiscountDate")]
        public DateTime DiscountDate { get; set; }
        [Column("Comments")]
        [StringLength(150)]
        public string Comments { get; set; }
        [Column("Maker")]
        public int Maker { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer customer { get; set; }
        [ForeignKey("Maker")]
        public virtual User user { get; set; }
    }
}