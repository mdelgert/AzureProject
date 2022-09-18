error fix - android-arm64\staticwebassets.build.json not found

https://github.com/dotnet/maui/issues/10102

rename C:\Program Files\dotnet\sdk\6.0.401 to C:\Program Files\dotnet\sdk\BAK_6.0.401

Download and install

https://download.visualstudio.microsoft.com/download/pr/9a1d2e89-d785-4493-aaf3-73864627a1ea/245bdfaa9c46b87acfb2afbd10ecb0d0/dotnet-sdk-6.0.400-win-x64.exe

# Samples
https://github.com/dotnet/maui-samples
https://github.com/dotnet/maui

https://github.com/dotnet/maui/discussions/2370

# App.xaml.cs

#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif

namespace Calculator;

public partial class App : Application
{
    const int WindowWidth = 400;
    const int WindowHeight = 600;

    public App()
	{
		InitializeComponent();

        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
        {
#if WINDOWS
            var mauiWindow = handler.VirtualView;
            var nativeWindow = handler.PlatformView;
            nativeWindow.Activate();
            IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
            WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
            AppWindow appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
            appWindow.Resize(new SizeInt32(WindowWidth, WindowHeight));
#endif
        });

        MainPage = new MainPage();
	}
}