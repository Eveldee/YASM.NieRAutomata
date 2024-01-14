// See https://aka.ms/new-console-template for more information

using YASM.NieRAutomata.Overlays;
using YASM.NieRAutomata.SaveManager;


var overlay = new MainOverlay();

// Try load base save
if (SaveInfoManager.TryLoadFromSettings(out var saveInfo))
{
    overlay.BaseSaveInfo = saveInfo;
}

await overlay.Run();
