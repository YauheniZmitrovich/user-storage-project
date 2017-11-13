using System;
using System.Collections.Generic;
using System.Linq;
using UserStorageServices.Abstract;
using UserStorageServices.Enums;

namespace UserStorageServices.Concrete
{
    public class UserStorageServiceMaster : UserStorageServiceBase
    {
        #region Fields

        /// <summary>
        /// Services with slave mode.
        /// </summary>
        private readonly IList<IUserStorageService> _slaveServices;

        /// <summary>
        /// Collection of subcribers.
        /// </summary>
        private readonly IList<INotificationSubscriber> _subscribers;

        #endregion

        #region Constructors and properties

        /// <summary>
        /// Create an instance of <see cref="UserStorageServiceMaster"/>. 
        /// </summary>
        public UserStorageServiceMaster(
            IUserIdGenerator idGenerator = null,
            IUserValidator validator = null,
            IEnumerable<IUserStorageService> slaveServices = null)
            : base(idGenerator, validator)
        {
            _slaveServices = slaveServices?.ToList() ?? new List<IUserStorageService>();

            _subscribers = new List<INotificationSubscriber>();
        }

        /// <summary>
        /// Mode of <see cref="UserStorageServiceMaster"/> work. 
        /// </summary>
        public override UserStorageServiceMode Mode => UserStorageServiceMode.MasterNode;

        #endregion

        #region Public methods

        #region Add

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public override void Add(User user)
        {
            base.Add(user);

            foreach (var ob in _subscribers)
            {
                ob.UserAdded(user);
            }

            foreach (var service in _slaveServices)
            {
                service.Add(user);
            }
        }

        #endregion

        #region Remove

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage by id.
        /// </summary>
        public override void Remove(Guid id)
        {
            base.Remove(id);

            foreach (var ob in _subscribers)
            {
                ob.UserRemoved(Users.First(x => x.Id == id));
            }

            foreach (var service in _slaveServices)
            {
                service.Remove(id);
            }
        }

        #endregion

        #region Subscriber magement

        public void AddSubscriber(INotificationSubscriber subscriber)
        {
            if (subscriber == null)
            {
                throw new ArgumentNullException(nameof(subscriber));
            }
            _subscribers.Add(subscriber);
        }

        public void RemoveSubscriber(INotificationSubscriber subscriber)
        {
            if (subscriber == null)
            {
                throw new ArgumentNullException(nameof(subscriber));
            }
            _subscribers.Remove(subscriber);
        }

        #endregion

        #endregion
    }
}
