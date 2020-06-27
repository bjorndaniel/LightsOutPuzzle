using LightsOutPuzzle.Xam;
using LightsOutPuzzle.Xam.Views;
using Ooui;
using Xamarin.Forms;

namespace LightsOutPuzzle.OouiWasm
{
    class Program
    {
        static void Main(string[] args)
        {
            UI.Publish("/", CreateMainPage);
        }

        public static Ooui.Element CreateMainPage()
        {
            var page = new MainPage();
            return page.GetOouiElement();
        }

        //private static Page CreateMainPage()
        //{
        //    var page = new Page();
        //    var element = page.GetOouiElement();
        //    UI.Styles[".flip-container"] = new Ooui.Style
        //    {
        //        Perspective = "1000px !important",
        //        TransformStyle = "preserve-3d !important"
        //    };
        //    UI.Styles[" .flip .back"] = new Ooui.Style
        //    {
        //        Transform = "rotateY(0deg) !important"
        //    };
        //    UI.Styles[" .flip .front"] = new Ooui.Style
        //    {
        //        Transform = "rotateY(180deg) !important"
        //    };
        //    UI.Styles[".flip-container, .flip .front"] = new Ooui.Style
        //    {
        //        Width = "50px !important",
        //        Height = "50px !important"
        //    };
        //    UI.Styles[".flipper"] = new Ooui.Styles
        //    {
        //        Transition = "0.6s !important",
        //        TransformStyle = "preserve-3d !important",
        //        Position = "relative !important"
        //    };
        //    UI.Styles[".front, .back"] = new Ooui.Style
        //    {
        //        BackfaceVisibility = "hidden !important",
        //        Transition = "0.6s !important",
        //        TransformStyle = "preserve-3d !important",
        //        Position = "absolute !important",
        //        Top = "0 !important",
        //        Left = "0 !important"
        //    };
        //    UI.Styles[".front"] = new Ooui.Style
        //    {
        //        ZIndex = "2 !important",

        //        Transform = "rotateY(0deg) !important"
        //    };
        //    UI.Styles[".back"] = new Ooui.Style
        //    {
        //        Transform = "rotateY(-180deg) !important"
        //    };
        //    return element;
        //    //return new ElementResult(element, "Lights out puzzle");
        //}
    }
}
