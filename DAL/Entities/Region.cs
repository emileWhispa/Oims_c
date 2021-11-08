using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("Regions")]
    public class Region
    {
        [NotMapped]
        public string TableName { get { return "Regions"; } }
        [Column("RegionId")]
        public int Id { get; set; }
        [Column("RegionCode")]
        [Required()]
        [StringLength(5)]
        public string RegionCode { get; set; }
        [Column("RegionName")]
        [Required()]
        [StringLength(30)]
        public string RegionName { get; set; }
        [Column("CBS_Code")]
        [Required()]
        [StringLength(10)]
        public string CBS_Code { get; set; }
    }
}
