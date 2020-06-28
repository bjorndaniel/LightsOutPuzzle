using Windows.Foundation;
using Windows.UI.ViewManagement;

namespace LightsOutPuzzle.Fabulous.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            LoadApplication(new LightsOutPuzzle.Fabulous.App());
        }
    }
}
