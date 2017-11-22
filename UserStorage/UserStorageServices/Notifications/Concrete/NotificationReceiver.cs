using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Notifications.Abstract;

namespace UserStorageServices.Notifications.Concrete
{
    public class NotificationReceiver : INotificationReceiver
    {
        public event Action<NotificationContainer> Received;

        public NotificationReceiver()
        {
            Received = c => { };
        }

        public void Receive(NotificationContainer container)
        {
            Received?.Invoke(container);
        }
    }
}
