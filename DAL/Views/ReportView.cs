using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace orion.ims.DAL
{
    public class ReportView
    {
        [Display(Name = "Data Source")]
        public int SourceId { get; set; }

        [Display(Name = "Report Name")]
        public int ReportId { get; set; }

        [Display(Name = "Report Type")]
        public int ReportTypeId { get; set; }

        [UIHint("DatePicker")]
        [Display(Name = "Date From")]
        public DateTime DateFrom { get; set; }

        [UIHint("DatePicker")]
        [Display(Name = "Date To")]
        public DateTime DateTo { get; set; }

        public int RecordId { get; set; }
        public ReportView()
        {
            this.DateFrom = DateTime.Now;
            this.DateTo = DateTime.Now;
            this.RecordId = 0;
        }
    }
}
