// See https://aka.ms/new-console-template for more information

using System.Text.Json;
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

// Check if there's any stored options
if (File.Exists(PathUtils.OptionsPath))
{
    try
    {
        overlay.YasmOptions = JsonSerializer.Deserialize<YasmOptions>(File.ReadAllText(PathUtils.OptionsPath), SerializationUtils.SerializerOptions)!;
    }
    catch (Exception)
    {
        // Ignore and let default options value
    }
}

// Load saves
overlay.CustomSavesManager.LoadCustomSaves();

// Initialization done, display overlay
await overlay.Run();
