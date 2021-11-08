using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("ProductCategories")]
    public class ProductCategory
    {
        [NotMapped]
        public string TableName { get { return "ProductCategories"; } }

        [Column("ProductCategoryId")]
        public int Id { get; set; }

        [Column("CategoryName")]
        [Display(Name = "Category Name")]
        [Required()]
        [StringLength(50)]
        public string CategoryName { get; set; }

    }
}
