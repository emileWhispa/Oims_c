using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("PaymentModes")]
    public class PaymentMode
    {
        [NotMapped]
        public string TableName { get { return "PaymentModes"; } }

        [Column("ModeId")]
        public int Id { get; set; }

        [Column("PaymentName")]
        [Required()]
        [StringLength(30)]
        public string PaymentName { get; set; }

        [Column("Description")]
        [StringLength(100)]
        public string Description { get; set; }
    }
}
