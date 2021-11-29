namespace MyConferece.Data.Entities
{
    public class SessionComposed : Session
    {
        public IList<SpeakerComposed> Speakers { get; set; } = new List<SpeakerComposed>();
    }
}
