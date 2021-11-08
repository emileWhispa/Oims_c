using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("Credits")]
    public class Credit
    {
        [NotMapped]
        public string TableName { get { return "Credits"; } }

        [Column("CreditId")]
        public int Id { get; set; }

        [Column("CustomerId")]
        [Required()]
        public int CustomerId { get; set; }
        [Column("Balance")]
        [Required()]
        public decimal Balance { get; set; }

        [Column("Amount")]
        [Required()]
        public decimal Amount { get; set; }

        [Column("TransactionDate")]
        [Required()]
        public DateTime TransactionDate { get; set; }

        [Column("AddInfo")]
        [StringLength(130)]
        public string AddInfo { get; set; }

       
        [ForeignKey("CustomerId")]
        public virtual Customer customer { get; set; }
    }
}
