using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OMS.Model.ViewModel
{
    public class DoctorSearchViewModel : BaseSearchViewModel
    {
            public string Mobile { get; set; }
            public string Phone { get; set; }
    }
}