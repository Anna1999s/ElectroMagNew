using DomainModel.Content;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Account
{
    public class Message: BaseEntity
    {
        public string SenderUserId { get; set; }
        public IdentityUser SenderUser { get; set; }

        public string RecipientUserId { get; set; }
        public IdentityUser RecipientUser { get; set; }

        public string Context { get; set; }
        public bool Read { get; set; }

        public IList<Attachment> Attachments { get; set; }
    }
}
