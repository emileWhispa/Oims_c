using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  orion.ims.DAL
{
[Table("StockItems")]
public class StockItem
{
[NotMapped]
public string TableName { get { return "StockItems"; } }
[Column("StockId")]
public int Id{get;set;}
[Column("ProductId")]
[Required()]
public int ProductId{get;set;}
[Column("posid")]
public int posid { get; set; }
[Column("Quantity")]
[Required()]
public decimal Quantity{get;set;}
[ForeignKey("ProductId")]
public virtual Product Product { get; set; }
[ForeignKey("posid")]
public virtual POSe Pos { get; set; }
}
}
