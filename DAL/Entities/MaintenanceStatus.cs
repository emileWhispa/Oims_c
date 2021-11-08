using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("MaintenanceStatus")]
    public class MaintenanceStatus
    {
        [NotMapped]
        public string TableName { get { return "MaintenanceStatus"; } }
        [Column("StatusId")]
        public int Id { get; set; }
        [Column("StatusName")]
        [StringLength(30)]
        public string StatusName { get; set; }
        [Column("Description")]
        [StringLength(100)]
        public string Description { get; set; }
    }
}
