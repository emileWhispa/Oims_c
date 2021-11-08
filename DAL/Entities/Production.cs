using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("Productions")]
    public class Production
    {
        [NotMapped]
        public string TableName { get { return "Productions"; } }

        [Column("ProductionId")]
        public int Id { get; set; }

        [Column("Prod_Date")]
        [Required()]
        //[UIHint("DatePicker")]
        //[DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        //[Display(Name = "Production Date")]
        public DateTime Prod_Date { get; set; }

        [Column("Remarks")]
        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }
        [Column("Authorized")]
        public int Authorized { get; set; }
        [Column("BatchNo")]
        [StringLength(15)]
        [Display(Name = "Batch No")]
        public string BatchNo { get; set; }

        [Column("UserId")]
        public int UserId { get; set; }

    }
}