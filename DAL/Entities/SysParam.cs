using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("SysParams")]
    public class SysParam
    {
        [NotMapped]
        public string TableName { get { return "SysParams"; } }
        [Column("SysParamId")]
        public int Id { get; set; }
        [Column("ParamName")]
        [Required()]
        [StringLength(30)]
        public string ParamName { get; set; }
        [Column("ParamValue")]
        [Required()]       
        public string ParamValue { get; set; }
        [Column("Descr")]
        [Required()]
        [StringLength(100)]
        public string Descr { get; set; }
    }
}
