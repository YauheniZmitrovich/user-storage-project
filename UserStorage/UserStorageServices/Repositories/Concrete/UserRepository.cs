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
        public UserRepository(IUserIdGenerator generator = null)
        {
            Users = new HashSet<User>();

            IdGenerator = generator ?? new UserIdGenerator();
        }

        public int Count => Users.Count;

        protected HashSet<User> Users { get; set; }

        protected IUserIdGenerator IdGenerator { get; set; }

        protected ReaderWriterLockSlim Locker { get; } = new ReaderWriterLockSlim();

        public User Get(int id)
        {
            Locker.EnterReadLock();

            try
            {
                return Users.FirstOrDefault(u => u.Id == id);
            }
            finally
            {
                Locker.ExitReadLock();
            }
        }

        public void Set(User user)
        {
            Locker.EnterWriteLock();

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
                Locker.ExitWriteLock();
            }
        }

        public bool Delete(int id)
        {
            Locker.EnterWriteLock();

            try
            {
                var sourceUser = Users.FirstOrDefault(u => u.Id == id);

                return Users.Remove(sourceUser);
            }
            finally
            {
                Locker.ExitWriteLock();
            }
        }

        public IEnumerable<User> Query(Predicate<User> comparer)
        {
            Locker.EnterReadLock();

            try
            {
                return Users.Select(u => u).Where(u => comparer(u));
            }
            finally
            {
                Locker.ExitReadLock();
            }
        }
    }
}
