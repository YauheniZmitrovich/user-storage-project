using System;
using System.Xml.Serialization;

namespace UserStorageServices.Notifications.Concrete
{
    [Serializable]
    public class AddUserActionNotification
    {
        [XmlElement("user")]
        public User User { get; set; }
    }
}
