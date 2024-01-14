using System.Runtime.InteropServices;

namespace YASM.NieRAutomata.Utils;

public static partial class WindowUtils
{
    // Pinvoke declaration for ShowWindow
    public const int SW_SHOWMAXIMIZED = 3;

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [LibraryImport("user32.dll", EntryPoint = "FindWindowW", StringMarshalling = StringMarshalling.Utf16)]
    public static partial IntPtr FindWindow(string lpClassName, string lpWindowName);
}