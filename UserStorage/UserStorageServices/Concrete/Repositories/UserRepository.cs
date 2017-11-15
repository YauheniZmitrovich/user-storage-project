using System;
using System.Collections.Generic;
using System.Linq;
using UserStorageServices.Abstract;

namespace UserStorageServices.Concrete.Repositories
{
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

        public virtual void Start()
        {
            throw new NotImplementedException();
        }

        public virtual void Stop()
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            return Users.FirstOrDefault(u => u.Id == id);
        }

        public void Set(User user)
        {
            var sourceUser = Get(user.Id);

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

        public bool Delete(int id)
        {
            var sourceUser = Get(id);

            return Users.Remove(sourceUser);
        }

        public IEnumerable<User> Query(Predicate<User> comparer)
        {
            return Users.Select(u => u).Where(u => comparer(u));
        }
    }
}
