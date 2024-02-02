﻿using System.Text.Json;
using System.Text.Json.Serialization;
using GlobalHotKeys.Native.Types;

namespace YASM.NieRAutomata.Utils;

[method: JsonConstructor]
public class YasmOptions(
    bool allowResize = false,
    int shortcutKey = (int)VirtualKeyCode.VK_BACK,
    bool shortcutCtrl = true,
    bool shortcutShift = false,
    bool shortcutAlt = false
)
{
    private bool _allowResize = allowResize;
    public ref bool AllowResize => ref _allowResize;

    private bool _shortcutCtrl = shortcutCtrl;
    public ref bool ShortcutCtrl => ref _shortcutCtrl;

    private bool _shortcutShift = shortcutShift;
    public ref bool ShortcutShift => ref _shortcutShift;

    private bool _shortcutAlt = shortcutAlt;
    public ref bool ShortcutAlt => ref _shortcutAlt;

    private int _shortcutKey = shortcutKey;
    public ref int ShortcutKey => ref _shortcutKey;

    public void Save()
    {
        File.WriteAllText(PathUtils.OptionsPath, JsonSerializer.Serialize(this, SerializationUtils.SerializerOptions));
    }
}