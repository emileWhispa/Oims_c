using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("Rights")]
    public class Right
    {
        [NotMapped]
        public string TableName { get { return "Rights"; } }

        [Column("RightId")]
        public int Id { get; set; }

        [Column("MenuId")]
        public int MenuId { get; set; }

        [Column("UserGroupId")]
        public int UserGroupId { get; set; }

        [Column("AllowAccess")]
        public bool AllowAccess { get; set; }

        [Column("AuthStatus")]
        public int AuthStatus { get; set; }        

        [ForeignKey("MenuId")]
        public virtual Menu Menu { get; set; }

        [ForeignKey("UserGroupId")]
        public virtual UserGroup UserGroup { get; set; }
    }
}
