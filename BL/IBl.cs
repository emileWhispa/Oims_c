using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using orion.ims.DAL;

namespace orion.ims.BL
{
    public interface IBl
    {
        #region Generic Methods
        IQueryable<T> Fetch<T>() where T : class;

        IQueryable<T> Search<T>(Func<T, bool> predicate) where T : class;

        void Execsql(string Sql);

        T Save<T>(T entity, string eName) where T : class;

        void Save<T>(List<T> entities) where T : class;

        void Delete<T>(Func<T, bool> predicate) where T : class;
        #endregion

        #region Banks
        Bank getBank(int id);

        IQueryable<Bank> getBanks();

        Bank saveBank(Bank bank);

        void deleteBank(int id);
        #endregion
        #region Branches
        Branch getBranch(int id);

        IQueryable<Branch> getBranches();

        IQueryable<Branch> getBankBranches(int bankId);

        Branch saveBranch(Branch branch);

        void deleteBranch(int id);
        #endregion

        #region Currencies
        Currency getCurrency(int id);

        Currency getCurrency(string code);

        IQueryable<Currency> getCurrencies();

        Currency saveCurrency(Currency currency);

        void deleteCurrency(int id);
        #endregion

        #region Densities
        Density getDensity(int id);

        IQueryable<Density> getDensities();

        Density saveDensity(Density density);

        void deleteDensity(int id);
        #endregion

        #region Users
        User getUser(int id);

        User getUser(string uname);

        IQueryable<User> getUsers();

        User processUserPass(User u, string uSalt = "");

        User saveUser(User u);

        void deleteUser(int id);

        bool ValidateUser(int id, string password, out string msg, out UserLoginStatus stat);

        bool ValidateUserPass(int id, string password);

        bool IsLoginOk(string userName, string password, string sKey, out string resp, out User user);
        //IQueryable<Vw_user> GetUserlist();
        #endregion

        #region UserGroups
        UserGroup getUserGroup(int id);

        IQueryable<UserGroup> getUserGroups();

        UserGroup saveUserGroup(UserGroup userGroup);

        void deleteUserGroup(int id);
        #endregion

        #region Settings
        SysParam getSettingParam(int id);
        SysParam getSettingParam(string pName);

        IQueryable<SysParam> getSettingParams();

        SysParam saveSettingParam(SysParam s);
        void deleteparam(int id);
        #endregion

        #region PassHist
        Pass_Hist getpass(int id);

        IQueryable<Pass_Hist> getpassbyUser(int id);

        IQueryable<Pass_Hist> getPassHist();

        Pass_Hist savePassHist(Pass_Hist passhist);

        void deletepassHist(int id);
        #endregion

        #region Menus
        IQueryable<Menu> GetMenus();
        #endregion

        #region Rights
        IQueryable<Right> GetRights();

        bool CheckMenuRights(string menuUrl);

        IQueryable<vw_ProfileMenus> GetProfileMenus(int groupId);

        void SaveMenuRights(vw_ProfileMenus menu);

        IQueryable<GroupRights> GetGroupRights(int groupId, int moduleId);

        Right SaveRights(Right right);

        #endregion

        #region Makerchecker
        Maintenance_Auth getMaintenaceAuth(int id);

        Maintenance_Auth getMaintenaceAuth(string name);

        IQueryable<Maintenance_Auth> getMaintenaceAuths();

        Maintenance_Auth saveMaintenaceAuth(Maintenance_Auth c);
        #endregion

        #region Cash Collection

        CashCollection getCashCollection(int id);
        IQueryable<CashCollection> getCashCollections();
        CashCollection saveCashCollection(CashCollection cashcollection);
        void deleteCashCollection(int id);


        #endregion
        #region Accounts
        Account getAccount(int id);

        Account getAccount(string accNo);

        IQueryable<Account> getAccounts();

        Account saveAccount(Account a);

        void deleteAccount(int id);

        IQueryable<Account> getBankAccounts(int bankId);

        #endregion

        #region BankTransactions
        BankTransaction getBankTransaction(int id);

        IQueryable<BankTransaction> getBankTransactions(int Id);

        IQueryable<BankTransaction> getBankTransactions();

        BankTransaction saveBankTransaction(BankTransaction a);

        void deleteBankTransaction(int id);
        #endregion

        #region Reports
        IQueryable<Report> GetReports();

        Report GetReport(int id);

        //string PrintReport(ReportView repView, string workingDir, string userName, string logoFile);
        #endregion

        #region EOD
        string runEOD(int userId);
        #endregion

        #region Products
        Product getProduct(int id);

        IQueryable<Product> getProducts();

        Product saveProduct(Product Product);

        void deleteProduct(int id);
        #endregion

        #region ItemDelivery
        ItemDelivery getItemDelivery(int id);

        IQueryable<ItemDelivery> getItemDeliveries();

        ItemDelivery saveItemDelivery(ItemDelivery deliv);

        void deleteItemDelivery(int id);
        #endregion


        #region Products Categories

        ProductCategory getProductCategory(int id);

        IQueryable<ProductCategory> getProductCategories();

        ProductCategory saveProductCategory(ProductCategory obj);

        void deleteProductCategory(int id);

        #endregion

        #region Customer
        Customer getCustomer(int id);

        IQueryable<Customer> getCustomers();

        Customer saveCustomer(Customer Customer);

        void deleteCustomer(int id);
        #endregion

        //#region Orders
        //Order getOrder(int id);

        //IQueryable<Order> getOrders();

        //Order saveOrder(Order order);

        //void deleteOrder(int id);
        //#endregion

        #region POSOrders
        POSOrder getPOSOrder(int id);
        POSOrder getPOSOrder(string orderno);
        IQueryable<POSOrder> getPOSOrders();

        POSOrder savePOSOrder(POSOrder order);

        void deletePOSOrder(int id);
        #endregion
        POSItemTransfer getPOSItemTransfer(int id);
        IQueryable<POSItemTransfer> getPOSItemTransfers();
        POSItemTransfer savePOSItemTransfer(POSItemTransfer tfr);
        void deletePOSItemTransfer(int id);
        #region POSItemTransfers
        
        #endregion

        #region POS
        POSe getPOS(int id);

        IQueryable<POSe> getPOSes();

        POSe savePOS(POSe order);

        void deletePOS(int id);
        #endregion

        #region Order Items
        OrderItem getOrderItem(int id);

        IQueryable<OrderItem> getOrderItems();

        IQueryable<OrderItem> getOrderItems(int id);

        OrderItem saveOrderItem(OrderItem item);

        void deleteOrderItem(int id);
        #endregion

        #region Sales
        Sale getSale(int id);
        Sale getSale(string refno);
        IQueryable<Sale> getSales();

        Sale saveSales(Sale sale);

        void deleteSales(int id);
        #endregion

        #region SalesDetails

        SalesDetail getSalesDetail(int id);

        IQueryable<SalesDetail> getSalesDetails();

        IQueryable<SalesDetail> getSalesDetls(int id);

        SalesDetail saveSalesDetail(SalesDetail entity);

        void deleteSalesDetail(int id);
        #endregion

        #region PaymentTypes
        PaymentType getPaymentType(int id);

        IQueryable<PaymentType> getPaymentTypes();

        PaymentType savePaymentTypes(PaymentType Paymenttype);

        void deletePaymentTypes(int id);
        #endregion

        #region StockItems
        StockItem getStockItem(int id);

        IQueryable<StockItem> getStockItems();

        StockItem saveStockItems(StockItem StockItem);

        StockItem getStockItemByProductId(int prodId, int posId);
      

        void deleteStockItems(int id);
        #endregion

        #region PaymentModes
        PaymentMode getPaymentMode(int id);

        IQueryable<PaymentMode> getPaymentModes();

        PaymentMode savePaymentMode(PaymentMode Paymenttype);

        void deletePaymentModes(int id);
        #endregion

        #region Credits
        Credit getcredit(int id);

        IQueryable<Credit> getcredits();

        Credit savecredit(Credit credit);

        void deletecredit(int id);
        #endregion

        #region CreditPayment
        CreditPayment getCreditPayment(int id);
       
        IQueryable<CreditPayment> getCreditPayments();

        CreditPayment saveCreditPayment(CreditPayment CreditPayment);

        void deleteCreditPayment(int id);
        #endregion

        #region TransactionType
        TransactionType getTransactionType(int id);

        IQueryable<TransactionType> getTransactionTypes();
        #endregion

        #region PostingType
        PostingType getPostingType(int id);

        IQueryable<PostingType> getPostingTypes();
        #endregion

        #region ExpenseCategory

        ExpenseCategory getExpenseCategory(int id);

        IQueryable<ExpenseCategory> getExpenseCategories();

        IQueryable<ExpenseCategory> getExpenseCategories(int id);

        ExpenseCategory saveExpenseCategory(ExpenseCategory expensecategory);

        void deleteExpCategory(int id);
        #endregion

        #region Expense

        Expense getExpense(int id);

        IQueryable<Expense> getExpenses();
        IQueryable<Expense> getExpenses(int id);

        Expense saveExpense(Expense expense);

        void deleteExpense(int id);

        #endregion

        #region ExpenseGroup

        ExpenseGroup getExpenseGroup(int id);

        IQueryable<ExpenseGroup> getExpenseGroups();

        ExpenseGroup saveExpenseGroup(ExpenseGroup expgroup);

        void deletexpenseGroup(int id);



        #endregion

        #region Productions

        Production getProduction(int id);

        Production getProduction(string batch);
        IQueryable<Production> getProductions();

        Production saveProduction(Production obj);

        void deleteProduction(int id);

        #endregion

        #region Production Items

        ProductionItem getProductionItem(int id);

        IQueryable<ProductionItem> getProductionItems(int id);

        ProductionItem saveProductionItem(ProductionItem obj);

        void deleteProductionItem(int id);

        #endregion

        #region Special Order

        SpecialOrder getSpecialOrder(int id);

        IQueryable<SpecialOrder> getSpecialOrders();

        SpecialOrder saveSpecialOrder(SpecialOrder specialorder);

        void deleteSpecialOrder(int id);
        #endregion

        #region Order Dispatch

        OrderDispatch getOrderDispatch(int id);

        IQueryable<OrderDispatch> getOrderDispatches();

        IQueryable<OrderDispatch> getOrderDispatches(int id);

        OrderDispatch saveOrderDispatch(OrderDispatch orderdispatch);

        void deleteOrderDispatch(int id);
        #endregion

        #region Salespayment

        SalesPayment getSalespayment(int id);

        IQueryable<SalesPayment> getSalespayments();

        SalesPayment saveSalespayment(SalesPayment salepayment);

        void deleteSalespayment(int id);
        #endregion
        #region Region
        IQueryable<Region> getRegions();
        #endregion

        #region Discounts

        Discount getDiscount(int id);
        IQueryable<Discount> getDiscounts();
        Discount saveDiscount(Discount discount);
        void deleteDiscount(int id);

        #endregion
    }
}
