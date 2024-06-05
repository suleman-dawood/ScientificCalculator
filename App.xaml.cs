#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif

namespace ScientificCalculator
{
    public partial class App : Application
    {
        const int WindowWidth = 550;
        const int WindowHeight = 980;

        public App()
        {
            InitializeComponent();

            // Append a mapping to the WindowHandler's Mapper to customize the window behavior
            Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
            {
#if WINDOWS
            var mauiWindow = handler.VirtualView; // Get the MAUI (Microsoft .NET Multi-platform App UI) virtual window
            var nativeWindow = handler.PlatformView; // Get the native platform window
            nativeWindow.Activate(); // Activate the native window
            IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow); // Retrieve the window handle (HWND) for the native window
            WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle); // Get the WindowId for the native window using the window handle
            AppWindow appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId); // Retrieve the AppWindow from the WindowId
            appWindow.Resize(new SizeInt32(WindowWidth, WindowHeight)); // Resize the AppWindow to the specified dimensions
#endif
            });

            MainPage = new MainPage();
        }
    }
}
