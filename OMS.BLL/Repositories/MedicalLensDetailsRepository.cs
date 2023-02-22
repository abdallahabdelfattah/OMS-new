using OMS.DAL;
using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.BLL.Data;
using OMS.Model.ViewModel;
using System.Web.Configuration;
using Commons.Framework.Extensions;
using System.Linq.Dynamic;

namespace OMS.BLL.Repositories
{
    public partial class MedicalLensDetailsRepository : RepositoryBase<MedicalLensDetail>
    {
        public MedicalLensDetailsRepository(DbContext context)
            : base(context)
        {
        }

        public List<MedicalLensDetailsVM> PrepareDtails(int id, int typeId)
        {
            var uow = new OMSUoW();
            var medicalLensDetail = DbSet.Include(a => a.SPH)
                                                                     .Where(a => a.MedicalLensMasterId == id && a.MedicalLensDetailsTypeId == typeId).Select(a => new MedicalLensDetailsVM
                                                                     {
                                                                         CYL_0 = a.CYL_0,
                                                                         CYL_0_25 = a.CYL_0_25,
                                                                         CYL_0_5 = a.CYL_0_5,
                                                                         CYL_0_75 = a.CYL_0_75,
                                                                         CYL_1 = a.CYL_1,
                                                                         CYL_1_25 = a.CYL_1_25,
                                                                         CYL_1_5 = a.CYL_1_5,
                                                                         CYL_1_75 = a.CYL_1_75,
                                                                         CYL_2 = a.CYL_2,
                                                                         CYL_2_25 = a.CYL_2_25,
                                                                         CYL_2_5 = a.CYL_2_5,
                                                                         CYL_2_75 = a.CYL_2_75,
                                                                         CYL_3 = a.CYL_3,
                                                                         CYL_3_25 = a.CYL_3_25,
                                                                         CYL_3_5 = a.CYL_3_5,
                                                                         CYL_3_75 = a.CYL_3_75,
                                                                         CYL_4 = a.CYL_4,
                                                                         CYL_4_25 = a.CYL_4_25,
                                                                         CYL_4_5 = a.CYL_4_5,
                                                                         CYL_4_75 = a.CYL_4_75,
                                                                         CYL_5 = a.CYL_5,
                                                                         CYL_5_25 = a.CYL_5_25,
                                                                         CYL_5_5 = a.CYL_5_5,
                                                                         CYL_5_75 = a.CYL_5_75,
                                                                         CYL_6 = a.CYL_6,
                                                                         Id = a.Id,
                                                                         MedicalLensMasterId = a.MedicalLensMasterId,
                                                                         SphId = a.SphId,
                                                                         SphName = a.SPH.Name
                                                                     }).ToList();

            return medicalLensDetail;


        }

        public int GetMedicalLensQty(long medicalLensId, int sphId, string cylColumn)
        {
            try
            {
                //ToDO Convert MedicalLensDetailsTypeId == 1 to enum
                int result = 0;
                //var res = DbSet.SqlQuery(
                  //  $"SELECT {cylColumn}  FROM MedicalLensDetails where MedicalLensMasterId = {medicalLensId} and SphId = {sphId}").FirstOrDefault<MedicalLensDetail>();

                //using (var con = new EntityConnection("name=Entities"))
                //{
                //    con.Open();
                //    EntityCommand cmd = con.CreateCommand();
                //    cmd.CommandText = $"SELECT {cylColumn}  FROM MedicalLensDetails where dbEntities.MedicalLensMasterId = {medicalLensId} and SphId = {sphId}";
                //    using (EntityDataReader rdr = cmd.ExecuteReader())
                //    {
                //        while (rdr.Read())
                //        {
                //            result = rdr.GetInt32(0);
                //        }
                //    }
                //}

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["OMSConnectionString"].ConnectionString);
                conn.Open();

                SqlCommand command = new SqlCommand($"SELECT {cylColumn}  FROM MedicalLensDetails where MedicalLensMasterId = {medicalLensId} and SphId = {sphId}", conn);
                // int result = command.ExecuteNonQuery();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = reader.GetInt32(0);

                    }
                }

                conn.Close();
                return result;

                //var result = DbSet.Where(a =>
                //    a.SphId == sphId && a.MedicalLensMasterId == medicalLensId && a.MedicalLensDetailsTypeId == 1);
                //if (result.ToList().Count > 0)
                //{
                //    var res = result.Select(new MedicalLensDetail{$"{cylColumn}"
                //}).FirstOrDefaultDynamic();
                //    return int.Parse(result.ToString());
                //}
                //return 0;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void SaveLensDetails(List<MedicalLensDetail> medicalLensDetailsList)
        {
            foreach (var LensDetail in medicalLensDetailsList)
            {
                if (LensDetail.SphId != null)
                {

                    this.DbSet.AddOrUpdate(LensDetail);
                }
            }

            this.Context.SaveChanges();
        }

        public bool UpdateMedicalLensQty(long medicalLensId, int sphIdValue, string cylColumnName, int qty)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["OMSConnectionString"].ConnectionString);
                conn.Open();

                SqlCommand command = new SqlCommand($"Update MedicalLensDetails  set {cylColumnName} = {qty} where MedicalLensMasterId = {medicalLensId} and SphId = {sphIdValue}", conn);
                var result = command.ExecuteNonQuery();
                conn.Close();
                return result == 0;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}