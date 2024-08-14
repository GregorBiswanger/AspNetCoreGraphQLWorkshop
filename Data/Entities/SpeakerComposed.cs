namespace MyConference.Data.Entities;

public class SpeakerComposed : Speaker
{
    public List<SessionComposed> Sessions { get; set; } = new();
}