using YASM.NieRAutomata.SaveManager;

namespace YASM.NieRAutomata.Overlays;

public partial class MainOverlay
{
    public BaseSaveInfo? BaseSaveInfo { get; set; }

    private bool _focusManagerNext = false;
}