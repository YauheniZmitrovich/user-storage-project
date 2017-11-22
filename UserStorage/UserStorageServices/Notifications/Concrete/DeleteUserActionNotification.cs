using System.Xml.Serialization;

namespace UserStorageServices.Notifications.Concrete
{
    class DeleteUserActionNotification
    {
        [XmlElement("userId")]
        public int UserId { get; set; }
    }
}
