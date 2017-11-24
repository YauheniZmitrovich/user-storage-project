using System;
using System.Configuration;
using System.Reflection;
using System.Web.UI;
using System.Linq;
using ServiceConfigurationSection;
using UserStorageServices.Generators.Abstract;
using UserStorageServices.Repositories.Abstract;
using UserStorageServices.SerializationStrategies.Abstract;
using UserStorageServices.Services.Abstract;
using UserStorageServices.Validators.Abstract;

namespace UserStorageServices.Services.Concrete
{
    public static class DomainFactoryService
    {
        private static int _count;

        public static UserStorageServiceSlave CreateSlave(
            IUserRepository repository = null,
            IUserSerializationStrategy strategy = null,
            string filePath = null,
            IUserIdGenerator generationService = null) =>
            CreateDomain<UserStorageServiceSlave>(repository, strategy, filePath, generationService);

        public static UserStorageServiceMaster CreateMaster(
            IUserRepository repository = null,
            IUserSerializationStrategy strategy = null,
            string filePath = null,
            IUserIdGenerator generationService = null) =>
            CreateDomain<UserStorageServiceMaster>(repository, strategy, filePath, generationService);

        public static UserStorageServiceMaster DefaultCreation(
            ServiceConfigurationSection.ServiceConfigurationSection configurationSection,
            IUserRepository repository = null, 
            IUserSerializationStrategy strategy = null,
            string filePath = null, 
            IUserIdGenerator generationService = null, 
            IValidator validator = null)
        {
            if (configurationSection.ServiceInstances.Count(i => i.Mode == ServiceInstanceMode.Master) != 1)
            {
                throw new ConfigurationErrorsException("It should be one MasterService.");
            }

            var master = CreateMaster(repository, strategy, filePath, generationService);

            foreach (var item in configurationSection.ServiceInstances)
            {
                if (item.Mode == ServiceInstanceMode.Slave)
                {
                    master.Sender.AddReceiver(CreateSlave().Receiver);
                }
            }

            return master;
        }

        private static T CreateDomain<T>(
            IUserRepository repository = null,
            IUserSerializationStrategy strategy = null,
            string filePath = null,
            IUserIdGenerator generationService = null,
            IUserValidator validator = null) where T : IUserStorageService
        {
            var newDomain = AppDomain.CreateDomain(
                "AppDomain" + _count++,
                null,
                new AppDomainSetup { ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase });

            return (T)newDomain.CreateInstanceAndUnwrap(
                typeof(T).Assembly.FullName,
                typeof(T).FullName,
                false,
                BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance,
                null,
                typeof(T) == typeof(UserStorageServiceMaster) ? new object[] { repository, validator, null } : new object[] { repository, validator },
                null,
                null);
        }
    }
}
