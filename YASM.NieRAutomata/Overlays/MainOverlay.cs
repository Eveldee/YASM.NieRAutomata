using System.Reflection;
using ClickableTransparentOverlay;
using ImGuiNET;
using YASM.NieRAutomata.Utils;

namespace YASM.NieRAutomata.Overlays;

public partial class MainOverlay() : Overlay("YASM")
{
    private bool _displayMainOverlay = true;

    protected override Task PostInitialized()
    {
        VSync = true;

        var windowHandle = WindowUtils.FindWindow(null!, "YASM");
        WindowUtils.ShowWindow(windowHandle, WindowUtils.SW_SHOWMAXIMIZED);

        return Task.CompletedTask;
    }

    protected override void Render()
    {
        if (_displayMainOverlay)
        {
            DisplayOverlay();
        }
        else
        {
            Environment.Exit(0);
        }
    }

    private void DisplayOverlay()
    {
        ImGui.Begin("YASM", ref _displayMainOverlay, ImGuiWindowFlags.NoResize);

        ImGui.SetWindowSize(new(-1, -1));

        ImGui.TextColored(TitleColor, $"Welcome to YASM version v{Assembly.GetExecutingAssembly().GetName().Version}, a NieR: Automata save manager.");

        ImGui.Separator();

        TabBar("MainTabBar", ImGuiTabBarFlags.AutoSelectNewTabs, () =>
        {
            // Save manager that allows to load saves
            if (BaseSaveInfo is not null)
            {
                if (_focusManagerNext)
                {
                    TabItem("Saves Manager", ImGuiTabItemFlags.SetSelected, DisplayManager);
                    _focusManagerNext = false;
                }
                else
                {
                    TabItem("Saves Manager", DisplayManager);
                }
            }

            // Import base save info
            TabItem("Settings Importer", DisplayImport);
        });

        ImGui.End();
    }
}