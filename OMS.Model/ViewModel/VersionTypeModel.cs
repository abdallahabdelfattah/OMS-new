using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Model.ViewModel
{
    public class VersionTypeModel: BaseModel
    {
        public VersionTypeModel()
        { }
        public VersionTypeModel(VersionType entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }

        public string Name { get; set; }

        public void FillEntity(VersionType entity)
        {
            entity.Id = Id;
            entity.Name = Name;
        }
    }
}
