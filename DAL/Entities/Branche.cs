using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("Branches")]
    public class Branch
    {
        [NotMapped]
        public string TableName { get { return "Branches"; } }

        [Column("BranchId")]
        public int Id { get; set; }

        [Column("BankId")]
        [Required()]
        [Display(Name = "Bank")]
        public int BankId { get; set; }

        [Column("RegionId")]
        [Required()]
        [Display(Name = "Region")]
        public int RegionId { get; set; }

        [Column("BranchCode")]
        [Required()]
        [StringLength(5)]
        [Display(Name = "Branch Code")]
        public string BranchCode { get; set; }

        [Column("BranchName")]
        [Required()]
        [StringLength(50)]
        [Display(Name = "Branch Name")]
        public string BranchName { get; set; }

        [Column("CBS_Code")]
        [Required()]
        [StringLength(10)]
        [Display(Name = "CBS Code")]
        public string CBS_Code { get; set; }


        [ForeignKey("BankId")]
        public virtual Bank Bank { get; set; }

        [ForeignKey("RegionId")]
        public virtual Region Region { get; set; }
    }
}
