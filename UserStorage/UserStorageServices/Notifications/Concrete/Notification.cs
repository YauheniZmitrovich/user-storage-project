using System;
using System.Xml.Serialization;
using UserStorageServices.Enums;

namespace UserStorageServices.Notifications.Concrete
{
    [Serializable]
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
