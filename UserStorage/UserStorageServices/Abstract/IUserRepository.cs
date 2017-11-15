using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.Abstract
{
    public interface IUserRepository
    {
        int Count { get; }

        void Start();

        void Stop();

        User Get(int id);

        void Set(User user);

        bool Delete(int id);

        IEnumerable<User> Query(Predicate<User> predicate);
    }
}
