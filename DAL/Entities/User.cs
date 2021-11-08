using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.ComponentModel;
using System.Collections;

namespace orion.ims.DAL
{
    [Table("Users")]
    public class User
    {
        [NotMapped]
        public string TableName { get { return "Users"; } }

        [Column("UserId")]
        public int Id { get; set; }

        [Column("UserName")]
        [Required()]
        [StringLength(20)]
        [Display(Name="User Name")]
        public string UserName { get; set; }

        [Column("UserGroupId")]
        [Required()]
        [Display(Name = "User Group")]
        public int UserGroupId { get; set; }

        [Column("FullNames")]
        [Required()]
        [StringLength(100)]
        [Display(Name = "Full Names")]
        public string FullNames { get; set; }

        [Column("Password")]
        [Required()]
        [StringLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[NotMapped]
        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm Password")]
        ////[System.Web.Mvc.Compare("Password", ErrorMessage = "Password mis-match!")]
        //public string ConfirmPassword { get; set; }

        [Column("Salt")]
        [StringLength(100)]
        public string Salt { get; set; }

        [Column("StatusId")]
        [Required()]
        public int StatusId { get; set; }

        [Column("Phone")]
        [StringLength(20)]
        public string Phone { get; set; }

        [Column("Email")]
        [StringLength(50)]
        public string Email { get; set; }

        [Column("ChangePassword")]
        [Required()]
        public bool ChangePassword { get; set; }

        [Column("PassChangeDate")]
        [Required()]
        public DateTime PassChangeDate { get; set; }

        [Column("LastLogin")]
        [Required()]
        public DateTime LastLogin { get; set; }
        [Column("InvalidAttempts")]
        public int InvalidAttempts { get; set; }

        [Column("POSID")]
        public int POSID { get; set; }

        [ForeignKey("UserGroupId")]
        public virtual UserGroup UserGroup { get; set; }

        [ForeignKey("POSID")]
        public virtual POSe Pos { get; set; }

    }

    [AttributeUsageAttribute(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class CompareValuesAttribute : ValidationAttribute
    {
        /// <summary>
        /// The other property to compare to
        /// </summary>
        public string OtherProperty { get; set; }

        public CompareValues Criteria { get; set; }

        /// <summary>
        /// Creates the attribute
        /// </summary>
        /// <param name="otherProperty">The other property to compare to</param>
        public CompareValuesAttribute(string otherProperty, CompareValues criteria)
        {
            if (otherProperty == null)
                throw new ArgumentNullException("otherProperty");

            OtherProperty = otherProperty;
            Criteria = criteria;
        }

        /// <summary>
        /// Determines whether the specified value of the object is valid.  For this to be the case, the objects must be of the same type
        /// and satisfy the comparison criteria. Null values will return false in all cases except when both
        /// objects are null.  The objects will need to implement IComparable for the GreaterThan,LessThan,GreatThanOrEqualTo and LessThanOrEqualTo instances
        /// </summary>
        /// <param name="value">The value of the object to validate</param>
        /// <param name="validationContext">The validation context</param>
        /// <returns>A validation result if the object is invalid, null if the object is valid</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // the the other property
            var property = validationContext.ObjectType.GetProperty(OtherProperty);

            // check it is not null
            if (property == null)
                return new ValidationResult(String.Format("Unknown property: {0}.", OtherProperty));

            // check types
            if (validationContext.ObjectType.GetProperty(validationContext.MemberName).PropertyType != property.PropertyType)
                return new ValidationResult(String.Format("The types of {0} and {1} must be the same.", validationContext.DisplayName, OtherProperty));

            // get the other value
            var other = property.GetValue(validationContext.ObjectInstance, null);

            // equals to comparison,
            if (Criteria == CompareValues.EqualTo)
            {
                if (Object.Equals(value, other))
                    return null;
            }
            else if (Criteria == CompareValues.NotEqualTo)
            {
                if (!Object.Equals(value, other))
                    return null;
            }
            else
            {
                // check that both objects are IComparables
                if (!(value is IComparable) || !(other is IComparable))
                    return new ValidationResult(String.Format("{0} and {1} must both implement IComparable", validationContext.DisplayName, OtherProperty));

                // compare the objects
                var result = Comparer.Default.Compare(value, other);

                switch (Criteria)
                {
                    case CompareValues.GreaterThan:
                        if (result > 0)
                            return null;
                        break;
                    case CompareValues.LessThan:
                        if (result < 0)
                            return null;
                        break;
                    case CompareValues.GreatThanOrEqualTo:
                        if (result >= 0)
                            return null;
                        break;
                    case CompareValues.LessThanOrEqualTo:
                        if (result <= 0)
                            return null;
                        break;
                }
            }

            // got this far must mean the items don't meet the comparison criteria
            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        /// <summary>
        /// Applies formatting to an error message.
        /// </summary>
        /// <param name="name">The name to include in the error message</param>
        /// <returns></returns>
        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, base.ErrorMessageString, name, OtherProperty, Criteria.Description());
        }

        /// <summary>
        /// retrieve the object to compare to
        /// </summary>
        /// <returns></returns>
        object GetOther(ValidationContext context)
        {
            return null;
        }
    }

    public static class EnumExtensions
    {
        /// <summary>
        /// Get the description attribute for the enum
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string Description(this Enum e)
        {
            var da = (DescriptionAttribute[])(e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false));

            return da.Length > 0 ? da[0].Description : e.ToString();
        }

    }

    /// <summary>
    /// Indicates a comparison criteria used by the CompareValues attribute
    /// </summary>
    public enum CompareValues
    {
        [Description("=")]
        EqualTo,
        [Description("!=")]
        NotEqualTo,
        [Description(">")]
        GreaterThan,
        [Description("<")]
        LessThan,
        [Description(">=")]
        GreatThanOrEqualTo,
        [Description("<=")]
        LessThanOrEqualTo
    }
}
