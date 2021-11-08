using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("UserAudit")]
    public class UserAudit
    {
        [NotMapped]
        public string TableName { get { return "UserAudit"; } }
        [Column("User_Adt_TrlId")]
        public int Id { get; set; }
        [Column("UserId")]
        [Required()]
        public int UserId { get; set; }
        [Column("ActionDescr")]
        [Required()]
       
        public string ActionDescr { get; set; }
        [Column("ModuleId")]
        [Required()]
        public int ModuleId { get; set; }
        [Column("Mdl_Function")]
        [Required()]
        [StringLength(50)]
        public string Mdl_Function { get; set; }
        [Column("AuditDate")]
        [Required()]
        public DateTime AuditDate { get; set; }
        [Column("ClntIP")]
        [Required()]
        [StringLength(50)]
        public string ClntIP { get; set; }
        [Column("Browser")]
        [Required()]
        [StringLength(30)]
        public string Browser { get; set; }
    }
}
