using System.Text.Json.Serialization;
using YASM.NieRAutomata.SaveManager;

namespace YASM.NieRAutomata.Utils;

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(BaseSaveInfo))]
internal partial class SourceGenerationContext : JsonSerializerContext
{

}