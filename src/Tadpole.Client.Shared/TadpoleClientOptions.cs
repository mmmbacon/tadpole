namespace Tadpole.Client.Shared;

public sealed class TadpoleClientOptions
{
    public const string SectionName = "Tadpole";

    public string ApiBaseUrl { get; set; } = "https://localhost:7001";
}
