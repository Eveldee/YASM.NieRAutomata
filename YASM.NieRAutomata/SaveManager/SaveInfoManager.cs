using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using YASM.NieRAutomata.Utils;

namespace YASM.NieRAutomata.SaveManager;

public static class SaveInfoManager
{
    public static readonly string BaseSaveInfoPath = Path.Combine(PathUtils.BasePath, "BaseSaveInfo.json");

    public const int InfoSteamIdOffset = 4;
    public const int InfoSteamIdLength = 4;

    public const int InfoSettingsOffset = 235340;
    public const int InfoSettingsLength = 264;

    public const int InfoControlsOffset = 235604;
    public const int InfoControlsLength = 172;

    private static readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
    {
        WriteIndented = true
    };

    public static bool TryLoadFromSettings([NotNullWhen(true)] out BaseSaveInfo? saveInfo)
    {
        if (!File.Exists(BaseSaveInfoPath))
        {
            saveInfo = null;
            return false;
        }

        saveInfo = JsonSerializer.Deserialize<BaseSaveInfo>(File.ReadAllText(BaseSaveInfoPath), _serializerOptions);
        return saveInfo is not null;
    }

    public static void SaveToSettings(BaseSaveInfo saveInfo)
    {
        File.WriteAllText(BaseSaveInfoPath, JsonSerializer.Serialize(saveInfo, _serializerOptions));
    }

    public static BaseSaveInfo ExtractFromSave(Span<byte> save)
    {
        var steamId = save[InfoSteamIdOffset..(InfoSteamIdOffset + InfoSteamIdLength)];
        var settings = save[InfoSettingsOffset..(InfoSettingsOffset + InfoSettingsLength)];
        var controls = save[InfoControlsOffset..(InfoControlsOffset + InfoControlsLength)];

        var saveInfo = new BaseSaveInfo(steamId, settings, controls);

        return saveInfo;
    }
    public static BaseSaveInfo ExtractFromSave(int saveSlot)
    {
        var data = File.ReadAllBytes(PathUtils.GetGameSaveSlotPath(saveSlot));

        return ExtractFromSave(data);
    }

    public static void InjectIntoSave(Span<byte> save, BaseSaveInfo saveInfo)
    {
        throw new NotImplementedException();
    }
}