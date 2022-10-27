using Microsoft.AspNetCore.Mvc;
using WebStock.Models;

namespace WebStock.Controllers;

public abstract class BaseController : Controller
{
    protected void SendNotification(string message, NotificationType type = NotificationType.Info)
    {
        TempData["notification"] = Notification
            .SendNotification(message, type);
    }
}
