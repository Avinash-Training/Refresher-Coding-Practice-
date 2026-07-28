using System;

interface INotification
{
    void Send(string message);
}

class EmailNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine("Email: " + message);
    }
}
class SmsNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine("Sms: " + message);
    }
}


class PushNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine("PushNotification: " + message);
    }
}

class NotificationService
{
    private INotification notification;
    public NotificationService(INotification notification)
    {
        this.notification = notification;
    }

    public void Notify(string message)
    {
        notification.Send(message);
    }
}

class Program
{
    static void Main(string[] args)
    {
        INotification notification = new EmailNotification();

        NotificationService service = new NotificationService(notification);
        service.Notify("Order Placed Sucessfully");

        
    }
}