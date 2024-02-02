using GlobalHotKeys;
using YASM.NieRAutomata.SaveManager;
using YASM.NieRAutomata.Utils;

namespace YASM.NieRAutomata.Overlays;

public partial class MainOverlay
{
    public BaseSaveInfo? BaseSaveInfo { get; set; }
    public CustomSavesManager CustomSavesManager { get; } = new();
    public YasmOptions YasmOptions { get; set; } = new();

    private readonly HotKeyManager _hotKeyManager = new();

    private bool _focusManagerNext = false;

    private string _newSaveName = "";
    private string _newSaveGroup = "";
    private bool _isExporting;
}