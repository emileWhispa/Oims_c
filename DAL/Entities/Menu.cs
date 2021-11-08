using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("Menus")]
    public class Menu
    {
        [NotMapped]
        public string TableName { get { return "Menus"; } }

        [Column("MenuId")]
        public int Id { get; set; }

        [Column("ModuleId")]
        [Required()]
        public int ModuleId { get; set; }

        [Column("Menu_Name")]
        [Required()]
        [StringLength(20)]
        public string Menu_Name { get; set; }

        [Column("Url")]
        [Required()]
        [StringLength(100)]
        public string Url { get; set; }

        [Column("Menu_Level")]
        [Required()]
        public int Menu_Level { get; set; }

        [Column("Parent_Id")]
        [Required()]
        public int Parent_Id { get; set; }

        [Column("Sign")]
        [StringLength(20)]
        public string Sign { get; set; }

        [Column("Active")]
        [Required()]
        public bool Active { get; set; }

        //[Column("MenuCode")]
        //[StringLength(5)]
        //public string MenuCode { get; set; }
    }
}
