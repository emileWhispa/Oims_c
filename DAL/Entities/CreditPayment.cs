using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
[Table("CreditPayments")]
public class CreditPayment
{
[NotMapped]
public string TableName { get { return "CreditPayments"; } }
[Column("CreditId")]
public int Id{get;set;}
[Column("Amount")]
[Required()]
public decimal Amount{get;set;}
[Column("PaymentDate")]
[Display(Name = "Payment Date")]
[UIHint("DatePicker")]
[DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
[Required()]
public DateTime PaymentDate{get;set;}
[Column("Addinfo")]
[StringLength(150)]
public string Addinfo{get;set;}
[Column("Balance")]
public Decimal Balance { get; set; }
[Column("Customerid")]
public int CustomerId { get; set; }

[ForeignKey("CustomerId")]
public virtual Customer Customer { get; set; }
}
}
