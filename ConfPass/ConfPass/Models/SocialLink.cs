namespace ConfPass.Models;

public record SocialLink
{
    public string Platform { get; init; } = string.Empty;
    public string Url { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
    public string IconPath { get; init; } = string.Empty;
}
