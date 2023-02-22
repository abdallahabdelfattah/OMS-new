using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMS.Model.ViewModel
{
    public class CurrencySummaryModel : BaseModel
    {
        public CurrencySummaryModel()
        { }    
        public CurrencySummaryModel(Currency entity)
        {
            Id = entity.Id;
            NameAr = entity.NameAr;
            NameEn = entity.NameEn;
            ExchangeRate = entity.ExchangeRate??0;
            IsDefault = entity.IsDefault;
        }

        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public decimal ExchangeRate { get; set; }
        public bool IsDefault { get; set; }
    }
    public class CurrencyModel : CurrencySummaryModel
    {
        public CurrencyModel() : base()
        {

        }
        public CurrencyModel(Currency entity) : base(entity)
        {
            
        }
        public void FillEntity(Currency entity)
        {
            entity.NameAr = NameAr;
            entity.NameEn = NameEn;
            entity.ExchangeRate = ExchangeRate;
            entity.IsDefault = IsDefault;
        }
    }
}