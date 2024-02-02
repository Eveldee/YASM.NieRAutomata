using System.Reflection;
using ClickableTransparentOverlay;
using GlobalHotKeys;
using GlobalHotKeys.Native.Types;
using ImGuiNET;
using YASM.NieRAutomata.Utils;

namespace YASM.NieRAutomata.Overlays;

public partial class MainOverlay() : Overlay("YASM")
{
    private bool _displayMainOverlay = true;

    protected override Task PostInitialized()
    {
        VSync = true;

        // Fix window size
        var windowHandle = WindowUtils.FindWindow(null!, "YASM");
        WindowUtils.ShowWindow(windowHandle, WindowUtils.SW_SHOWMAXIMIZED);

        // Global toggle shortcut
        _hotKeyManager.HotKeyPressed.Subscribe(ShortcutPressed);
        RegisterHotkey();

        return Task.CompletedTask;
    }

    private void RegisterHotkey()
    {
        Modifiers modifiers = (YasmOptions.ShortcutCtrl  ? Modifiers.Control : 0) |
                              (YasmOptions.ShortcutShift ? Modifiers.Shift   : 0) |
                              (YasmOptions.ShortcutAlt   ? Modifiers.Alt     : 0);

        VirtualKeyCode key = Enum.Parse<VirtualKeyCode>(ShortcutUtils.KeyNames[YasmOptions.ShortcutKeyNameIndex]);

        // Unregister all shortcut and register updated one
        _shortcutRegistration?.Dispose();
         _shortcutRegistration = _hotKeyManager.Register(key, modifiers);
    }

    private void ShortcutPressed(HotKey hotKey)
    {
        _displayMainOverlay = !_displayMainOverlay;
    }

    protected override void Render()
    {
        if (_displayMainOverlay)
        {
            DisplayOverlay();
        }
    }

    private void DisplayOverlay()
    {
        ImGui.Begin("YASM", ImGuiWindowFlags.MenuBar);

        if (!YasmOptions.AllowResize)
        {
            ImGui.SetWindowSize(new(-1, -1));
        }

        if (ImGui.BeginMenuBar())
        {
            if (ImGui.MenuItem("Quit"))
            {
                YasmOptions.Save();

                ImGui.SaveIniSettingsToDisk("imgui.ini");
                Environment.Exit(0);
            }

            if (ImGui.BeginMenu("Options"))
            {
                ImGui.SeparatorText("General");

                ImGui.MenuItem("Allow Resize", null, ref YasmOptions.AllowResize);

                ImGui.NewLine();
                ImGui.SeparatorText("Toggle shortcut");

                if (ImGui.Checkbox("Ctrl", ref YasmOptions.ShortcutCtrl))
                {
                    RegisterHotkey();
                }
                ImGui.SameLine();
                if (ImGui.Checkbox("Shift", ref YasmOptions.ShortcutShift))
                {
                    RegisterHotkey();
                }
                ImGui.SameLine();
                if (ImGui.Checkbox("Alt", ref YasmOptions.ShortcutAlt))
                {
                    RegisterHotkey();
                }

                if (ImGui.Combo("Key", ref YasmOptions.ShortcutKeyNameIndex, ShortcutUtils.KeyNames, ShortcutUtils.KeyNames.Length))
                {
                    RegisterHotkey();
                }

                ImGui.EndMenu();
            }

            ImGui.EndMenuBar();
        }

        ImGui.TextColored(TitleColor, $"Welcome to YASM version v{Assembly.GetExecutingAssembly().GetName().Version}, a NieR: Automata save manager.");
        ImGui.Separator();

        TabBar("MainTabBar", ImGuiTabBarFlags.AutoSelectNewTabs, () =>
        {
            // Save manager that allows to load saves
            if (BaseSaveInfo is not null)
            {
                if (_focusManagerNext)
                {
                    TabItem("Saves Loader", ImGuiTabItemFlags.SetSelected, DisplayLoader);
                    _focusManagerNext = false;
                }
                else
                {
                    TabItem("Saves Loader", DisplayLoader);
                }
            }

            // Import base save info
            TabItem("Settings Importer", DisplayImport);
        });

        ImGui.End();
    }
}