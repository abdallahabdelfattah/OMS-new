// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfWorkBase.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Data
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Reflection;
    using System.Web;

    using Commons.Framework.Extensions;
    using Commons.Framework.Logging;

    #endregion

    /// <summary>
    /// The unit of work base.
    /// </summary>
    public class UnitOfWorkBase : IUnitOfWork
    {
        /// <summary>
        ///     The Logger
        /// </summary>
        private static readonly Logger Logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType);

        protected DbContextTransaction dbTransaction;

        /// <summary>
        /// The context.
        /// </summary>
        protected DbContext context;

        /// <summary>
        /// Calling SaveChanges does create a DB transaction so
        ///     every query executed against the DB will be rollbacked is something goes wrong.
        /// </summary>
        /// <param name="userId">
        /// The user Id.
        /// </param>
        /// <param name="validateOnSaveEnabled">
        /// The validate On Save Enabled.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Save(string userId = null, bool validateOnSaveEnabled = true, string checkTRXColumnName = null,bool updatePropertiesBeforeSave = true)
        {
            // In all versions of Entity Framework, whenever you execute SaveChanges() to insert, update or delete on the 
            // database the framework will wrap that operation in a transaction. This transaction lasts only long enough to             
            // execute the operation and then completes. When you execute another such operation a new transaction is started.
            try
            {
                //bool checkTRX = !string.IsNullOrEmpty(checkTRXColumnName);
                this.context.Configuration.ValidateOnSaveEnabled = validateOnSaveEnabled;
                this.context.Configuration.ProxyCreationEnabled = false;
                this.context.Configuration.AutoDetectChangesEnabled = updatePropertiesBeforeSave;
                //if (checkTRX)
                //{
                //    CheckTRXExistBeforeSave(checkTRXColumnName);
                //}

                if (updatePropertiesBeforeSave)
                {
                    this.UpdatePropertiesBeforeSave(userId);
                }
                
                return this.context.SaveChanges();

                //return 0;
            }
            catch (DbEntityValidationException entityValidationException)
            {
                string validationErrors = null;
                foreach (var entityValidationError in entityValidationException.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        validationErrors +=
                            $"Entity: {entityValidationError.Entry.Entity} - Property: {validationError.PropertyName} - Error: {validationError.ErrorMessage} \n ";
                    }
                }

                var exception = new Exception(validationErrors);
                Logger.Error(exception);
                throw exception;
            }
        }

        public DbContextTransaction BeginTransaction()
        {
            this.dbTransaction = this.context.Database?.BeginTransaction();
            return this.dbTransaction;
        }

        public void UseTransaction(DbContextTransaction transaction)
        {
            this.context.Database.UseTransaction(transaction.UnderlyingTransaction);
        }

        public void CommitTransaction()
        {
            this.dbTransaction?.Commit();
        }

        public void RollbackTransaction()
        {
            this.dbTransaction?.Rollback();
        }

        public void DisposeTransaction()
        {
            this.context.Database.CurrentTransaction?.Dispose();
            this.dbTransaction?.Dispose();
        }

        /// <summary>
        /// Updates the properties before save.
        /// </summary>
        /// <param name="userId">
        /// The user identifier.
        /// </param>
        private void UpdatePropertiesBeforeSave(string userId = null)
        {
            if (string.IsNullOrEmpty(userId) && HttpContext.Current != null && HttpContext.Current.User != null
                && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                userId = HttpContext.Current.User.Identity.Name;
            }

            const string CreatedOnProperty = "CreatedOn";
            const string ModifiedOnProperty = "UpdatedOn";

            // const string IsActiveProperty = "IsActive";
            const string IdProperty = "Id";

            var entitiesWithCreatedOn = this.context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added && e.Entity.GetType().GetProperty(CreatedOnProperty) != null);
            foreach (var entry in entitiesWithCreatedOn)
            {
                entry.Property(CreatedOnProperty).CurrentValue = DateTime.Now;
            }

            // IEnumerable<DbEntityEntry> entitiesWithStateCode =
            // this.context.ChangeTracker.Entries()
            // .Where(
            // e => e.State == EntityState.Added && e.Entity.GetType().GetProperty(IsActiveProperty) != null);
            // foreach (DbEntityEntry entry in entitiesWithStateCode)
            // {
            // entry.Property(IsActiveProperty).CurrentValue = true;
            // }
            var entitiesWithId = this.context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added && e.Entity.GetType().GetProperty(IdProperty) != null);
            foreach (var entry in entitiesWithId)
            {
                Guid id;

                if (Guid.TryParse(entry.Property(IdProperty).CurrentValue.ToString(), out id) && id == Guid.Empty)
                {
                    entry.Property(IdProperty).CurrentValue = Guid.NewGuid().AsSequentialGuid();
                }
            }

            var entitiesWithModifiedOn = this.context.ChangeTracker.Entries()
                .Where(
                    e => e.State == EntityState.Modified && e.Entity.GetType().GetProperty(ModifiedOnProperty) != null);
            foreach (var entry in entitiesWithModifiedOn)
            {
                entry.Property(ModifiedOnProperty).CurrentValue = DateTime.Now;
            }

            if (!string.IsNullOrEmpty(userId))
            {
                const string CreatedByProperty = "CreatedBy";
                const string ModifiedByProperty = "UpdatedBy";
                var entitiesWithCreatedBy = this.context.ChangeTracker.Entries()
                    .Where(
                        e => e.State == EntityState.Added && e.Entity.GetType().GetProperty(CreatedByProperty) != null);
                foreach (var entry in entitiesWithCreatedBy)
                {
                    entry.Property(CreatedByProperty).CurrentValue = userId;
                }

                var entitiesWithModifiedBy = this.context.ChangeTracker.Entries()
                    .Where(
                        e => e.State == EntityState.Modified
                             && e.Entity.GetType().GetProperty(ModifiedByProperty) != null);
                foreach (var entry in entitiesWithModifiedBy)
                {
                    entry.Property(ModifiedByProperty).CurrentValue = userId;
                }
            }
        }

        private void CheckTRXExistBeforeSave(string ColumnName)
        {


            string TransSeqNumber = ColumnName;

            //const string IsActiveProperty = "IsActive";
            //const string IdProperty = "Id";

            var entitiesWithTransSeqNumber = this.context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added && e.Entity.GetType().GetProperty(TransSeqNumber) != null);
            var ListOfTRX = new List<string>();
            foreach (var entry in entitiesWithTransSeqNumber)
            {
                if (!ListOfTRX.Contains(entry.Property(TransSeqNumber).CurrentValue.ToString()))
                {
                    ListOfTRX.Add(entry.Property(TransSeqNumber).CurrentValue.ToString());
                }
                else
                {
                    entry.State = EntityState.Detached;
                }

            }



        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.DisposeTransaction();
            this.context?.Dispose();
        }
    }
}