using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UserStorageServices.Enums;

namespace UserStorageServices.Notifications
{
    public class Notification
    {
        [XmlIgnore]
        public NotificationType Type { get; set; }

        [XmlElement("addUser", typeof(AddUserActionNotification))]
        [XmlElement("deleteUser", typeof(DeleteUserActionNotification))]
        [XmlChoiceIdentifier("Type")]
        public object Action { get; set; }
    }
}
