using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SessionData
/// </summary>
public class SessionData
{
    public static string DBConString { get; set; }
    //public static string UserName { get; set; }
    //public static string UserFullName { get; set; }
    //public static int UserId { get; set; }
    //public static int UserGroupId { get; set; }
    //public static int UserBranchId { get; set; }
    //public static string UserBranch { get; set; }
    //public static int POSId { get; set; }
    //public static string POSName { get; set; }
    public const string secKey = "B@r20!4";
    //public static int UserbankId { get; set; }
    public static DateTime sessionduration { get; set; }
    public static Boolean Logout { get; set; }
    public static void Clear()
    {
        //UserName = null;
        //UserFullName = null;
        //UserId = -1;
        //UserBranchId = -1;
        //UserBranch = null;
        //POSId = -1;
        //POSName = null;
        //UserGroupId = -1;
    }
}