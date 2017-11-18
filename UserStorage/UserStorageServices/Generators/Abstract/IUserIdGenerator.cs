namespace UserStorageServices.Generators.Abstract
{
    public interface IUserIdGenerator
    {
        /// <summary>
        /// Last generated id.
        /// </summary>
        int LastId { get; set; }

        /// <summary>
        /// Generates an id for user.
        /// </summary>
        int Generate();
    }
}
