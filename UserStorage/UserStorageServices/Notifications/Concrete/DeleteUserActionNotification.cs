using System.Xml.Serialization;

namespace UserStorageServices.Notifications.Concrete
{
    public class DeleteUserActionNotification
    {
        [XmlElement("userId")]
        public int UserId { get; set; }
    }
}
