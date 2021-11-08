using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("Accounts")]
    public class Account
    {
        [NotMapped]
        public string TableName { get { return "Accounts"; } }
        [Column("AccountId")]
        public int Id { get; set; }

        [Column("AccountNo")]
        [Required()]
        [StringLength(20)]
        [Display(Name = "Account No")]
        public string AccountNo { get; set; }

        [Column("AccountName")]
        [Required()]
        [StringLength(30)]
        [Display(Name = "Account Name")]
        public string AccountName { get; set; }

        [Column("BankId")]
        [Required()]
        [Display(Name = "Bank")]
        public int BankId { get; set; }

        [Column("BranchId")]
        [Required()]
        [Display(Name = "Branch")]
        public int BranchId { get; set; }

        [Column("Balance")]
        public decimal Balance { get; set; }
        [Column("CurrencyId")]
        [Required()]
        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }

        //----Navigator properties

        [ForeignKey("BankId")]
        public virtual Bank Bank { get; set; }
        [ForeignKey("CurrencyId")]
        public virtual Currency Currency { get; set; }
        [ForeignKey("BranchId")]
        public virtual Branch Branch { get; set; }
    }
}
