#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;



[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="OIMS_RF")]
public partial class OIMSDataContext : System.Data.Linq.DataContext
{
	
	private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
	
  #region Extensibility Method Definitions
  partial void OnCreated();
  #endregion
	
	public OIMSDataContext() : 
			base(global::System.Configuration.ConfigurationManager.ConnectionStrings["OIMS_RFConnectionString"].ConnectionString, mappingSource)
	{
		OnCreated();
	}
	
	public OIMSDataContext(string connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public OIMSDataContext(System.Data.IDbConnection connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public OIMSDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public OIMSDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.usp_InsertSales")]
	public int usp_InsertSales([global::System.Data.Linq.Mapping.ParameterAttribute(Name="QuoteDate", DbType="DateTime")] System.Nullable<System.DateTime> quoteDate, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Remarks", DbType="VarChar(200)")] string remarks, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="CustomerId", DbType="Int")] System.Nullable<int> customerId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Deposit", DbType="Decimal(18,0)")] System.Nullable<decimal> deposit, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="SalesRef", DbType="VarChar(14)")] string salesRef, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="MakerId", DbType="Int")] System.Nullable<int> makerId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="DiscountCustomerId", DbType="Int")] System.Nullable<int> discountCustomerId)
	{
		IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), quoteDate, remarks, customerId, deposit, salesRef, makerId, discountCustomerId);
		return ((int)(result.ReturnValue));
	}
}
#pragma warning restore 1591
