namespace UserStorageServices.Repositories.Abstract
{
    public interface INotificationSubscriber
    {
        void UserAdded(User user);

        void UserRemoved(User user);
    }
}
