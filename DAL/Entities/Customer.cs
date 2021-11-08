using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orion.ims.DAL
{
    [Table("Customers")]
    public class Customer
    {
        [NotMapped]
        public string TableName { get { return "Customers"; } }

        [Column("CustomerId")]
        public int Id { get; set; }

        [Column("CustomerName")]
        [Display(Name = "Customer Name")]
        [Required()]
        [StringLength(50)]
        public string CustomerName { get; set; }

        [Column("CreditLimit")]
        public decimal? CreditLimit { get; set; }

        [Column("Balance")]
        public decimal? Balance { get; set; }
        [Column("Address")]
        [StringLength(50)]
        public string Address { get; set; }
        [Column("PhoneNo")]
        [StringLength(20)]
        public string PhoneNo { get; set; }

        [Column("AddInfo")]

        [StringLength(130)]
        public string AddInfo { get; set; }

        [Column("Distributor")]
        public bool Distributor { get; set; }
    }
}
