using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("ExpenseCategories")]
    public class ExpenseCategory
    {
        [NotMapped]
        public string TableName { get { return "ExpenseCategories"; } }

        [Column("ExpenseCategoryId")]
        public int Id { get; set; }

        [Column("ExpenseGroupId")]
        public int ExpenseGroupId { get; set; }

        [Column("ExpenseName")]
        [StringLength(50)]
        [Display (Name="Expense Name")]
        public string ExpenseName { get; set; }

        [Column("SageLedgerNo")]
        [Display(Name = "Sage Ledger No")]
        public int SageLedgerNo { get; set; }

        [ForeignKey("ExpenseGroupId")]
        public virtual ExpenseGroup expensegroup { get; set; }
        
    }
}
