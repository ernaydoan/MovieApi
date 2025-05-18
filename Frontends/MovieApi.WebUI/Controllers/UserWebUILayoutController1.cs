using Microsoft.AspNetCore.Mvc;

namespace MovieApi.WebUI.Controllers
{
    public class UserWebUILayoutController1 : Controller
    {
        public IActionResult LayoutUI()
        {
            return View();
        }
    }
}
