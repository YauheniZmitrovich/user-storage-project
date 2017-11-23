using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Notifications.Concrete;

namespace UserStorageServices.Notifications.Abstract
{
    public interface INotificationSender
    {
        void Send(NotificationContainer container);

        void AddReceiver(INotificationReceiver receiver);
    }
}
