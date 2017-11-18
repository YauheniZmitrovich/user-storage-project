using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UserStorageServices.Notifications
{
    class DeleteUserActionNotification
    {
        [XmlElement("userId")]
        public int UserId { get; set; }
    }
}
