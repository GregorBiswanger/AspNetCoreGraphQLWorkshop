using GraphQL.Types;
using MyConferece.Data.Entities;

namespace MyConference.GraphQL.Types
{
    public class SpeakerType : ObjectGraphType<SpeakerComposed>
    {
        public SpeakerType()
        {
            Field(speaker => speaker.Id);
            Field(speaker => speaker.Name).Description("The speakers name");
            Field(speaker => speaker.Description);
        }
    }
}