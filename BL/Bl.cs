using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using orion.ims.DAL;
using System.Data.Entity.Core;
using System.IO;
using Bmat.Tools;
//using bmat.Reporting;
using System.Data;
using System.Data.SqlClient;

namespace orion.ims.BL
{
    public class Bl : IBl
    {
        IMSContext db;
        private string connString;
        public Bl(string dbConnString)
        {
            db = new IMSContext(dbConnString);
            connString = dbConnString;
        }

        #region Generic Methods
        public IQueryable<T> Fetch<T>() where T : class
        {
            return db.ObjectContext().CreateObjectSet<T>().AsQueryable();
        }

        public IQueryable<T> Search<T>(Func<T, bool> predicate) where T : class
        {
            return db.ObjectContext().CreateObjectSet<T>().Where(predicate).AsQueryable();
        }
        public void Execsql(string Sql)
        {
            db.Exec(Sql);
        }

        public T Save<T>(T entity, string eName) where T : class
        {
            EntityKey key = db.ObjectContext().CreateEntityKey(eName, entity);
            object o;
            db.ObjectContext().TryGetObjectByKey(key, out o);
            if (o == null)
                db.ObjectContext().CreateObjectSet<T>().AddObject(entity);
            else
                db.ObjectContext().ApplyCurrentValues(eName, entity);
            db.SaveChanges();
            return entity;
        }

        public void Save<T>(List<T> entities) where T : class
        {
            foreach (T entity in entities)
            {
                EntityKey key = db.ObjectContext().CreateEntityKey(typeof(T).Name, entity);
                object o;
                db.ObjectContext().TryGetObjectByKey(key, out o);
                if (o == null)
                    db.ObjectContext().CreateObjectSet<T>().AddObject(entity);
                else
                    db.ObjectContext().ApplyCurrentValues(typeof(T).Name, entity);
            }
            db.SaveChanges();
        }

        public void Delete<T>(Func<T, bool> predicate) where T : class
        {
            foreach (T entity in Search<T>(predicate))
                db.ObjectContext().CreateObjectSet<T>().DeleteObject(entity);
            db.SaveChanges();
        }

        #endregion

        #region Banks
        public Bank getBank(int id)
        {
            return db.Banks.Where(x => x.Id == id).FirstOrDefault();
        }

        public IQueryable<Bank> getBanks()
        {
            return db.Banks.AsQueryable();
        }

        public Bank saveBank(Bank bank)
        {
            return Save<Bank>(bank, "Banks");
        }

        public void deleteBank(int id)
        {
            Bank b = getBank(id);
            if (b != null)
                db.DeleteObject(b);
        }
        #endregion

        #region Branches
        public Branch getBranch(int id)
        {
            return db.Branches.Where(x => x.Id == id).FirstOrDefault();
        }

        public IQueryable<Branch> getBankBranches(int bankId)
        {
            return db.Branches.Where(x => x.Bank.Id == bankId).AsQueryable();
        }

        public IQueryable<Branch> getBranches()
        {
            return db.GetAll<Branch>();
        }

        public Branch saveBranch(Branch branch)
        {
            return Save<Branch>(branch, "Branches");
        }
        public void deleteBranch(int id)
        {
            Branch b = getBranch(id);
            if (b != null)
                db.DeleteObject(b);
        }
        #endregion

        #region Currencies
        public Currency getCurrency(int id)
        {
            return db.Currencies.Where(x => x.Id == id).FirstOrDefault();
        }

        public Currency getCurrency(string code)
        {
            return db.Currencies.Where(x => x.CurrencyCode == code).FirstOrDefault();
        }

        public IQueryable<Currency> getCurrencies()
        {
            return db.Currencies.AsQueryable();
        }

        public Currency saveCurrency(Currency currency)
        {
            return Save<Currency>(currency, "Currencies");
        }

        public void deleteCurrency(int id)
        {
            Currency b = getCurrency(id);
            if (b != null)
                db.DeleteObject(b);
        }
        #endregion

        #region Users
        public User getUser(int id)
        {
            return db.Users.Where(x => x.Id == id).FirstOrDefault();
        }

        public User getUser(string uname)
        {
            return db.Users.Where(x => x.UserName == uname).FirstOrDefault();
        }

        public IQueryable<User> getUsers()
        {
            return db.Users.AsQueryable();
        }

        public User processUserPass(User u, string uSalt = "")
        {
            BTSecurity sec = new BTSecurity();
            //----- Process password
            string salt = string.IsNullOrEmpty(uSalt) ? sec.GenerateSalt(u.Password.Length) : uSalt;
            string hash = sec.HashPassword(u.Password, salt);

            u.Salt = salt;
            u.Password = hash;
            // u.ConfirmPassword = hash;
            return u;
        }

        public User saveUser(User u)
        {

            return Save<User>(u, "Users");
        }

        public void deleteUser(int id)
        {
            User b = getUser(id);
            if (b != null)
                db.DeleteObject(b);
        }

        public bool ValidateUser(int id, string password, out string msg, out UserLoginStatus stat)
        {
            stat = UserLoginStatus.Ok;
            msg = "";
            var user = getUser(id);
            if (user == null)
                msg = "Uknown User!";
            else
            {
                //---- Validate Password
                BTSecurity sec = new BTSecurity();
                if (ValidateUserPass(user.Id, password))
                {
                    //----- Check user status
                    if (user.ChangePassword)
                        stat = UserLoginStatus.ChangePassword;

                    return true;
                }
                else
                    msg = "Incorrect Username and/or Password!";
            }
            return false;
        }

        public bool ValidateUserPass(int id, string password)
        {
            var user = getUser(id);
            if (user != null)
            {
                BTSecurity sec = new BTSecurity();
                if (sec.ValidatePassword(password, user.Password, user.Salt))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsLoginOk(string userName, string password, string sKey, out string resp, out User user)
        {
            user = null;
            //EncryptDecrypt ed = new EncryptDecrypt(sKey);
            resp = "";
            //user = getUser(userName.ToUpper());

            //if (user == null)
            //{
            //    resp = "Invalid user name!";
            //}
            //else
            //{
            //    if (ed.Decrypt(user.Password).Equals(password))
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        resp = "Invalid user password!";
            //        return false;
            //    }
            //}
            return false;
        }
        //public IQueryable<Vw_user> GetUserlist()
        //{
        //    return db.Vw_users.AsQueryable();
        //}
        #endregion

        #region UserGroups
        public UserGroup getUserGroup(int id)
        {
            return db.UserGroups.Where(x => x.Id == id).FirstOrDefault();
        }

        public IQueryable<UserGroup> getUserGroups()
        {
            return db.UserGroups.AsQueryable();
        }

        public UserGroup saveUserGroup(UserGroup userGroup)
        {
            return Save<UserGroup>(userGroup, "UserGroups");
        }

        public void deleteUserGroup(int id)
        {
            UserGroup b = getUserGroup(id);
            if (b != null)
                db.DeleteObject(b);
        }
        #endregion

        #region Settings
        public SysParam getSettingParam(int id)
        {
            return db.SysParams.Where(x => x.Id == id).FirstOrDefault();
        }

        public SysParam getSettingParam(string pName)
        {
            return db.SysParams.Where(x => x.ParamName == pName.Trim()).FirstOrDefault();
        }

        public IQueryable<SysParam> getSettingParams()
        {
            return db.SysParams.AsQueryable();
        }

        public SysParam saveSettingParam(SysParam s)
        {
            return Save<SysParam>(s, "SysParams");
        }
        public void deleteparam(int id)
        {
            SysParam b = getSettingParam(id);
            if (b != null)
                db.DeleteObject(b);
        }
        #endregion

        #region PassHist
        public Pass_Hist getpass(int id)
        {
            return db.Pass_Hist.Where(x => x.Id == id).FirstOrDefault();
        }

        public IQueryable<Pass_Hist> getpassbyUser(int id)
        {
            return db.Pass_Hist.Where(x => x.UserId == id).Take(5).OrderBy(x => x.PassDate).AsQueryable();
        }

        public IQueryable<Pass_Hist> getPassHist()
        {
            return db.Pass_Hist.AsQueryable();
        }

        public Pass_Hist savePassHist(Pass_Hist passhist)
        {
            return Save<Pass_Hist>(passhist, passhist.TableName);
        }

        public void deletepassHist(int id)
        {
            Pass_Hist b = getpass(id);
            if (b != null)
                db.DeleteObject(b);
        }
        #endregion

        #region Menus
        public IQueryable<Menu> GetMenus()
        {
            return db.GetAll<Menu>().Where(x=>x.Menu_Level==0);
        }
        #endregion

        #region Rights
        public IQueryable<Right> GetRights()
        {
            return db.GetAll<Right>().AsQueryable();
        }

        public bool CheckMenuRights(string menuUrl)
        {
            var right = (from m in db.Menus join r in db.Rights on m.Id equals r.MenuId where m.Url.Contains(menuUrl) && r.AuthStatus == 1 select r).FirstOrDefault();
            if (right == null)
                return false;
            else
                return right.AllowAccess;
        }

        public IQueryable<vw_ProfileMenus> GetProfileMenus(int groupId)
        {
            return (from m in db.Menus.Where(x => x.Active)
                    join r in db.Rights.Where(x => x.UserGroupId == groupId && x.AuthStatus == 1) on m.Id equals r.MenuId into temp
                    from t in temp.DefaultIfEmpty()
                    select new vw_ProfileMenus
                    {
                        Active = m.Active,
                        AllowAccess = t == null ? false : t.AllowAccess,
                        Id = m.Id,
                        MenuLevel = m.Menu_Level,
                        MenuName = m.Menu_Name,
                        ParentMenuId = m.Parent_Id,
                        UserGroupId = t == null ? 0 : t.UserGroupId,
                        Url = t == null ? "~/AccessDenied.aspx" : t.AllowAccess ? m.Url : "~/AccessDenied.aspx"
                    });
        }

        public void SaveMenuRights(vw_ProfileMenus menu)
        {
            var mnu = db.Rights.Where(x => x.MenuId == menu.Id && x.UserGroupId == menu.UserGroupId).FirstOrDefault();
            if (mnu == null)
            {
                //---- Add new
                mnu = new Right
                {
                    AllowAccess = menu.AllowAccess,
                    AuthStatus = 1,
                    MenuId = menu.Id,
                    UserGroupId = menu.UserGroupId
                };
            }
            else
            {
                //---- Update
                mnu.AuthStatus = 1;
                mnu.AllowAccess = menu.AllowAccess;
            }
            db.SaveEntity<Right>(mnu, mnu.TableName);
        }

        public IQueryable<GroupRights> GetGroupRights(int groupId, int moduleId)
        {
            return (from m in db.Menus.Where(x => x.Parent_Id == moduleId && x.Parent_Id != 0 && x.Active)
                    join r in db.Rights.Where(x => x.UserGroupId == groupId && x.AuthStatus == 1) on m.Id equals r.MenuId into temp
                    from t in temp.DefaultIfEmpty()
                    select new GroupRights
                    {
                        GroupName = "",
                        MenuId = m.Id,
                        AllowAccess = t == null ? false : t.AllowAccess,
                        MenuName = m.Menu_Name,
                        Id = m.Id,
                        UserGroupId = t == null ? 0 : t.UserGroupId,
                    });
        }

        public Right SaveRights(Right e)
        {
            return db.SaveEntity<Right>(e, e.TableName);
        }

        #endregion

        #region Makerchecker
        public Maintenance_Auth getMaintenaceAuth(int id)
        {
            return db.Maintenance_Auth.Where(x => x.Id == id).FirstOrDefault();
        }

        public Maintenance_Auth getMaintenaceAuth(string name)
        {
            return db.Maintenance_Auth.Where(x => x.ActionName == name).FirstOrDefault();
        }

        public IQueryable<Maintenance_Auth> getMaintenaceAuths()
        {
            return db.Maintenance_Auth.AsQueryable();
        }

        public Maintenance_Auth saveMaintenaceAuth(Maintenance_Auth c)
        {
            return Save<Maintenance_Auth>(c, c.TableName);
        }
        #endregion

        #region Accounts
        public Account getAccount(int id)
        {
            return db.GetById<Account>(x => x.Id == id);
        }

        public Account getAccount(string accNo)
        {
            return db.GetById<Account>(x => x.AccountNo == accNo);
        }

        public IQueryable<Account> getAccounts()
        {
            return db.GetAll<Account>();
        }

        public Account saveAccount(Account a)
        {
            return db.SaveEntity<Account>(a, a.TableName);
        }

        public void deleteAccount(int id)
        {
        }

        public IQueryable<Account> getBankAccounts(int bankId)
        {
            return db.Accounts.Where(x => x.Bank.Id == bankId).AsQueryable();
        }
        #endregion

        #region Reports
        public IQueryable<Report> GetReports()
        {
            return db.GetAll<Report>();
        }

        public Report GetReport(int id)
        {
            return db.GetById<Report>(x => x.Id == id);
        }

        //public string PrintReport(ReportView repView, string workingDir, string userName, string logoFile)
        //{
        //    string sql = "";
        //    //---- Get report
        //    var report = db.GetById<Report>(x => x.Id == repView.ReportId);
        //    if (report != null)
        //    {
        //        //--- Get Reports dir
        //        string repDir = @"c:\";
        //        var param = getSettingParam("REPORTS_PATH");
        //        if (param != null)
        //            repDir = param.ParamValue;
        //        //----Client Name
        //        string clientName = "";
        //        param = getSettingParam("CLIENT_NAME");
        //        if (param != null)
        //            clientName = param.ParamValue;

        //        //---- Create Report Header
        //        var h = GetReportHeader(report.Title, repView.DateFrom.ToString("dd-MMM-yyyy"), repView.DateTo.ToString("dd-MMM-yyyy"), userName, clientName);
        //        //--- Add Header Logo
        //        if (File.Exists(logoFile))
        //        {
        //            var col = new ReportHeaderDefination()
        //            {
        //                Data = File.ReadAllBytes(logoFile),
        //                DataType = ColDataType.Blob,
        //                Name = "Logo"
        //            };
        //            h.Add(col);
        //        }
        //        //---Create header
        //        var header = new ReportHeader
        //        {
        //            HeaderData = h
        //        };

        //        //--- Create settings
        //        var setting = new ReportSetting()
        //        {
        //            DestReportsDir = workingDir,
        //            HeaderReportFile = "Header.rpt",
        //            ReportFileName = report.Filename,
        //            ReportHeader = header,
        //            ReportsDir = repDir,
        //            ReportType = (ReportType)repView.ReportTypeId
        //        };

        //        //--- Create report engine
        //        ReportEngine rEngine = new ReportEngine(setting);

        //        //--- Manage Archive reports
        //        if (repView.SourceId == 1)
        //        {
        //            report.Datasource += "_Arch";
        //        }
        //        //---- Get Report data
        //        sql = ReportEngine.CreateSql(report.Datasource, report.Excel_Cols, report.Filters, report.ShowAll,
        //            repView.SourceId == 1, repView.DateFrom, repView.DateTo);
        //        if (repView.ReportId == 1)
        //        { sql = sql + " where orderId=" + repView.RecordId; }
        //        if (repView.ReportId == 5)
        //        { sql = sql + " where SalesId=" + repView.RecordId; }
        //        //if (repView.ReportId == 6)
        //        //{ sql = sql + " where DeliveryId=" + repView.RecordId; }
        //        //if (repView.ReportId == 8)
        //        //{ sql = sql + " where SaleDeliveryId=" + repView.RecordId; }
        //        //---- Get Data
        //        var data = getReportData(repView, sql, report.Date_Col, report.ShowAll);

        //        //---- Creare report
        //        var file = rEngine.CreateReport(data);
        //        return file;
        //    }
        //    return "";
        //}

        //private List<ReportHeaderDefination> GetReportHeader(string title, string dFrom, string dTo, string userName, string client)
        //{
        //    var header = new List<ReportHeaderDefination>();
        //    //---- Title
        //    var col = new ReportHeaderDefination()
        //    {
        //        Data = title,
        //        DataType = ColDataType.String,
        //        Name = "Title"
        //    };
        //    header.Add(col);
        //    //---- Title
        //    col = new ReportHeaderDefination()
        //    {
        //        Data = client,
        //        DataType = ColDataType.String,
        //        Name = "ClientName"
        //    };
        //    header.Add(col);
        //    //---- User Name
        //    col = new ReportHeaderDefination()
        //    {
        //        Data = userName,
        //        DataType = ColDataType.String,
        //        Name = "UserName"
        //    };
        //    header.Add(col);
        //    //---- Currency
        //    col = new ReportHeaderDefination()
        //    {
        //        Data = "KWACHA",
        //        DataType = ColDataType.String,
        //        Name = "Currency"
        //    };
        //    header.Add(col);
        //    //---- Date From
        //    col = new ReportHeaderDefination()
        //    {
        //        Data = dFrom,
        //        DataType = ColDataType.String,
        //        Name = "DateFrom"
        //    };
        //    header.Add(col);
        //    //---- Date To
        //    col = new ReportHeaderDefination()
        //    {
        //        Data = dTo,
        //        DataType = ColDataType.String,
        //        Name = "DateTo"
        //    };
        //    header.Add(col);

        //    return header;
        //}

        private DataTable getReportData(ReportView repView, string sql, string dateCol, bool showAll)
        {
            try
            {
                if (!showAll)
                {
                    if (repView.SourceId != 0)
                    {
                        if (!sql.ToUpper().Contains(" WHERE "))
                            sql += " WHERE ";
                        else
                            sql += " AND ";

                        //---- Date Filters
                        sql += String.Format("{0}>={1:dd MMM yyyy} AND {0}<={2:dd MMM yyyy}", dateCol, repView.DateFrom, repView.DateTo);

                    }
                }
                //--- Query Data
                SqlConnection conn = new SqlConnection(connString);
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
            }
            return null;
        }
        #endregion

        #region EOD
        public string runEOD(int userId)
        {
            string resp = "";
            string sql = "Exec sp_EOD " + userId.ToString();
            List<string> results = db.Exec<string>(sql).ToList();
            if (results.Count == 0)
            {
                resp = "OK";
            }
            else
            {
                resp = "ERROR!<br/>";
                foreach (var r in results)
                    resp += r + "<br/>";
            }
            //----- Move Files 
            FileArchiver archiver = new FileArchiver();
            //------ Client Export Files
            var param = getSettingParam("EXPORT_PATH");
            if (param != null)
                archiver.Archive(param.ParamValue, Path.Combine(param.ParamValue, "Archive"));

            //------ Bulk Credits Files
            param = getSettingParam("BULK_CREDS_PATHS");
            if (param != null)
                archiver.Archive(param.ParamValue, Path.Combine(param.ParamValue, "Archive"));

            //------ ACH Files
            //param = getSettingParam("OUT_ACH_PATH");
            //if (param != null)
            //    archiver.Archive(param.ParamValue, Path.Combine(param.ParamValue, "Archive"));

            //------ Upload Files
            param = getSettingParam("UPLOAD_PATH");
            if (param != null)
                archiver.Archive(param.ParamValue, Path.Combine(param.ParamValue, "Archive"));

            return resp;
        }
        #endregion

        #region Products
        public Product getProduct(int id)
        {
            return db.Products.Where(x => x.Id == id).FirstOrDefault();
        }

        public IQueryable<Product> getProducts()
        {
            return db.Products.AsQueryable().Where(x=>x.Active==true);
        }

        public Product saveProduct(Product Product)
        {
            return Save<Product>(Product, "Products");
        }

        public void deleteProduct(int id)
        {
            Product b = getProduct(id);
            if (b != null)
                db.DeleteObject(b);

        }
        #endregion

        #region ItemDelivery
        public ItemDelivery getItemDelivery(int id)
        {
            return db.ItemDelivery.Where(x => x.Id == id).FirstOrDefault();
        }

        public IQueryable<ItemDelivery> getItemDeliveries()
        {
            return db.ItemDelivery.AsQueryable();
        }

        public ItemDelivery saveItemDelivery(ItemDelivery deliv)
        {
            return Save<ItemDelivery>(deliv, "Products");
        }

        public void deleteItemDelivery(int id)
        {
            ItemDelivery b = getItemDelivery(id);
            if (b != null)
                db.DeleteObject(b);

        }
        #endregion

        #region Products Categories
        
        public ProductCategory getProductCategory(int id)
        {
            return db.ProductCategories.Where(x => x.Id == id).FirstOrDefault();
        }

        public IQueryable<ProductCategory> getProductCategories()
        {
            return db.ProductCategories.AsQueryable();
        }

        public ProductCategory saveProductCategory(ProductCategory obj)
        {
            return Save<ProductCategory>(obj, obj.TableName);
        }

        public void deleteProductCategory(int id)
        {
            ProductCategory b = getProductCategory(id);
            if (b != null)
                db.DeleteObject(b);

        }
        #endregion
    
        #region Customers
        public Customer getCustomer(int id)
        {
            return db.Customers.Where(x => x.Id == id).FirstOrDefault();
        }

        public IQueryable<Customer> getCustomers()
        {
            return db.Customers.AsQueryable();
        }

        public Customer saveCustomer(Customer customer)
        {
            return Save<Customer>(customer, customer.TableName);
        }

        public void deleteCustomer(int id)
        {
            Customer b = getCustomer(id);
            if (b != null)
                db.DeleteObject(b);
        }
        #endregion

        //#region Orders
        //public Order getOrder(int id)
        //{
        //    return db.GetById<Order>(x => x.Id == id);
        //}

        //public Order getOrder(string orderno)
        //{
        //    return db.GetById<Order>(x => x.OrderNo == orderno);
        //}
        //public IQueryable<Order> getOrders()
        //{
        //    return db.GetAll<Order>();
        //}

        //public Order saveOrder(Order order)
        //{
        //    return db.SaveEntity<Order>(order, order.TableName);
        //}

        //public void deleteOrder(int id)
        //{
        //    POSOrder b = getPOSOrder(id);
        //    if (b != null)
        //        db.DeleteObject(b);
        //}
        //#endregion

        #region POSOrders
        public POSOrder getPOSOrder(int id)
        {
            return db.GetById<POSOrder>(x => x.Id == id);
        }
        public POSOrder getPOSOrder(string order)
        {
            return db.GetById<POSOrder>(x => x.OrderNo == order);
        }
        public IQueryable<POSOrder> getPOSOrders()
        {
            return db.GetAll<POSOrder>();
        }

        public POSOrder savePOSOrder(POSOrder order)
        {
            return db.SaveEntity<POSOrder>(order, order.TableName);
        }

        public void deletePOSOrder(int id)
        {
            POSOrder b = getPOSOrder(id);
            if (b != null)
                db.DeleteObject(b);
        }
        #endregion

        #region POSItemTransfer
        public POSItemTransfer getPOSItemTransfer(int id)
        {
            return db.GetById<POSItemTransfer>(x => x.Id == id);
        }
       
        public IQueryable<POSItemTransfer> getPOSItemTransfers()
        {
            return db.GetAll<POSItemTransfer>();
        }

        public POSItemTransfer savePOSItemTransfer(POSItemTransfer tfr)
        {
            return db.SaveEntity<POSItemTransfer>(tfr, tfr.TableName);
        }

        public void deletePOSItemTransfer(int id)
        {
            POSItemTransfer b = getPOSItemTransfer(id);
            if (b != null)
                db.DeleteObject(b);
        }
        #endregion


        #region POS
        public POSe getPOS(int id)
        {
            return db.GetById<POSe>(x => x.Id == id);
        }

        public IQueryable<POSe> getPOSes()
        {
            return db.GetAll<POSe>();
        }

        public POSe savePOS(POSe order)
        {
            return db.SaveEntity<POSe>(order, order.TableName);
        }

        public void deletePOS(int id)
        {
            POSe b = getPOS(id);
            if (b != null)
                db.DeleteObject(b);
        }
        #endregion

        #region Order Items
        public OrderItem getOrderItem(int id)
        {
            return db.GetById<OrderItem>(x => x.Id == id);
        }

        public IQueryable<OrderItem> getOrderItems()
        {
            return db.GetAll<OrderItem>();
        }

        public IQueryable<OrderItem> getOrderItems(int id)
        {
            return db.GetAll<OrderItem>().Where(x => x.OrderId == id);
        }

        public OrderItem saveOrderItem(OrderItem entity)
        {
            return db.SaveEntity<OrderItem>(entity, entity.TableName);
        }

        public void deleteOrderItem(int id)
        {
            OrderItem b = getOrderItem(id);
            if (b != null)
                db.DeleteObject(b);
        }
        #endregion

        #region Sales
        public Sale getSale(int id)
        {
            return db.GetById<Sale>(x => x.Id == id);
        }
        public Sale getSale(string refno)
        {
            return db.GetById<Sale>(x => x.SalesRef == refno);
        }
        public IQueryable<Sale> getSales()
        {
            return db.GetAll<Sale>();
        }

        public Sale saveSales(Sale entity)
        {
            return db.SaveEntity<Sale>(entity, entity.TableName);
        }

        public void deleteSales(int id)
        {
            Sale b = getSale(id);
            if (b != null)
                db.DeleteObject(b);
        }
        #endregion

        #region SalesDetails

        public SalesDetail getSalesDetail(int id)
        {
            return db.GetById<SalesDetail>(x => x.Id == id);
        }
      
        public IQueryable<SalesDetail> getSalesDetails()
        {
            return db.GetAll<SalesDetail>();
        }
        public IQueryable<SalesDetail> getSalesDetls(int id)
        {
            return db.GetAll<SalesDetail>().Where(x => x.SalesId == id);
        }

        public SalesDetail saveSalesDetail(SalesDetail entity)
        {
            return db.SaveEntity<SalesDetail>(entity, entity.TableName);
        }

        public void deleteSalesDetail(int id)
        {
            SalesDetail b = getSalesDetail(id);
            if (b != null)
                db.DeleteObject(b);
        }

        #endregion

        #region Salespayment

        public SalesPayment getSalespayment(int id)
        {
            return db.GetById<SalesPayment>(x => x.Id == id);
        }

        public IQueryable<SalesPayment> getSalespayments()
        {
            return db.GetAll<SalesPayment>();
        }
        
        public SalesPayment saveSalespayment(SalesPayment entity)
        {
            return db.SaveEntity<SalesPayment>(entity, entity.TableName);
        }

        public void deleteSalespayment(int id)
        {
            SalesPayment b = getSalespayment(id);
            if (b != null)
                db.DeleteObject(b);
        }

        #endregion


        #region PaymentTypes
        public PaymentType getPaymentType(int id)
        {
            return db.GetById<PaymentType>(x => x.Id == id);
        }

        public IQueryable<PaymentType> getPaymentTypes()
        {
            return db.GetAll<PaymentType>();
        }

        public PaymentType savePaymentTypes(PaymentType entity)
        {
            return db.SaveEntity<PaymentType>(entity, entity.TableName);
        }

        public void deletePaymentTypes(int id)
        {
            PaymentType b = getPaymentType(id);
            if (b != null)
                db.DeleteObject(b);
        }
        #endregion

        #region StockItems
        public StockItem getStockItem(int id)
        {
            return db.GetById<StockItem>(x => x.Id == id);
        }

        public StockItem getStockItemByProductId(int prodId, int posId)
        {
            return db.GetById<StockItem>(x => x.ProductId== prodId && x.posid==posId);
        }

        public IQueryable<StockItem> getStockItems()
        {
            return db.GetAll<StockItem>();
        }

        public StockItem saveStockItems(StockItem entity)
        {
            return db.SaveEntity<StockItem>(entity, entity.TableName);
        }

        public void deleteStockItems(int id)
        {
            StockItem b = getStockItem(id);
            if (b != null)
                db.DeleteObject(b);
        }
        #endregion

        #region PaymentMode
        public PaymentMode getPaymentMode(int id)
        {
            return db.GetById<PaymentMode>(x => x.Id == id);
        }

        public IQueryable<PaymentMode> getPaymentModes()
        {
            return db.GetAll<PaymentMode>();
        }

        public PaymentMode savePaymentMode(PaymentMode entity)
        {
            return db.SaveEntity<PaymentMode>(entity, entity.TableName);
        }

        public void deletePaymentModes(int id)
        {
            PaymentMode b = getPaymentMode(id);
            if (b != null)
                db.DeleteObject(b);
        }
        #endregion

        #region CreditPayment
        public CreditPayment getCreditPayment(int id)
        {
            return db.GetById<CreditPayment>(x => x.Id == id);
        }

        public IQueryable<CreditPayment> getCreditPayments()
        {
            return db.GetAll<CreditPayment>();
        }

        public CreditPayment saveCreditPayment(CreditPayment entity)
        {
            return db.SaveEntity<CreditPayment>(entity, entity.TableName);
        }

        public void deleteCreditPayment(int id)
        {
            CreditPayment b = getCreditPayment(id);
            if (b != null)
                db.DeleteObject(b);
        }
        #endregion

        #region BankTransaction
        public BankTransaction getBankTransaction(int id)
        {
            return db.GetById<BankTransaction>(x => x.Id == id);
        }
        public IQueryable<BankTransaction> getBankTransactions()
        {
            return db.GetAll<BankTransaction>();
        }
        public IQueryable<BankTransaction> getBankTransactions(int Id)
        {
            return db.GetAll<BankTransaction>().Where(x => x.AccountId == Id);
        }
        public BankTransaction saveBankTransaction(BankTransaction entity)
        {
            return db.SaveEntity<BankTransaction>(entity, entity.TableName);
        }

        public void deleteBankTransaction(int id)
        {
            BankTransaction b = getBankTransaction(id);
            if (b != null)
                db.DeleteObject(b);
        }
        #endregion

        #region TransactionType
        public TransactionType getTransactionType(int id)
        {
            return db.GetById<TransactionType>(x => x.Id == id);
        }
        public IQueryable<TransactionType> getTransactionTypes()
        {
            return db.GetAll<TransactionType>();
        }
        #endregion

        #region PostingType
        public PostingType getPostingType(int id)
        {
            return db.GetById<PostingType>(x => x.Id == id);
        }
        public IQueryable<PostingType> getPostingTypes()
        {
            return db.GetAll<PostingType>();
        }
        #endregion

        #region Credits
        public Credit getcredit(int id)
        {
            return db.GetById<Credit>(x => x.Id == id);
        }

        public IQueryable<Credit> getcredits()
        {
            return db.GetAll<Credit>();
        }

        public Credit savecredit(Credit entity)
        {
            return db.SaveEntity<Credit>(entity, entity.TableName);
        }

        public void deletecredit(int id)
        {
            Credit b = getcredit(id);
            if (b != null)
                db.DeleteObject(b);
        }
        #endregion   

        #region Density

        public Density getDensity(int id)
        {
            return db.Densities.Where(x => x.Id == id).FirstOrDefault();
        }

        public IQueryable<Density> getDensities()
        {
            return db.Densities.AsQueryable();
        }

        public Density saveDensity(Density dens)
        {
            return Save<Density>(dens, dens.TableName);
        }

        public void deleteDensity(int id)
        {
            Density b = getDensity(id);
            if (b != null)
                db.DeleteObject(b);

        }
        #endregion

        #region ExpenseCategory

        public ExpenseCategory getExpenseCategory(int id)
        {
            return db.ExpenseCategories.Where(x => x.Id == id).FirstOrDefault();
        }
        public IQueryable<ExpenseCategory> getExpenseCategories()
        {
            return db.ExpenseCategories.AsQueryable();
        }
        public IQueryable<ExpenseCategory> getExpenseCategories(int id)
        {
            return db.GetAll<ExpenseCategory>().Where(x => x.ExpenseGroupId == id);
        }


        public ExpenseCategory saveExpenseCategory(ExpenseCategory expensecategory)
        {
            return Save<ExpenseCategory>(expensecategory, expensecategory.TableName);
        }
        public void deleteExpCategory(int id)
        {
            ExpenseCategory expcat = getExpenseCategory(id);
            if (expcat != null)
                db.DeleteObject(expcat);
        }

        #endregion

        #region Cash Collection

        public CashCollection getCashCollection(int id)
        {
            return db.GetById<CashCollection>(x => x.Id == id);
        }
        public IQueryable<CashCollection> getCashCollections()
        {
            return db.GetAll<CashCollection>();

        }
        public CashCollection saveCashCollection(CashCollection cashcollection)
        {
            return db.SaveEntity<CashCollection>(cashcollection, cashcollection.TableName);
        }
        public void deleteCashCollection(int id)
        {
            CashCollection c = getCashCollection(id);
            if (c != null)
                db.DeleteObject(c);
        }
        #endregion

        #region Expense

        public Expense getExpense(int id)
        {
            return db.GetById<Expense>(x => x.Id == id);
        }
        public IQueryable<Expense> getExpenses()
        {
            return db.Expenses.AsQueryable();
        }
        public IQueryable<Expense> getExpenses(int id)
        {
            return db.GetAll<Expense>().Where( x =>x.ExpenseCategoryId ==id); 
                         
        }

        public Expense saveExpense(Expense expense)
        {
            return Save<Expense>(expense, expense.TableName);
        }

        public void deleteExpense(int id)
        {
            Expense exp = getExpense(id);
            if (exp != null)
                 db.DeleteObject(exp);
        }


        #endregion

        #region ExpenseGroup

        public ExpenseGroup getExpenseGroup(int id)
        {
            return db.GetById<ExpenseGroup>(x => x.Id == id);
        }

        public IQueryable<ExpenseGroup> getExpenseGroups()
        {
            return db.ExpenseGroups.AsQueryable();
        
        }
        public ExpenseGroup saveExpenseGroup(ExpenseGroup expgroup)
        {
            return Save<ExpenseGroup>(expgroup,expgroup.TableName);
        }
        public void deletexpenseGroup(int id)
        {
            ExpenseGroup expgrp = getExpenseGroup(id);
            if (expgrp != null)
            {
                db.DeleteObject(expgrp);
            }

        }

        #endregion

        #region Productions

        public Production getProduction(int id)
        {
            return db.GetById<Production>(x => x.Id == id);
        }

        public Production getProduction(string batch)
        {
            return db.GetById<Production>(x => x.BatchNo == batch);
        }

        public IQueryable<Production> getProductions()
        {
            return db.GetAll<Production>();
        }

        public Production saveProduction(Production obj)
        {
            return db.SaveEntity<Production>(obj, obj.TableName);
        }

        public void deleteProduction(int id)
        {

        }

        #endregion

        #region Production Items

        public ProductionItem getProductionItem(int id)
        {
            return db.GetById<ProductionItem>(x => x.Id == id);
        }

        public IQueryable<ProductionItem> getProductionItems(int id)
        {
            return db.GetAll<ProductionItem>().Where(x => x.ProductionId == id);
        }

        public ProductionItem saveProductionItem(ProductionItem obj)
        {
            return db.SaveEntity<ProductionItem>(obj, obj.TableName);
        }

        public void deleteProductionItem(int id)
        {
            ProductionItem p = getProductionItem(id);
            if (p != null)
                db.DeleteObject(p);
        }

        #endregion

        #region Special Orders

        public SpecialOrder getSpecialOrder(int id)
        {
            return db.GetById<SpecialOrder>(x => x.Id == id);
        }

        public IQueryable<SpecialOrder> getSpecialOrders()
        {
            return db.GetAll<SpecialOrder>();
        }

        public SpecialOrder saveSpecialOrder(SpecialOrder specialorder)
        {
            return db.SaveEntity<SpecialOrder>(specialorder, specialorder.TableName);
        }

        public void deleteSpecialOrder(int id)
        {
            SpecialOrder b = getSpecialOrder(id);
            if (b != null)
                db.DeleteObject(b);
        }

        #endregion

        #region Order Dispatch

        public OrderDispatch getOrderDispatch(int id)
        {
            return db.GetById<OrderDispatch>(x => x.Id == id);
        }

        public IQueryable<OrderDispatch> getOrderDispatches()
        {
            return db.GetAll<OrderDispatch>();
        }

        public IQueryable<OrderDispatch> getOrderDispatches(int id)
        {
            return db.GetAll<OrderDispatch>().Where(x => x.OrderId == id); ;
        }
        public OrderDispatch saveOrderDispatch(OrderDispatch orderdispatch)
        {
            return db.SaveEntity<OrderDispatch>(orderdispatch, orderdispatch.TableName);
        }

        public void deleteOrderDispatch(int id)
        {
            OrderDispatch b = getOrderDispatch(id);
            if (b != null)
                db.DeleteObject(b);
        }

        #endregion

        #region Regions
        public IQueryable<Region> getRegions()
        {
            return db.GetAll<Region>();
        }

        #endregion

        #region Discount
        public Discount getDiscount(int id)
        {
            return db.GetById<Discount>(x => x.Id == id);
        }
        public IQueryable<Discount> getDiscounts()
        {
            return db.GetAll<Discount>();
        }
        public Discount saveDiscount(Discount discount)
        {
            return db.SaveEntity<Discount>(discount, discount.TableName);
        }
        public void deleteDiscount(int id)
        {
            Discount ds = getDiscount(id);
            if (ds != null)
                db.DeleteObject(ds);
        }
        #endregion
    }
}
