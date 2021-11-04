using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NoteShared.Services.Interfaces;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace NoteShared.Api.Controllers
{
    [Route("api/notifications")]
    [Authorize(LocalApi.PolicyName)]
    [ApiController]
    public class NotificationsController : BaseController
    {
        private readonly INotificationsService _notificationService;

        private readonly ILogger<NotificationsController> _logger;

        public NotificationsController(
            ILogger<NotificationsController> logger,
            INotificationsService notificationService
        )
        {
            _logger = logger;
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _notificationService.GetNotifications(LoggedInUserUserId);
            return ResultOf(result);
        }

        [Route("delete-notification")]
        [HttpDelete]
        public async Task<IActionResult> DeleteNotification(int notificationID)
        {
            var result = await _notificationService.DeleteNotification(LoggedInUserUserId, notificationID);
            return ResultOf(result);
        }
    }
}
