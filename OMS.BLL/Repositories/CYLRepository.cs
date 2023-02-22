using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Z.EntityFramework.Plus;
using OMS.BLL.Data;
using OMS.DAL.DataAccess;
using OMS.Model.ViewModel;
using OMS.Model.ViewModel.SC;

namespace OMS.BLL.Repositories
{
    public class CYLRepository:RepositoryBase<CYL>
    {
        public Dictionary<int, string> GetNames() => GetAllFromCache().ToDictionary(o => o.Id, o => o.Name);
        internal List<CYL> GetAllFromCache() => DbSet.AsNoTracking().ToList();
        public CYLRepository(DbContext context) : base(context)
        {
        }

        public List<LookupModel> GetCYLLookUpModel()
        {
            return new List<LookupModel>
            {
                new LookupModel
                {
                    Id = 0, Name = "0"
                },
                new LookupModel
                {
                    Id = 1, Name = "-0.25"
                },
                new LookupModel
                {
                    Id = 2, Name = "-0.5"
                },
                new LookupModel
                {
                    Id = 3, Name = "-0.75"
                },
                new LookupModel
                {
                    Id = 4, Name = "-1.00"
                },
                new LookupModel
                {
                    Id = 5, Name = "-1.25"
                },
                new LookupModel
                {
                    Id = 6, Name = "-1.5"
                },
                new LookupModel
                {
                    Id = 7, Name = "-1.75"
                },
                new LookupModel
                {
                    Id = 8, Name = "-2.00"
                },
                new LookupModel
                {
                    Id = 9, Name = "-2.25"
                },
                new LookupModel
                {
                    Id = 10, Name = "-2.5"
                },
                new LookupModel
                {
                    Id = 11, Name = "-2.75"
                },
                new LookupModel
                {
                    Id = 12, Name = "-3.00"
                },
                new LookupModel
                {
                    Id = 13, Name = "-3.25"
                },
                new LookupModel
                {
                    Id = 14, Name = "-3.5"
                },
                new LookupModel
                {
                    Id = 15, Name = "-3.75"
                },
                new LookupModel
                {
                    Id = 16, Name = "-4.00"
                },
                new LookupModel
                {
                    Id = 17, Name = "-4.25"
                },
                new LookupModel
                {
                    Id = 18, Name = "-4.5"
                },
                new LookupModel
                {
                    Id = 19, Name = "-4.75"
                },
                new LookupModel
                {
                    Id = 20, Name = "-5.00"
                },
                new LookupModel
                {
                    Id = 21, Name = "-5.25"
                },
                new LookupModel
                {
                    Id = 22, Name = "-5.5"
                },
                new LookupModel
                {
                    Id = 23, Name = "-5.75"
                },
                new LookupModel
                {
                    Id = 24, Name = "-6.00"
                }
            };
        }
        
    }
}
