using DomainModel.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Content
{
    public class Attachment:BaseEntity
    {        
        public string FileBase64 { get; set; }
        public int? MessageId { get; set; }
        public Message Message { get; set; }
    }
}
