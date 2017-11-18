using System;
using System.Collections.Generic;

namespace UserStorageServices.Repositories.Abstract
{
    public interface IUserRepository
    {
        int Count { get; }

        User Get(int id);

        void Set(User user);

        bool Delete(int id);

        IEnumerable<User> Query(Predicate<User> predicate);
    }
}
