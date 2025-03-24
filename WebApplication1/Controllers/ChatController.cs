using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication1.Service;

namespace MegaStore.Controllers
{
    public class ChatController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        public ChatController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index(string ReceiverId)
        {
            var user = await userManager.GetUserAsync(User);
            var Messages = context.Messages
                .Where(m => (m.SenderId == user.Id && m.ReceiverId == ReceiverId) || (m.SenderId == ReceiverId && m.ReceiverId == user.Id))
                .OrderBy(m => m.TimeStamp).ToList();

            ViewBag.ReceiverId = ReceiverId;

            return View("MessageView",Messages);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string receiverId, string content)
        {
            var sender = await userManager.GetUserAsync(User);

            var message = new Message
            {
                SenderId = sender.Id,
                ReceiverId = receiverId,
                Content = content
            };

            context.Messages.Add(message);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}