using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.DAL.DataAccess;

namespace OMS.Model.ViewModel
{
    public class EmailSummerModel: BaseModel
    {
        public EmailSummerModel()
        {
            
        }
        public EmailSummerModel(Email entity)
        {
            Id = entity.Id;
            EmailBody = entity.EmailBody;
            EmailAddress= entity.EmailAddress;
            IsEmailSent= entity.IsEmailSent;
            AttachmentId = entity.AttachmentId;
            AttachmentId = entity.AttachmentId;

        }
        public long Id { get; set; }
        public string EmailBody { get; set; }
        public string EmailAddress { get; set; }
        public bool IsEmailSent { get; set; }
        public Nullable<long> AttachmentId { get; set; }
        
        public virtual Attachment Attachment { get; set; }
    }

    public class EmailViewModel : EmailSummerModel
    {
        public List<dynamic> EmailReceiversModel { get; set; }

        public EmailViewModel():base()
        {
            
        }
        public EmailViewModel(Email entity):base(entity:entity)
        {
        }
    }
}
