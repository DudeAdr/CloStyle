using CloStyle.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Text.Json;

namespace CloStyle.Extensions
{
    public static class ControllerExtensions
    {
        public static void AddNotification(this Controller controller, string type, string message)
        {
            var notification = new Notification(type, message);
            controller.TempData["Notification"] = JsonSerializer.Serialize(notification);
        }
    }
}
