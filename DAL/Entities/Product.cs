using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("Products")]
    public class Product
    {
        [NotMapped]
        public string TableName { get { return "Products"; } }

        [Column("ProductId")]
        public int Id { get; set; }

        [Column("ProductName")]
        [Display(Name = "Product Name")]
        [Required()]
        [StringLength(50)]
        public string ProductName { get; set; }

        [Column("Description")]
        [StringLength(150)]
        public string Description { get; set; }

        [Column("ReorderLevel")]
        [Required()]
        [Display(Name = "Reorder Level")]
        public decimal ReorderLevel { get; set; }

        [Column("PackagingUnit")]
        [Required()]
        [StringLength(10)]
        [Display(Name = "Packaging Unit")]
        public string PackagingUnit { get; set; }

        [Column("SellingPrice")]
        [Required()]
        [Display(Name = "Selling Price")]
        public decimal SellingPrice { get; set; }

        [Column("ProductCategoryId")]
        [Required()]
        [Display(Name = "Product Category")]
        public int ProductCategoryId { get; set; }
        [Column("Active")]
        public bool Active { get; set; }
        [ForeignKey("ProductCategoryId")]
        public virtual ProductCategory ProductCategory { get; set; }
    }
}
