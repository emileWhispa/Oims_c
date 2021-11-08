using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("Reports")]
    public class Report
    {
        [NotMapped]
        public string TableName { get { return "Reports"; } }

        [Column("ReportId")]
        public int Id { get; set; }

        [Column("Filename")]
        [Required()]
        [StringLength(50)]
        public string Filename { get; set; }

        [Column("Title")]
        [Required()]
        [StringLength(100)]
        public string Title { get; set; }

        [Column("Datasource")]
        [Required()]
        [StringLength(50)]
        public string Datasource { get; set; }

        [Column("Showreport")]
        [Required()]
        public bool Showreport { get; set; }

        [Column("ReportCat")]
        [Required()]
        public int ReportCat { get; set; }

        [Column("Datasource_Type")]
        [Required()]
        public int Datasource_Type { get; set; }

        [Column("Filters")]
        [Required()]
        [StringLength(50)]
        public string Filters { get; set; }

        [Column("Excel_Cols")]
        [Required()]
        [StringLength(1000)]
        public string Excel_Cols { get; set; }

        [Column("Date_Filter")]
        [Required()]
        public bool Date_Filter { get; set; }

        [Column("Date_Col")]
        [Required()]
        [StringLength(20)]
        public string Date_Col { get; set; }

        [Column("ShowAll")]
        [Required()]
        public bool ShowAll { get; set; }
    }
}
