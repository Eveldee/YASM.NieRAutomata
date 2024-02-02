using System.Text.Json;
using System.Text.Json.Serialization;

namespace YASM.NieRAutomata.Utils;

[method: JsonConstructor]
public class YasmOptions(bool allowResize = false)
{
    private bool _allowResize = allowResize;
    public ref bool AllowResize => ref _allowResize;

    public void Save()
    {
        File.WriteAllText(PathUtils.OptionsPath, JsonSerializer.Serialize(this, SerializationUtils.SerializerOptions));
    }
}