using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
[Table("Densities")]
public class Density
{
[NotMapped]
public string TableName { get { return "Densities"; } }
[Column("DensityID")]
public int Id{get;set;}
[Column("DensityName")]
[Required()]
[StringLength(50)]
public string DensityName{get;set;}
}
}
