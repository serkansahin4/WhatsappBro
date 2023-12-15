using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        public IActionResult Client()
        {
            return View();
        }
    }
}
