using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.IO;
using orion.ims.BL;
using orion.ims.DAL;
using ExcelLibrary;

/// <summary>
/// Summary description for ReportEngine
/// </summary>
public class ReportEngine
{
    private string _dir;
    private Bl  bl;
    private Report report;
    private readonly string _rptPath = HttpContext.Current.Server.MapPath("~/Reports/rpt");

    public ReportEngine(string dir)
    {
        _dir = dir;
        bl = new Bl(Util.DbConnectionString());
    }

    public string ViewReport(ReportDetails rpt, ReportingType rptType)
    {
        //----- Clear old files
        try
        {
            var files = Directory.GetFiles(_rptPath);
            foreach (var f in files)
            {
                var fi = new FileInfo(f);
                if (DateTime.Now.Subtract(fi.CreationTime).TotalMinutes > 30)
                {
                    //====Delete
                    try
                    {
                        File.Delete(f);
                    }
                    catch { }
                }
            }
        }
        catch { }
        //-------- Process report
        report = bl.GetReport(rpt.ReportId);
        switch (rptType)
        {
            case ReportingType.PDF:
                return CreatePdf(rpt);
            case ReportingType.Excel:
                return CreateExcelReport(rpt);
        }
        return "";
    }
    private string CreatePdf(ReportDetails rpt)
    {
        var rptUrl = Guid.NewGuid().ToString().ToUpper().Replace("-", "") + ".pdf";
        var fullPath = Path.Combine(_rptPath, rptUrl);

        if (report == null)
            return "ERROR:Invalid report!";
        ReportDocument rptDoc = new ReportDocument();
        string rptName = Path.Combine(_dir, report.Filename);
        rptName = rptName + (rptName.ToLower().EndsWith(".rpt") ? "" : ".rpt");
        if (!File.Exists(rptName))
            return "ERROR:Report file does not exist!";
        //------Load report
        rptDoc.Load(rptName);
        //----put headers
        SetReportHeaders(ref rptDoc, report.Title);
        //----Get report data
        DataTable data = GetData(report.Datasource, rpt, ReportingType.PDF, (DataSourceType)report.Datasource_Type);
        if (data.Rows.Count == 0)
            return "ERROR:No data to display!";
        rptDoc.SetDataSource(data);
        //----- Export Data to disk
        rptDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, fullPath);

        return "~/rpt/" + rptUrl;
    }

    public string CreateExcelReport(ReportDetails rptD)
    {
        Random rdm = new Random();
        int rptId = rdm.Next(1000, 9999);
        var rptUrl = report.Title.ToString().ToUpper().Replace("-", "") + "_" + rptId.ToString() + ".xls";
        var rpt = Path.Combine(_rptPath, rptUrl);
        var dt = GetData(report.Datasource, rptD, ReportingType.Excel, (DataSourceType)report.Datasource_Type);
        if (dt.Rows.Count > 0)
        {
            var ds = new DataSet(report.Title) { Locale = Thread.CurrentThread.CurrentCulture };
            dt.Locale = Thread.CurrentThread.CurrentCulture;
            ds.Tables.Add(dt);
            DataSetHelper.CreateWorkbook(rpt, ds);
            return "~/rpt/" + rptUrl;
        }
        return "";
    }

    private DataTable GetData(string tableName, ReportDetails rpt, ReportingType rptType, DataSourceType dsType = DataSourceType.Table)
    {
        string connString = ToNormalConString(Util.DbConnectionString());
        SqlConnection sConn = new SqlConnection(connString);
        sConn.Open();
        DataTable dt = new DataTable();
        SqlDataAdapter sa;
        string sql = "";
        switch (dsType)
        {
            case DataSourceType.Table:
                sql = "SELECT * FROM " + tableName;
                if (rptType == ReportingType.Excel)
                    sql = "SELECT " + report.Excel_Cols + " FROM " + tableName;

                if (!string.IsNullOrEmpty(report.Filters.Trim()))
                    sql += " WHERE " + report.Filters;
                foreach (var filter in rpt.Filters)
                {
                    if (sql.Contains(" WHERE "))
                        sql += " AND " + filter.FilterName + filter.Operator + (filter.IsNumeric ? filter.FilterValue.ToString() : "'" + filter.FilterValue.ToString() + "'");
                    else
                        sql += " WHERE " + filter.FilterName + filter.Operator + (filter.IsNumeric ? filter.FilterValue.ToString() : "'" + filter.FilterValue.ToString() + "'");
                }
                sa = new SqlDataAdapter(sql, sConn);
                sa.Fill(dt);
                break;
            case DataSourceType.StoredProc:
                sql = string.Format("{0} {1},{2},'{3}','{4}'", tableName, rpt.FboId, rpt.EventId, rpt.Filters[0].FilterValue, rpt.Filters[1].FilterValue);

                SqlCommand cmd = new SqlCommand(sql, sConn);
                //cmd.CommandType = CommandType.StoredProcedure;
                sa = new SqlDataAdapter(cmd);
                sa.Fill(dt);

                break;
            case DataSourceType.Function:
                break;
        }

        return dt;
    }

    private void SetReportHeaders(ref ReportDocument rpt, string title)
    {
        if (rpt.Subreports.Count == 0)
            return;
        //----Create header
        DataTable header = new DataTable("HEADER");
        header.Columns.Add("Title");
        header.Rows.Add(title);

        //----- Assign data to the sub report
        if (rpt.Subreports.Count > 0)
            rpt.Subreports[0].SetDataSource(header);

    }
    private string ToNormalConString(string connString)
    {
        string newConn = connString;
        if (connString.Trim().ToLower().StartsWith("metadata"))
        {
            newConn = connString.Substring(connString.ToLower().IndexOf("data source"));
            if (newConn.EndsWith("\""))
                newConn = newConn.Substring(0, newConn.Length - 1);
        }
        return newConn;
    }
}

public enum ReportingType { PDF = 0, Excel = 1, Html = 2 }
public enum DataSourceType { Table = 0, StoredProc = 1, Function = 2 }

public class ReportFilters
{
    public string FilterName { get; set; }
    public bool IsNumeric { get; set; }
    public string Operator { get; set; }
    public object FilterValue { get; set; }
}

public class ReportDetails
{
    public int ReportId { get; set; }
    public int FboId { get; set; }
    public int EventId { get; set; }
    public int RptCategory { get; set; }
    public List<ReportFilters> Filters { get; set; }
    public ReportDetails()
    {
        this.Filters = new List<ReportFilters>();
    }
}