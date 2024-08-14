namespace MyConference.Data.Entities;

public class SessionComposed : Session
{
    public List<SpeakerComposed> Speakers { get; set; } = new();
}