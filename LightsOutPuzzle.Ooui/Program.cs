using System;
using Ooui;
namespace LightsOutPuzzle.OouiWasm
{
    class Program
    {
        static void Main(string[] args)
        {
            SetUpStyles();
            var cover = CreateCover();
            cover.AppendChild(CreatePuzzleGrid());
            cover.AppendChild(CreateFooter());
            UI.Publish("/", cover);
            Console.ReadLine();
        }

        private static Div CreateCover()
        {
            var cover = new Div
            {
                ClassName = "cover-container d-flex h-100 p-3 mx-auto flex-column"
            };

            var row = new Div
            {
                ClassName = "row"
            };
            var col = new Div
            {
                ClassName = "col-lg-12 text-center"
            };
            col.AppendChild(new Heading(3)
            {
                Text = "A Ooui wasm game based on 'Lights out' with a Marvel-twist"
            });
            row.AppendChild(col);
            cover.AppendChild(row);
            return cover;
        }

        private static Div CreatePuzzleGrid()
        {
            var row = new Div { ClassName = "row" };
            var col = new Div { ClassName = "col-lg-12" };
            col.AppendChild(new Image { Source = "img/row-1-col-1.jpg" });
            row.AppendChild(col);

            return row;
        }

        private static Div CreateFooter()
        {
            var footer = new Div
            {
                ClassName = "mastfoot mt-auto text-center"
            };
            var inner = new Div
            {
                ClassName = "inner"
            };
            var p = new Paragraph();
            p.AppendChild(new Anchor("https://github.com/bjorndaniel/lightsoutpuzzle")
            {
                Target = "_blank",
                Text = "Check out the code on github"
            });
            inner.AppendChild(p);
            footer.AppendChild(inner);
            return footer;
        }

        private static void SetUpStyles()
        {
            UI.Styles["body"] = new Style
            {
                BackgroundColor = "#333",
                Height = "100%",
                Display = "flex",
                Color = "#fff"
            };
            UI.Styles["body"]["background-image"] = "linear-gradient(#111,#555)";
            UI.Styles["body"]["justify-content"] = "center";
            UI.Styles["html"]["height"] = "100%";
            UI.Styles["cover-container"]["max-width"] = "55em";
            UI.Styles["cover-container"]["min-width"] = "55em";
            UI.Styles["cover-container"]["overflow-y"] = "scroll";
            UI.Styles["cover-container"]["scrollbar-width"] = "none";
            UI.Styles["a, a:focus, a:hover"]["color"] = "#fff";
            UI.Styles[".flip-container"] = new Ooui.Style
            {
                Perspective = "1000px !important",
                TransformStyle = "preserve-3d !important"
            };
            UI.Styles[" .flip .back"] = new Ooui.Style
            {
                Transform = "rotateY(0deg) !important"
            };
            UI.Styles[" .flip .front"] = new Ooui.Style
            {
                Transform = "rotateY(180deg) !important"
            };
            UI.Styles[".flip-container, .flip .front"] = new Ooui.Style
            {
                Width = "50px !important",
                Height = "50px !important"
            };
            UI.Styles[".flipper"] = new Ooui.Style
            {
                Transition = "0.6s !important",
                TransformStyle = "preserve-3d !important",
                Position = "relative !important"
            };
            UI.Styles[".front, .back"] = new Ooui.Style
            {
                BackfaceVisibility = "hidden !important",
                Transition = "0.6s !important",
                TransformStyle = "preserve-3d !important",
                Position = "absolute !important",
                Top = "0 !important",
                Left = "0 !important"
            };
            UI.Styles[".front"] = new Ooui.Style
            {
                ZIndex = "2 !important",

                Transform = "rotateY(0deg) !important"
            };
            UI.Styles[".back"] = new Ooui.Style
            {
                Transform = "rotateY(-180deg) !important"
            };
        }

    }
}
