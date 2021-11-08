using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("Expenses")]
    public class Expense
    {
        [NotMapped]
        public string TableName { get { return "Expenses"; } }
        [Column("ExpenseId")]
        [Required()]
        public int Id { get; set; }
        [Column("ExpenseCategoryId")]
        public int ExpenseCategoryId { get; set; }
        [Column("Amount")]
        public decimal Amount { get; set; }
        [Column("CurrencyId")]
        public int CurrencyId { get; set; }
        [Column("ExchangeRate")]
        public decimal ExchangeRate { get; set; }
        [Column("ExpenseDate")]
        [UIHint("DatePicker")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        [Required()]
        public DateTime ExpenseDate { get; set; }
        [Column("Description")]
        [StringLength(130)]
        public string Description { get; set; }
        [Column("UserId")]
        public int UserId { get; set; }
        [Column("vatrate")]
        public decimal vatrate { get; set; }

        [ForeignKey("ExpenseCategoryId")]
        public virtual ExpenseCategory expensecategory { get; set; }

        [ForeignKey("CurrencyId")]
        public virtual Currency currency { get; set; }

        [ForeignKey("UserId")]
        public virtual User user { get; set; }
    }
}
