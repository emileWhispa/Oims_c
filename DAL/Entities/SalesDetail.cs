using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace orion.ims.DAL
{
    [Table("SalesDetails")]
    public class SalesDetail
    {
        [NotMapped]
        public string TableName { get { return "SalesDetails"; } }
        [Column("SalesDetailsId")]
        public int Id { get; set; }
        [Column("SalesId")]
        public int SalesId { get; set; }

        [Column("ProductId")]
        [Required()]
        public int ProductId { get; set; }

        [Column("Quantity")]
        public decimal Quantity { get; set; }
        [Column("UnitPrice")]
        public decimal UnitPrice { get; set; }
        [Column("Discounts")]
        public decimal Discounts { get; set; }
        [Column("Vatable")]
        public int Vatable { get; set; }
        [Column("vat_rate")]
        public decimal vatrate { get; set; }
        [Column("ManualDescr")]
        public string ManualDescr { get; set; }
        // Navigation properties

        [ForeignKey("SalesId")]
        public virtual Sale sale { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product product { get; set; }
    }
}
