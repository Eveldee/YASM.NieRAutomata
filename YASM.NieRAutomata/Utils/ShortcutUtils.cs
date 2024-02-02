using GlobalHotKeys.Native.Types;

namespace YASM.NieRAutomata.Utils;

public static class ShortcutUtils
{
    public static string[] KeyNames { get; } = Enum.GetNames<VirtualKeyCode>();
}