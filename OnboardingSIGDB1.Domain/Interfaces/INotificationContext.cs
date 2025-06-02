using System.Collections.Generic;
using FluentValidation.Results;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Domain.Interfaces;

public interface INotificationContext
{
    IReadOnlyCollection<Notification> Notifications { get; }
    bool HasNotifications { get; }

    void AddNotification(string key, string message);
    void AddNotification(Notification notification);
    void AddNotifications(IReadOnlyCollection<Notification> notifications);
    void AddNotifications(IList<Notification> notifications);
    void AddNotifications(ICollection<Notification> notifications);
    void AddNotifications(ValidationResult validationResult);
}