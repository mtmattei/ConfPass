namespace ConfPass.Models;

public partial record ScheduleItem
{
    public string Id { get; init; } = string.Empty;
    public string Time { get; init; } = string.Empty;
    public string Period { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string Room { get; init; } = string.Empty;
    public string Duration { get; init; } = string.Empty;
    public bool IsSpeaking { get; init; }
}
