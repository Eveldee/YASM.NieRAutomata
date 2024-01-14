using YASM.NieRAutomata.SaveManager;

namespace YASM.NieRAutomata.Overlays;

public partial class MainOverlay
{
    public BaseSaveInfo? BaseSaveInfo { get; set; }
    public CustomSavesManager CustomSavesManager { get; } = new();

    private bool _focusManagerNext = false;

    private string _newSaveName = "";
    private string _newSaveGroup = "";
    private bool _isExporting;
}