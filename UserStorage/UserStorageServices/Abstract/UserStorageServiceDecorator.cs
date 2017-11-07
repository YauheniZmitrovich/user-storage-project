using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.Abstract
{
    public abstract class UserStorageServiceDecorator : IUserStorageService
    {
        protected readonly IUserStorageService StorageService;

        protected UserStorageServiceDecorator(IUserStorageService storageService)
        {
            StorageService = storageService;
        }

        public abstract int Count { get; }

        public abstract void Add(User user);

        public abstract void Add(string firstName, string lastName, int age);

        public abstract void Remove(Guid id);

        public abstract void Remove(User user);

        public abstract IEnumerable<User> SearchByFirstName(string name);

        public abstract IEnumerable<User> SearchByLastName(string name);

        public abstract IEnumerable<User> SearchByAge(int age);

        public abstract IEnumerable<User> Search(Predicate<User> comparer);

        public abstract User FindFirstByFirstName(string name);

        public abstract User FindFirstByLastName(string name);

        public abstract User FindFirstByAge(int age);

        public abstract User FindFirst(Predicate<User> comparer);
    }
}
