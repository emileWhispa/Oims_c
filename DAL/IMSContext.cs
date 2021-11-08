using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace orion.ims.DAL
{
    public class IMSContext : DbContext
    {
        #region Properties

        #endregion

        #region Constructor
        public IMSContext(string connString)
            : base(connString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }

        public ObjectContext ObjectContext()
        {
            return ((IObjectContextAdapter)this).ObjectContext;
        }

        #endregion

        #region DbSets declaration
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CashCollection> CashCollections { get;set; }
        public DbSet<Discount> Discounts { get;set;}
        public DbSet<MaintenanceStatus> MaintenanceStatus { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Pass_Hist> Pass_Hist { get; set; }
        public DbSet<SysParam> SysParams { get; set; }
        public DbSet<UserAudit> UserAudit { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserStatus> Userstatus { get; set; }
        public DbSet<Maintenance_Auth> Maintenance_Auth { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Right> Rights { get; set; }        
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<StockItem> StockItems { get; set; }
        public DbSet<CreditPayment> CreditPayments { get; set; }
        public DbSet<BankTransaction> BankTransactions { get; set; }
        public DbSet<PostingType> PostingType { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<Credit> Credits { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<PaymentMode> PaymentModes { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<OrderDispatch> OrderDispatch { get; set; }
        public DbSet<SalesDetail> SalesDetails { get; set; }
        public DbSet<SalesPayment> SalesPayments { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<Density> Densities { get; set; }
        public DbSet<POSe> POSes { get; set; }
        public DbSet<POSOrder> POSOrders { get; set; }

        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseGroup> ExpenseGroups { get; set; }

        public DbSet<ProductionItem> ProductionItems { get; set; }
        public DbSet<Production> Productions { get; set; }

        public DbSet<SpecialOrder> SpecialOrders { get; set; }
        public DbSet<POSItemTransfer> POSItemTransfers { get; set; }

        public DbSet<ItemDelivery> ItemDelivery { get; set; }

        #endregion

        #region Generic Methods
        public T GetById<T>(Func<T, bool> predicate) where T : class
        {
            return this.ObjectContext().CreateObjectSet<T>().FirstOrDefault(predicate);
        }

        public void SaveObject<T>(T entity, string entityName) where T : class
        {
            System.Data.Entity.Core.EntityKey key = this.ObjectContext().CreateEntityKey(entityName, entity);
            object o;
            this.ObjectContext().TryGetObjectByKey(key, out o);
            if (o == null)
                this.ObjectContext().CreateObjectSet<T>().AddObject(entity);
            else
                this.ObjectContext().ApplyCurrentValues(entityName, entity);
            this.SaveChanges();
        }

        public T SaveEntity<T>(T entity, string entityName) where T : class
        {
            System.Data.Entity.Core.EntityKey key = this.ObjectContext().CreateEntityKey(entityName, entity);
            object o;
            this.ObjectContext().TryGetObjectByKey(key, out o);
            if (o == null)
                this.ObjectContext().CreateObjectSet<T>().AddObject(entity);
            else
                this.ObjectContext().ApplyCurrentValues(entityName, entity);
            this.SaveChanges();
         //   this.ObjectContext().Refresh(RefreshMode.ClientWins,entityName);
            return entity;
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return this.ObjectContext().CreateObjectSet<T>().AsQueryable();
        }

        public void DeleteObject<T>(T entity) where T : class
        {
            this.ObjectContext().CreateObjectSet<T>().DeleteObject(entity);
            this.SaveChanges();
        }

        public IQueryable<T> Exec<T>(string sQuery) where T : class
        {
            return this.ObjectContext().ExecuteStoreQuery<T>(sQuery).AsQueryable();
        }

        public void Exec(string sQuery)
        {
            this.ObjectContext().ExecuteStoreCommand(sQuery, null);
        }

        public IQueryable<T> Search<T>(Func<T, bool> predicate) where T : class
        {
            return this.ObjectContext().CreateObjectSet<T>().Where(predicate).AsQueryable();
        }

        public T Max<T>(Func<T, bool> predicate) where T : class
        {
            throw new NotImplementedException();
        }

        public int NextId<T>(Func<T, int> field) where T : class
        {
            int id = this.ObjectContext().CreateObjectSet<T>().Max(field);
            return id + 1;
            //throw new NotImplementedException();
        }
        #endregion
    }
}
