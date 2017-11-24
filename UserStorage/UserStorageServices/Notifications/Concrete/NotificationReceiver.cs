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
    [Serializable]
    public class NotificationReceiver : INotificationReceiver
    {
        public NotificationReceiver()
        {
            Received = c => { };
        }

        public event Action<NotificationContainer> Received;

        public void Receive(string msg)
        {
            using (var stringReader = new StringReader(msg))
            {
                var serializer = new XmlSerializer(typeof(NotificationContainer));

                var container = (NotificationContainer)serializer.Deserialize(stringReader);

                Received?.Invoke(container);
            }
        }
    }
}
