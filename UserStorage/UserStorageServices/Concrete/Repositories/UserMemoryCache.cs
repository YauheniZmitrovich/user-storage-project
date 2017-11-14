using System;
using System.Collections.Generic;
using System.Linq;
using UserStorageServices.Abstract;

namespace UserStorageServices.Concrete.Repositories
{
    public class UserMemoryCache : IUserRepository
    {
        private readonly HashSet<User> _users;

        private readonly IUserIdGenerator _userIdGenerator;

        public UserMemoryCache(IUserIdGenerator generator = null)
        {
            _users = new HashSet<User>();

            _userIdGenerator = generator ?? new GuidUserIdGenerator();
        }

        public int Count => _users.Count;

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public User Get(Guid id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public void Set(User user)
        {
            var sourceUser = Get(user.Id);

            if (sourceUser == null)
            {
                user.Id = _userIdGenerator.Generate();
 
                _users.Add(user);
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

            return _users.Remove(sourceUser);
        }

        public IEnumerable<User> Query(Predicate<User> comparer)
        {
            return _users.Select(u => u).Where(u => comparer(u));
        }
    }
}
