using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UserStorageServices.Generators.Abstract;
using UserStorageServices.Generators.Concrete;
using UserStorageServices.Repositories.Abstract;

namespace UserStorageServices.Repositories.Concrete
{
    [Serializable]
    public class UserRepository : IUserRepository
    {
        protected ReaderWriterLockSlim _locker = new ReaderWriterLockSlim();

        public UserRepository(IUserIdGenerator generator = null)
        {
            Users = new HashSet<User>();

            IdGenerator = generator ?? new UserIdGenerator();
        }

        public int Count => Users.Count;

        protected HashSet<User> Users { get; set; }

        protected IUserIdGenerator IdGenerator { get; set; }

        public User Get(int id)
        {
            _locker.EnterReadLock();

            try
            {
                return Users.FirstOrDefault(u => u.Id == id);
            }
            finally
            {
                _locker.ExitReadLock();
            }
        }

        public void Set(User user)
        {
            _locker.EnterWriteLock();

            try
            {
                var sourceUser = Users.FirstOrDefault(u => u.Id == user.Id);

                if (sourceUser == null)
                {
                    user.Id = IdGenerator.Generate();

                    Users.Add(user);
                }
                else
                {
                    sourceUser.LastName = user.LastName;
                    sourceUser.FirstName = user.FirstName;
                    sourceUser.Age = user.Age;
                }
            }
            finally
            {
                _locker.ExitWriteLock();
            }
        }

        public bool Delete(int id)
        {
            _locker.EnterWriteLock();

            try
            {
                var sourceUser = Users.FirstOrDefault(u => u.Id == id); ;

                return Users.Remove(sourceUser);
            }
            finally
            {
                _locker.ExitWriteLock();
            }
        }

        public IEnumerable<User> Query(Predicate<User> comparer)
        {
            _locker.EnterReadLock();

            try
            {
                return Users.Select(u => u).Where(u => comparer(u));
            }
            finally
            {
                _locker.ExitReadLock();
            }
        }
    }
}
