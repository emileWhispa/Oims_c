using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("Currencies")]
    public class Currency
    {
        [NotMapped]
        public string TableName { get { return "Currencies"; } }
        [Column("CurrencyId")]
        [Required()]
        public int Id { get; set; }
        [Column("CurrencyCode")]
        [Required()]
        [StringLength(5)]
        public string CurrencyCode { get; set; }
        [Column("CurrencyName")]
        [Required()]
        [StringLength(30)]
        public string CurrencyName { get; set; }
        [Column("ExchangeRate")]
        [Required()]
        public decimal ExchangeRate { get; set; }
    }
}
