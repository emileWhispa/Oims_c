using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
[Table("SalesPayments")]
public class SalesPayment
{
[NotMapped]
public string TableName { get { return "SalesPayments"; } }
[Column("SalesPaymentId")]
public int Id{get;set;}
[Column("SalesId")]
[Required()]
public int SalesId{get;set;}
[Column("PaymentDate")]
[Required()]
public DateTime PaymentDate{get;set;}
[Column("PaymentModeId")]
[Required()]
public int PaymentModeId{get;set;}
[Column("Amount")]
[Required()]
public decimal Amount{get;set;}
[Column("DocumentNo")]
public string DocumentNo { get; set; }
[Column("UserId")]
[Required()]
public int UserId{get;set;}
[Column("PaymentType")]
public string PaymentType { get; set; }
}
}
