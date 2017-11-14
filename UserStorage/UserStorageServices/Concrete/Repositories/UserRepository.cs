using System;
using System.Collections.Generic;
using System.Linq;
using UserStorageServices.Abstract;

namespace UserStorageServices.Concrete.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected HashSet<User> Users;

        private readonly IUserIdGenerator _userIdGenerator;

        public UserRepository(IUserIdGenerator generator = null)
        {
            Users = new HashSet<User>();

            _userIdGenerator = generator ?? new GuidUserIdGenerator();
        }

        public int Count => Users.Count;

        public virtual void Start()
        {
            throw new NotImplementedException();
        }

        public virtual void Stop()
        {
            throw new NotImplementedException();
        }

        public User Get(Guid id)
        {
            return Users.FirstOrDefault(u => u.Id == id);
        }

        public void Set(User user)
        {
            var sourceUser = Get(user.Id);

            if (sourceUser == null)
            {
                user.Id = _userIdGenerator.Generate();
 
                Users.Add(user);
            }
            else
            {
                sourceUser.LastName = user.LastName;
                sourceUser.FirstName = user.FirstName;
                sourceUser.Age = user.Age;
            }
        }

        public bool Delete(Guid id)
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
