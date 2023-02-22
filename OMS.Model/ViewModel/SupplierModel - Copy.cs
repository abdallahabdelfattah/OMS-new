using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMS.Model.ViewModel
{
    public class SupplierSummaryModel : BaseModel
    {
        public SupplierSummaryModel()
        {
            
        }
        public SupplierSummaryModel(Supplier entity)
        {
            Id = entity.Id;
            Code = entity.Code;
            NameAr = entity.NameAr;
            NameEn = entity.NameEn;
            RegionName = entity.Region?.NameAr;
            Phone = entity.Phone;
            Balance = entity.Balance ?? 0;
            Mobile = entity.Mobile;
            BankName = entity.BankName;
            BankAccount = entity.BankAccount;
            Email = entity.Email;
            FaxNo = entity.FaxNo;
            CRNo = entity.CRNo;
            Address = entity.Address;
        }

        public string Code { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string RegionName { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
        public string BankName { get; set; }
        public string BankAccount { get; set; }
        public string FaxNo { get; set; }
        public string CRNo { get; set; }
        public string Address { get; set; }
    }

    public class SupplierModel : SupplierSummaryModel
    {
        public long? RegionId { get; set; }
        public List<SuppilerContactPersonModel> ContactPersons { get; set; } = new List<SuppilerContactPersonModel>();
        public SupplierModel() : base()
        { }
        public SupplierModel(Supplier entity) : base(entity)
        {
            RegionId = entity.RegionId;

            foreach (var item in entity.SupplierContactPersons)
            {
                ContactPersons.Add(new SuppilerContactPersonModel() { Title = item.Title, Name = item.NameAr, Email = item.Email, Mobile1 = item.Mobile1, Mobile2 = item.Mobile2 });
            }
        }

        public void FillEntity(Supplier entity)
        {
            entity.Code = Code;
            entity.NameAr = NameAr;
            entity.RegionId = RegionId;
            entity.Phone = Phone;
            entity.Mobile = Mobile;
            entity.Email = Email;
            entity.FaxNo = FaxNo;
            entity.CRNo = CRNo;
            entity.Address = Address;
            entity.BankName = BankName;
            entity.BankAccount = BankAccount;


            if (entity.Id > 0)
            {
                using (Entities C = new Entities())
                {
                    List<SupplierContactPerson> CustPersons = C.SupplierContactPersons.Where(p => p.SupplierId == entity.Id).ToList();
                    C.SupplierContactPersons.RemoveRange(CustPersons);

                    C.SaveChanges();
                }
            }
            foreach (var item in ContactPersons)
            {
                entity.SupplierContactPersons.Add(new SupplierContactPerson() { NameAr = item.Name, Title = item.Title, Email = item.Email, Mobile1 = item.Mobile1, Mobile2 = item.Mobile2 });
            }

            //entity.ModifiedBy = SessionRegistery.CurrentUser.ID;
            entity.UpdatedOn = DateTime.Now;

            if (entity.Id == 0)
            {
                //entity.CreatedBy = SessionRegistery.CurrentUser.Id;
                entity.CreatedOn = DateTime.Now;
            }
        }
    }

    public class SuppilerContactPersonModel
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string Email { get; set; }
    }
}