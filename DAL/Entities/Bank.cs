using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("Banks")]
    public class Bank
    {
        [NotMapped]
        public string TableName { get { return "Banks"; } }
        [Column("BankId")]
        public int Id { get; set; }
        [Column("BankName")]
        [Required()]
        [StringLength(50)]
        public string BankName { get; set; }
    }
}
