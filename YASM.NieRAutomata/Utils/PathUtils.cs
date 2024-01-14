using System.Reflection;

namespace YASM.NieRAutomata.Utils;

public static class PathUtils
{
    public static readonly string BasePath = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!;

    public static readonly string GameSavePath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games", "NieR_Automata");

    public static string GetGameSaveSlotPath(int slotIndex) => slotIndex switch
    {
        0 or 1 or 2 =>  Path.Combine(GameSavePath, $"SlotData_{slotIndex}.dat"),
        _ => throw new ArgumentOutOfRangeException(nameof(slotIndex), "Slot index must be either 0, 1 or 2")
    };
}