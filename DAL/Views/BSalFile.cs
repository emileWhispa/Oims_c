using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace orion.ims.DAL
{
    public class BSalFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileDir { get; set; }
        public long Size { get; set; }
        public DateTime CreateTime { get; set; }
        public bool Imported { get; set; }
        public int RecordCount { get; set; }
        public decimal SumAmount { get; set; }
        public int RejectsCount { get; set; }
        public decimal RejectsAmount { get; set; }
        public string Sender { get; set; }
        public string BatchNo { get; set; }
    }
}
