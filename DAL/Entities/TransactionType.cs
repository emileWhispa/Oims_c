using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
[Table("TransactionTypes")]
public class TransactionType
{
[NotMapped]
public string TableName { get { return "TransactionTypes"; } }
[Column("TransactionId")]
public int Id{get;set;}
[Column("Description")]
[StringLength(50)]
public string Description{get;set;}
}
}
