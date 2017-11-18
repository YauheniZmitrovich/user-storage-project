namespace UserStorageServices.Services.Abstract
{
    public interface INotificationSubscriber
    {
        void UserAdded(User user);

        void UserRemoved(User user);
    }
}
