using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("Userstatus")]
    public class UserStatus
    {
        [NotMapped]
        public string TableName { get { return "Userstatus"; } }
        [Column("Statusid")]
        public int Id { get; set; }
        [Column("Decription")]
        [StringLength(100)]
        public string Decription { get; set; }
    }
}
