using System.Xml.Serialization;

namespace UserStorageServices.Notifications.Concrete
{
    public class AddUserActionNotification
    {
        [XmlElement("user")]
        public User User { get; set; }
    }
}
