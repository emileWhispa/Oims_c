using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
[Table("POSes")]
public class POSe
{
[NotMapped]
public string TableName { get { return "POSes"; } }
[Column("POSID")]
public int Id{get;set;}
[Column("POSName")]
[Required()]
[StringLength(50)]
public string POSName{get;set;}
public bool Mainstore { get; set; }
[Column("POSAddress")]
[StringLength(100)]
public string POSAddress{get;set;}
[Column("POSCode")]
[StringLength(5)]
public string POSCode { get; set; }
}
}
