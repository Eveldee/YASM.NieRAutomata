// See https://aka.ms/new-console-template for more information

using YASM.NieRAutomata.Overlays;
using YASM.NieRAutomata.SaveManager;
using YASM.NieRAutomata.Utils;


var overlay = new MainOverlay();

// Try load base save
if (SaveInfoManager.TryLoadFromSettings(out var saveInfo))
{
    overlay.BaseSaveInfo = saveInfo;
}

// Check if saves directory exists
if (!Directory.Exists(PathUtils.CustomSavesPath))
{
    Directory.CreateDirectory(PathUtils.CustomSavesPath);
}

// Load saves
overlay.CustomSavesManager.LoadCustomSaves();

// Initialization done, display overlay
await overlay.Run();
