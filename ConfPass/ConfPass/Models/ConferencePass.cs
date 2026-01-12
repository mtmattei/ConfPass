namespace ConfPass.Models;

public record ConferencePass
{
    public string EventName { get; init; } = "UNO CONF 2028";
    public string FullName { get; init; } = "Matt Mattei";
    public string Role { get; init; } = "Speaker";
    public string Title { get; init; } = "Developer Relations";
    public string CompanyName { get; init; } = "Uno Platform";
    public string Initials { get; init; } = "MM";
    public string Venue { get; init; } = "Crew, Montreal";
    public string Date { get; init; } = "January 14-17, 2028";
    public string Session { get; init; } = "Cross-Platform Development";
    public string ScanText { get; init; } = "Scan for schedule & materials";
    public string PassId { get; init; } = "ID: CM25-SPK-0847";
    public string AccessLevel { get; init; } = "ALL ACCESS PASS";
}
