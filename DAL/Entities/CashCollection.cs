using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("CashCollections")]
    public class CashCollection
    {
        [NotMapped]
        public string TableName { get { return "CashCollections"; } }
        [Column("CashCollectionId")]
        public int Id { get; set; }
        [Column("CollectorName")]
        [StringLength(120)]
        public string CollectorName { get; set; }
        [Column("CollectionDate")]
        public DateTime CollectionDate { get; set; }
        [Column("TotalCash")]
        public decimal TotalCash { get; set; }
        [Column("TotalCheque")]
        public decimal TotalCheque { get; set; }
        [Column("UserId")]
        public int UserId { get; set; }
        [Column("POSID")]
        public int POSID { get; set; }

        //[ForeignKey("UserId")]
        //public virtual User User { get; set; }

        //[ForeignKey("POSID")]
        //public virtual POSe POS { get; set; }
    }
}
