using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
[Table("PostingType")]
public class PostingType
{
[NotMapped]
public string TableName { get { return "PostingType"; } }
[Column("PostingId")]
public int Id{get;set;}
[Column("Postingmode")]
[Required()]
public int Postingmode{get;set;}
[Column("Description")]
[StringLength(50)]
public string Description{get;set;}
}
}
