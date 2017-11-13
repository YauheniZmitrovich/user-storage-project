using System;
using System.Diagnostics;
using System.Linq;
using UserStorageServices.Abstract;
using UserStorageServices.Enums;

namespace UserStorageServices.Concrete.Services
{
    public sealed class UserStorageServiceSlave : UserStorageServiceBase
    {
        #region Constructors and properties

        /// <summary>
        /// Create an instance of <see cref="UserStorageServiceSlave"/>. 
        /// </summary>
        public UserStorageServiceSlave(IUserIdGenerator idGenerator = null, IUserValidator validator = null)
            : base(idGenerator, validator) { }

        /// <summary>
        /// Mode of <see cref="UserStorageServiceSlave"/> work. 
        /// </summary>
        public override UserStorageServiceMode Mode => UserStorageServiceMode.SlaveNode;

        #endregion

        #region Public methods

        #region Add

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public override void Add(User user)
        {
            if (IsAvailable())
            {
                base.Add(user);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        #endregion

        #region Remove

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage by id.
        /// </summary>
        public override void Remove(Guid id)
        {
            if (IsAvailable())
            {
                base.Remove(id);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        #endregion

        #endregion

        #region Private methods

        /// <summary>
        /// Check is method called from masterNode by reflection.
        /// </summary>
        private bool IsAvailable()
        {
            var trace = new StackTrace();

            var currentCalled = trace.GetFrame(1).GetMethod();
            var currentCalledParams = currentCalled.GetParameters();
            Type[] parTypes = new Type[currentCalledParams.Length];
            for (int i = 0; i < parTypes.Length; i++)
            {
                parTypes[i] = currentCalledParams[i].ParameterType;
            }

            var calledMethod = typeof(UserStorageServiceMaster).GetMethod(currentCalled.Name, parTypes);

            var flag = (trace.GetFrames() ?? throw new InvalidOperationException()).Select(x => x.GetMethod())
                .Contains(calledMethod);

            return flag; 
        }

        #endregion
    }
}
