using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Model.ViewModel
{
    public class LenseIndexModel : BaseModel
    {
        public LenseIndexModel()
        { }
        public LenseIndexModel(LenseIndex entity)
        {
            Id = entity.Id;
            Index = entity.Index;
        }

        public double? Index { get; set; }

        public void FillEntity(LenseIndex entity)
        {
            if (entity == null)
            {
                entity = new LenseIndex();
            }
            entity.Id = Id;
            entity.Index = Index;
        }
    }
}
