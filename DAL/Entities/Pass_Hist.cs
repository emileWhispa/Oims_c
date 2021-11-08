using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("Pass_Hist")]
    public class Pass_Hist
    {
        [NotMapped]
        public string TableName { get { return "Pass_Hist"; } }
        [Column("HistId")]
        public int Id { get; set; }
        [Column("UserId")]
        [Required()]
        public int UserId { get; set; }
        [Column("Password")]
        [Required()]
        [StringLength(100)]
        public string Password { get; set; }
        [Column("PassDate")]
        [Required()]
        public DateTime PassDate { get; set; }
    }
}
