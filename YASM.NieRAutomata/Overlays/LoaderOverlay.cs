using System.Numerics;
using ImGuiNET;

namespace YASM.NieRAutomata.Overlays;

public partial class MainOverlay
{
    private void DisplayManager()
    {
        ImGui.PushTextWrapPos();

        ImGui.TextColored(InfoColor, "There are");
        ImGui.SameLine();
        ImGui.TextColored(ValueColor, $"{CustomSavesManager.SavesCount}");
        ImGui.SameLine();
        ImGui.TextColored(InfoColor, "saves available.");

        ImGui.Separator();

        ImGui.TextColored(WarningColor, "Remember that you need to create a save in each save slot before being able to use them. If you load a save and it's not visible in game, first create a save in the slot and then retry.");

        ImGui.Separator();
        ImGui.NewLine();

        if (ImGui.Button("Refresh Saves"))
        {
            CustomSavesManager.LoadCustomSaves();
        }

        ImGui.NewLine();
        ImGui.SeparatorText("Saves");

        TabBar("SavesLoadTabBar", () =>
        {
            foreach (var saveGroup in CustomSavesManager.SaveGroups)
            {
                TabItem(saveGroup.Key, () =>
                {
                    Child($"##{saveGroup.Key}", new Vector2(-1, 400), ImGuiChildFlags.Border, () =>
                    {
                        foreach (var customSave in saveGroup)
                        {
                            if (ButtonColored(InfoColorSecondary, $"Slot 0##{customSave.SavePath}"))
                            {
                                CustomSavesManager.Load(customSave, 0, BaseSaveInfo!);
                            }
                            ImGui.SameLine();
                            if (ButtonColored(WarningColor, $"Slot 1##{customSave.SavePath}"))
                            {
                                CustomSavesManager.Load(customSave, 1, BaseSaveInfo!);
                            }
                            ImGui.SameLine();
                            if (ButtonColored(ValueColor, $"Slot 2##{customSave.SavePath}"))
                            {
                                CustomSavesManager.Load(customSave, 2, BaseSaveInfo!);
                            }

                            ImGui.SameLine();
                            ImGui.TextColored(WarningColor, ">");
                            ImGui.SameLine();
                            ImGui.TextColored(InfoColor, customSave.Name);
                        }
                    });
                });
            }
        });

        ImGui.PopTextWrapPos();
    }
}