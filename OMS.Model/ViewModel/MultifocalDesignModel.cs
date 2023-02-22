using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Model.ViewModel
{
    public class MultifocalDesignModel : BaseModel
    {
        public MultifocalDesignModel()
        { }
        public MultifocalDesignModel(MultifocalDesign entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }

        public string Name { get; }

        public void FillEntity(MultifocalDesign entity)
        {
            entity.Id = Id;
            entity.Name = Name;
        }
    }
}
