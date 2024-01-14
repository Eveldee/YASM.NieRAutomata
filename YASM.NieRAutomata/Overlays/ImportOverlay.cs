using System.Globalization;
using Humanizer;
using ImGuiNET;
using YASM.NieRAutomata.SaveManager;
using YASM.NieRAutomata.Utils;

namespace YASM.NieRAutomata.Overlays;

public partial class MainOverlay
{
    private void DisplayImport()
    {
        ImGui.PushTextWrapPos();
        ImGui.Text("""
                   For the save manager to work, you need to import various information (id, settings, controls, ...) from one of your game saves.

                   Please pick one game save to fetch those information.
                   """);

        ImGui.NewLine();
        ImGui.TextColored(WarningColor, "Don't forget to import again from an updated save if you ever change your settings and controls.");
        ImGui.PopTextWrapPos();

        ImGui.NewLine();
        ImGui.SeparatorText("Saves");

        for (int i = 0; i < 3; i++)
        {
            var savePath = PathUtils.GetGameSaveSlotPath(i);

            if (File.Exists(savePath))
            {
                var fileInfo = new FileInfo(savePath);

                if (ButtonColored(TitleColor, "Import"))
                {
                    BaseSaveInfo = SaveInfoManager.ExtractFromSave(i);

                    // Write those info in a file to reuse later
                    SaveInfoManager.SaveToSettings(BaseSaveInfo);

                    _focusManagerNext = true;
                }

                ImGui.SameLine();
                ImGui.TextColored(InfoColor, Path.GetFileName(savePath));

                ImGui.SameLine();
                ImGui.TextColored(ValueColor, $"(last modified: {fileInfo.LastWriteTime.Humanize()})");
            }
        }
    }
}