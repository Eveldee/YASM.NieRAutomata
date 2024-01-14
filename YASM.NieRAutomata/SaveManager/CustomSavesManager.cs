using YASM.NieRAutomata.Utils;

namespace YASM.NieRAutomata.SaveManager;

public class CustomSavesManager
{
    public const long SaveLength = 235980;

    public IEnumerable<CustomSaveInfo> CustomSaveInfos => _customSaveInfos;
    public IEnumerable<IGrouping<string, CustomSaveInfo>> SaveGroups => CustomSaveInfos
                                                                            .OrderBy(save => save.Name)
                                                                            .GroupBy(save => save.Group)
                                                                            .OrderBy(group => group.Key);

    public int SavesCount => _customSaveInfos.Count;

    private readonly List<CustomSaveInfo> _customSaveInfos = [];

    public void LoadCustomSaves()
    {
        _customSaveInfos.Clear();

        // Each directory is a group
        var baseDirectoryInfo = new DirectoryInfo(PathUtils.CustomSavesPath);

        LoadGroup(baseDirectoryInfo, "|Default");

        foreach (var directoryInfo in baseDirectoryInfo.EnumerateDirectories())
        {
            LoadGroup(directoryInfo, directoryInfo.Name);
        }
    }

    private void LoadGroup(DirectoryInfo directoryInfo, string groupName)
    {
        // Filter only valid saves
        foreach (var fileInfo in directoryInfo.EnumerateFiles().Where(f => f.Length == SaveLength))
        {
            _customSaveInfos.Add(new CustomSaveInfo(groupName, fileInfo.Name.Replace("_fog", "")[..^4], fileInfo.FullName));
        }
    }

    public void Load(CustomSaveInfo customSave, int slotIndex, BaseSaveInfo baseSaveInfo)
    {
        // Read custom save and inject needed data
        var data = File.ReadAllBytes(customSave.SavePath);

        Array.Copy(baseSaveInfo.SteamId, 0, data, SaveInfoManager.InfoSteamIdOffset, SaveInfoManager.InfoSteamIdLength);
        Array.Copy(baseSaveInfo.Settings, 0, data, SaveInfoManager.InfoSettingsOffset, SaveInfoManager.InfoSettingsLength);
        Array.Copy(baseSaveInfo.Controls, 0, data, SaveInfoManager.InfoControlsOffset, SaveInfoManager.InfoControlsLength);

        // Override existing save
        File.WriteAllBytes(PathUtils.GetGameSaveSlotPath(slotIndex), data);
    }

    public void CreateSave(string newSaveName, string newSaveGroup, int slotIndex)
    {
        if (string.IsNullOrWhiteSpace(newSaveName) || string.IsNullOrEmpty(newSaveGroup))
        {
            return;
        }

        var gameSavePath = PathUtils.GetGameSaveSlotPath(slotIndex);

        if (!File.Exists(gameSavePath))
        {
            return;
        }

        var targetDirectory = Path.Combine(PathUtils.CustomSavesPath, newSaveGroup);

        if (!Directory.Exists(targetDirectory))
        {
            Directory.CreateDirectory(targetDirectory);
        }

        File.Copy(gameSavePath, Path.Combine(targetDirectory, $"{newSaveName}.dat"), true);

        LoadCustomSaves();
    }
}