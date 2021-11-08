using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("POSItemTransfers")]
    public class POSItemTransfer
    {
        [NotMapped]
        public string TableName { get { return "POSItemTransfers"; } }
        [Column("TransferId")]
        public int Id { get; set; }
        [Column("RequestPOSId")]
        public int RequestPOSId { get; set; }
        [Column("SupplyingPOSId")]
        public int SupplyingPOSId { get; set; }
        [Column("ProductId")]
        public int ProductId { get; set; }
        [Column("Quantity")]
        public int Quantity { get; set; }
        [Column("RequestDate")]
        public DateTime RequestDate { get; set; }
        [Column("TransferDate")]
        public DateTime TransferDate { get; set; }
        [Column("Status")]
        public int Status { get; set; }
        [Column("MakerId")]
        public int MakerId { get; set; }
        [Column("ApproverId")]
        public int ApproverId { get; set; }
        [Column("Remarks")]
        public string Remarks { get; set; }
        //---- Navigation properties ----
        [ForeignKey("MakerId")]
        public virtual User Maker { get; set; }

        [ForeignKey("ApproverId")]
        public virtual User Approver { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("RequestPOSId")]
        public virtual POSe RequestPOSe { get; set; }

        [ForeignKey("SupplyingPOSId")]
        public virtual POSe SupplyingPOSe { get; set; }

    }
}
