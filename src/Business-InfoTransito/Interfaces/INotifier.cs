using Business_InfoTransito.Notifications;

namespace Business_InfoTransito.Interfaces;

public interface INotifier
{
    bool HasNotification();
    List<Notification> GetNotifications();
    void Handle(Notification notification);
}
