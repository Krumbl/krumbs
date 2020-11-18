using System.Drawing;
using System;
using System.Runtime.InteropServices;
using System.Diagnostics;


// https://stackoverflow.com/questions/15988917/how-can-i-set-the-window-text-of-an-application-using-net-process-start
public class Launch {

    [DllImport("user32.dll")]
    static extern IntPtr FindWindow(string windowClass, string windowName);
    [DllImport("user32.dll", EntryPoint="FindWindow", SetLastError = true)]
    static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);
    [DllImport("user32.dll")]
    static extern bool SetWindowText(IntPtr hWnd, string text);
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPTStr)] string lParam);
    [DllImport("user32.dll")]
    static extern bool GetWindowLong(IntPtr hWnd, int text);
    [DllImport("user32.dll")]
    static extern bool SetWindowLong(IntPtr hWnd, int nIndex, long dwNewLong);
    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int  X, int  Y, int  cx, int  cy, int uFlags);

// https://stackoverflow.com/questions/4717667/how-do-i-find-all-windows-using-c
    public void launch() {
        // IntPtr handle = FindWindow("ConsoleWindowClass", path); //get the Handle of the 
        // SetWindowText(handle, title); //sets the caption

        Console.WriteLine("Hello World!");

        // launch
        // Process.WaitForInputIdle
        // rename

        Process[] processes = Process.GetProcessesByName("Wow");

        foreach (Process p in processes) {
            Console.WriteLine(p.MainWindowTitle + " - " +  p.Id);
            // SetWindowText(p.MainWindowHandle, "WOW" + p.Id);
            // SetWindowText(Process.GetCurrentProcess().MainWindowHandle, "WOW" + p.Id);
            // send message because we don't own wow process
            // SendMessage(p, 0x000C, IntPtr.Zero, "wow - " + p.Id);
        }


        IntPtr handle = FindWindow("World of Warcraft - Master", "");

        
        Console.WriteLine("Goodbye World!");
    }

        private const int WS_BORDER = 0x00800000;       //window with border
        private const int WS_CAPTION = 0x00C00000;      //window with a title bar
        private const int WS_SYSMENU = 0x00080000;      //window with no borders etc.
        private const int WS_MINIMIZEBOX = 0x00020000;  //window with minimizebox

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;        // x position of upper-left corner
        public int Top;         // y position of upper-left corner
        public int Right;       // x position of lower-right corner
        public int Bottom;      // y position of lower-right corner
    }
        private const int SWP_NOSIZE = 0x0001;
    private const int SWP_NOZORDER = 0x0004;
    private const int SWP_SHOWWINDOW = 0x0040;

    public void start() {
        // https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.process.start?redirectedfrom=MSDN&view=net-5.0#System_Diagnostics_Process_Start_System_String_System_String_
        var process = Process.Start("notepad.exe");
        process.WaitForInputIdle();

        Console.WriteLine(process.MainWindowHandle);

        var GWL_STYLE = -16;
        var style = GetWindowLong(process.MainWindowHandle, GWL_STYLE);
        // https://docs.microsoft.com/en-us/windows/win32/winmsg/window-styles
        // SetWindowLong(process.MainWindowHandle, GWL_STYLE, 0xC40000); // 0xC40000 (no caption no frame)
        // SetWindowLong(process.MainWindowHandle, GWL_STYLE, WS_BORDER);

        // SetWindowPos(window, -2, 0, 0, 800, 600, 0x0040);

        // https://stackoverflow.com/questions/31271828/how-do-i-use-setwindowpos-to-center-a-window
        RECT rct;
        GetWindowRect(process.MainWindowHandle, out rct);
        SetWindowPos(process.MainWindowHandle, IntPtr.Zero, 0, 0, 1920, 1080, SWP_NOZORDER | SWP_NOSIZE | SWP_SHOWWINDOW);
    }
}