using System.Threading;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class Krumbs {

    [DllImport("user32.dll")]
    static extern bool SetWindowText(IntPtr hWnd, string text);

    [DllImport("user32.dll")]
    static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    // uint instead of long works https://stackoverflow.com/a/9855516
    [DllImport("user32.dll")]
    private static extern uint GetWindowLong(IntPtr hWnd, int nIndex);
    [DllImport("user32.dll", EntryPoint = "SetWindowLongPtrA")]
    private static extern uint SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);
    [DllImport("user32.dll")]
    public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int w, int h, bool repaint);
    [DllImport("user32.dll")]
    public static extern bool UpdateWindow(IntPtr hWnd);
    [DllImport("user32.dll")]
    public static extern bool EnableWindow(IntPtr hWnd, bool enable);

    // https://docs.microsoft.com/en-us/windows/win32/winmsg/window-styles
    private const uint WS_CAPTION = 0x00C00000;
    private const uint WS_SIZEBOX = 0x00040000;
    private const uint WS_BORDER = 0x00800000;
    private const uint WS_DISABLED = 0x08000000;




    // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowpos
    private const int SWP_NOSIZE = 0x0001;
    private const int SWP_NOZORDER = 0x0004;
    private const int SWP_SHOWWINDOW = 0x0040;

    private const String WOW_PATH = "D:\\Blizzard\\World of Warcraft\\_retail_";

    private bool debug = false; // launch notepad
    public Krumbs(){}
    public Krumbs(bool bDebug) {
        debug = bDebug;
    }


    public Process launch(String name, String config) {


        Process process = new Process();
        if (debug) {
            //process = Process.Start("notepad.exe");
            process.StartInfo.FileName = "notepad.exe";
        } else {
            //process = Process.Start($"{WOW_PATH}\\WoW.exe");
            process.StartInfo.FileName = $"{WOW_PATH}\\Wow.exe";
            process.StartInfo.Arguments = $"-config {config}.WTF";
            //process.StartInfo.Arguments = "-config krumbs.WTF";
            
        }
        process.Start();

        Console.WriteLine($"Strated");
        process.WaitForInputIdle();
        Thread.Sleep(2000); // is idle immediately, just hack a workaround
        
        Console.WriteLine($"Idle");

        SetWindowText(process.MainWindowHandle, name); //sets the caption

        Console.WriteLine(process.MainWindowHandle);

        return process;
    }

    public void removeBorder(Process process) {
        // remove border
        var GWL_STYLE = -16;
        uint style = GetWindowLong(process.MainWindowHandle, GWL_STYLE);
        style &= ~(WS_CAPTION | WS_SIZEBOX | WS_BORDER);
        //style &= WS_DISABLED;
        SetWindowLong(process.MainWindowHandle, GWL_STYLE, style);
        Console.WriteLine("DISABLED");
        //style &= ~WS_DISABLED;
        //Thread.Sleep(2000);
        //SetWindowLong(process.MainWindowHandle, GWL_STYLE, style);
        //EnableWindow(process.MainWindowHandle, true);
        Console.WriteLine("ENABLED");
    }

    public void position(Process process, int x, int y, int w, int h) {
        // position window
        Console.WriteLine($"Target Position: {x} {y} {w} {h}");
        RECT rct;
        GetWindowRect(process.MainWindowHandle, out rct);
        Console.WriteLine($"Current position {rct.Left} {rct.Top} {rct.Right} {rct.Bottom}");

        //System.Drawing.Rectangle screenRect = Screen.PrimaryScreen.WorkingArea;
        //Console.WriteLine($"Primary {screenRect.Left} {screenRect.Top} {screenRect.Right} {screenRect.Bottom}");
        //SetWindowPos(process.MainWindowHandle, IntPtr.Zero, x, y, w, h, SWP_NOZORDER | SWP_NOSIZE | SWP_SHOWWINDOW);
        MoveWindow(process.MainWindowHandle, x, y, w, h, true);
        Thread.Sleep(200);
        MoveWindow(process.MainWindowHandle, x, y, w, h, true);
        Thread.Sleep(200);
        MoveWindow(process.MainWindowHandle, x, y, w, h, true);
        Thread.Sleep(200);
        MoveWindow(process.MainWindowHandle, x, y, w, h, true);
        //MoveWindow(process.MainWindowHandle, rct.Left - 200, rct.Top, rct.Right - rct.Left, rct.Bottom - rct.Top, true);
        //Console.WriteLine($"Update {result}");
        //GetWindowRect(process.MainWindowHandle, out rct);
        //Console.WriteLine($"Current position {rct.Left} {rct.Top} {rct.Right} {rct.Bottom}");
        //result = UpdateWindow(process.MainWindowHandle);
        //Console.WriteLine($"Update {result}");
    }
}

public struct RECT
{
    public int Left;        // x position of upper-left corner
    public int Top;         // y position of upper-left corner
    public int Right;       // x position of lower-right corner
    public int Bottom;      // y position of lower-right corner
}
