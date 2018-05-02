using Microsoft.AspNetCore.Blazor.Browser.Interop;

namespace LightsOutPuzzle.Blazor.Client
{
    public class JsInterop
    {
        public static string Alert(string message)
        {
            return RegisteredFunction.Invoke<string>(
                "LightsOutPuzzle.Blazor.Client.JsInterop.Alert",
                message);
        }
    }
}
