using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
[Table("OrderDispatch")]
public class OrderDispatch
{
[NotMapped]
public string TableName { get { return "OrderDispatch"; } }
[Column("DispatchId")]
public int Id{get;set;}
[Column("OrderId")]
public int OrderId{get;set;}
[Column("ProductId")]
public int ProductId{get;set;}
[Column("QuantityDispatched")]
[Required()]
public decimal QuantityDispatched{get;set;}
[Column("OrderQty")]
[Required()]
public decimal OrderQty { get; set; }
[Column("balance")]
[Required()]
public decimal balance { get; set; }
[Column("DispatchDate")]
[Required()]
public DateTime DispatchDate{get;set;}
[ForeignKey("OrderId")]
public virtual POSOrder Order { get; set; }
[ForeignKey("ProductId")]
public virtual Product Product { get; set; }
}
}
