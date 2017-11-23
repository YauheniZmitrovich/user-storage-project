using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UserStorageServices.Notifications.Abstract;

namespace UserStorageServices.Notifications.Concrete
{
    public class NotificationSender : INotificationSender
    {
        public INotificationReceiver Receiver { get; set; }

        public NotificationSender(INotificationReceiver receiver = null)
        {
            Receiver = receiver ?? new NotificationReceiver();
        }

        public void Send(NotificationContainer container)
        {
            using (var stringWriter = new StringWriter())
            {
                var serializer = new XmlSerializer(typeof(NotificationContainer));

                serializer.Serialize(stringWriter, container);

                Receiver.Receive(stringWriter.ToString());
            }
        }
    }
}
