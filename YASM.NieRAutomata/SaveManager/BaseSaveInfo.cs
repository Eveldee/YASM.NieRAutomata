using System.Text.Json.Serialization;

namespace YASM.NieRAutomata.SaveManager;

[method: JsonConstructor]
public record BaseSaveInfo(byte[] SteamId, byte[] Settings, byte[] Controls)
{
    public BaseSaveInfo(Span<byte> SteamId, Span<byte> Settings, Span<byte> Controls) : this(SteamId.ToArray(), Settings.ToArray(), Controls.ToArray())
    {

    }
}