using System;
using System.Xml.Serialization;

namespace UserStorageServices.Notifications.Concrete
{
    [Serializable]
    public class DeleteUserActionNotification
    {
        [XmlElement("userId")]
        public int UserId { get; set; }
    }
}
