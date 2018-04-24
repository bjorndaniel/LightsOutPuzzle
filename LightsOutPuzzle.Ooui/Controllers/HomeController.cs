using Microsoft.AspNetCore.Mvc;
using Ooui.AspNetCore;
using Xamarin.Forms;

namespace LightsOutPuzzle.Ooui.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var page = new MainPage();
            var element = page.GetOouiElement();
            return new ElementResult(element, "Lights out puzzle");
        }
    }
}
