using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("ProductionItems")]
    public class ProductionItem
    {
        [NotMapped]
        public string TableName { get { return "ProductionItems"; } }

        [Column("ProductionItemId")]
        public int Id { get; set; }

        [Column("ProductionId")]
        [Required()]
        public int ProductionId { get; set; }

        [Column("ProductId")]
        [Required()]
        [Display(Name = "Product Name")]
        public int ProductId { get; set; }

        [Column("Units")]
        [Required()]
        public int Units { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        [ForeignKey("ProductionId")]
        public virtual Production Production { get; set; }
    }
}
