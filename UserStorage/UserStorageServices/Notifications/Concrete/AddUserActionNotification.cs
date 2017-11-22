using System.Xml.Serialization;

namespace UserStorageServices.Notifications.Concrete
{
    class AddUserActionNotification
    {
        [XmlElement("user")]
        public User User { get; set; }
    }
}
