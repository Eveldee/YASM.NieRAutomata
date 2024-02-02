using System.Text.Json;

namespace YASM.NieRAutomata.Utils;

public static class SerializationUtils
{
    public static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = true,
        TypeInfoResolver = SourceGenerationContext.Default
    };
}
