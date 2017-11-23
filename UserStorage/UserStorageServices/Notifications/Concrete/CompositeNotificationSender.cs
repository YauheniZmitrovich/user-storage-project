using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Notifications.Abstract;

namespace UserStorageServices.Notifications.Concrete
{
    public class CompositeNotificationSender : INotificationSender
    {
        private readonly List<INotificationSender> _senders;

        public CompositeNotificationSender(List<INotificationSender> senders = null)
        {
            _senders = senders ?? new List<INotificationSender>();
        }

        public void Send(NotificationContainer container)
        {
            foreach (var item in _senders)
            {
                item?.Send(container);
            }
        }

        public void AddReceiver(INotificationReceiver receiver)
        {
            if (receiver == null)
            {
                throw new ArgumentNullException(nameof(receiver));
            }

            var sender = new NotificationSender(receiver);

            _senders.Add(sender);
        }
    }
}
