using Newtonsoft.Json;

namespace WebStock.Models;

public class Notification
{
    public NotificationType Type { get; set; }
    public string Message { get; set; }

    public Notification(string message, NotificationType type = NotificationType.Info)
    {
        Message = message;
        Type = type;
    }

    public static string ToSerial(string message, 
        NotificationType type = NotificationType.Info) => 
            JsonConvert.SerializeObject(new Notification(message, type));

    public static Notification Unserialize(string message) =>
        JsonConvert.DeserializeObject<Notification>(message);
}