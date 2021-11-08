using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("Maintenance_Auth")]
    public class Maintenance_Auth
    {
        [NotMapped]
        public string TableName { get { return "Maintenance_Auth"; } }
        [Column("MaintId")]
        public int Id { get; set; }
        [Column("ActionName")]
        [Required()]
        [StringLength(20)]
        public string ActionName { get; set; }
        [Column("Description")]
        [Required()]
        [StringLength(1000)]
        public string Description { get; set; }
        [Column("Maker")]
        [Required()]
        public int Maker { get; set; }
        [Column("Checker")]
        [Required()]
        public int Checker { get; set; }
        [Column("ActionString")]
        [StringLength(1000)]
        public string ActionString { get; set; }
        [Column("ActionDate")]
        [Required()]
        public DateTime ActionDate { get; set; }
        [Column("ActionSalt")]
        [Required()]
        [StringLength(50)]
        public string ActionSalt { get; set; }
        [Column("Modulename")]
        [Required()]
        [StringLength(50)]
        public string Modulename { get; set; }
        [Column("statusid")]
        [Required()]
        public int Statusid { get; set; }
        [Column("ActionChecks")]
        [StringLength(200)]
        public string ActionChecks { get; set; }
    }
}
