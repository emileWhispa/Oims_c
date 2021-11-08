using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("PaymentTypes")]
    public class PaymentType
    {
        [NotMapped]
        public string TableName { get { return "PaymentTypes"; } }

        [Column("TypeId")]
        public int Id { get; set; }

        [Column("TypeName")]
        [Required()]
        [StringLength(30)]
        public string TypeName { get; set; }

        [Column("Description")]
        [StringLength(100)]
        public string Description { get; set; }
    }
}
