namespace MyConferece.Data.Entities
{
    public class SpeakerComposed : Speaker
    {
        public IList<SessionComposed> Sessions { get; set; } = new List<SessionComposed>();
    }
}
