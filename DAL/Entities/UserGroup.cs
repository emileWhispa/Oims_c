using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("UserGroups")]
    public class UserGroup
    {
        [NotMapped]
        public string TableName { get { return "UserGroups"; } }

        [Column("UserGroupId")]
        public int Id { get; set; }

        [Column("GroupName")]
        [Required()]
        [StringLength(20)]
        public string GroupName { get; set; }

        [Column("Description")]
        [Required()]
        [StringLength(50)]
        public string Description { get; set; }

        public int UserType { get; set; }
    }
}
