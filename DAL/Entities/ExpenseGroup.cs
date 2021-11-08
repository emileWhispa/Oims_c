using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("ExpenseGroups")]
    public class ExpenseGroup
    {
        [NotMapped]
        public string TableName { get { return "ExpenseGroups"; } }
        [Column("ExpenseGroupId")]
        [Required()]
        public int Id { get; set; }
        [Column("ExpenseGroupName")]
        [StringLength(120)]
        public string ExpenseGroupName { get; set; }
    }
}
