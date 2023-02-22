using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMS.Model.ViewModel
{
    public class MenuModel
    {
        public long ID { get; set; }
        private string _Name;
        public string URL { get; set; }
        public List<MenuModel> Children { get; set; }
        public string Icon { get; set; }
        public long? Sequence { get; set; }

        public string Name
        {
            get
            {return _Name;}
            set
            {_Name = value;}
        }

        public MenuModel()
        {
            Children = new List<MenuModel>();
        }
    }
}