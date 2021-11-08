using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
[Table("BankTransactions")]
public class BankTransaction
{
[NotMapped]
public string TableName { get { return "BankTransactions"; } }
[Column("TransactionId")]
public int Id{get;set;}
[Column("AccountId")]
[Required()]
public int AccountId { get; set; }
[Column("TransactionTypeId")]
[Required()]
public int TransactionTypeId{get;set;}
[Column("Amount")]
public decimal Amount{get;set;}
[Column("TransactionDate")]
[Display(Name = "Transaction Date")]
[DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
public DateTime TransactionDate{get;set;}
[Column("PostingId")]
[Required()]
public int PostingId{get;set;}
[Column("DocRef")]
public string DocRef { get; set; }
[Column("AdditionalInfo")]
[StringLength(100)]
public string AdditionalInfo{get;set;}
[Column("Balance")]
public decimal Balance { get; set; }
[ForeignKey("AccountId")]
public virtual Account Account { get; set; }
[ForeignKey("TransactionTypeId")]
public virtual TransactionType TransactionType { get; set; }
[ForeignKey("PostingId")]
public virtual PostingType PostingType { get; set; }
}
}
