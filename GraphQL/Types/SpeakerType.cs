using GraphQL.Types;
using MyConferece.Data.Entities;

namespace MyConference.GraphQL.Types
{
    public class SpeakerType : ObjectGraphType<SpeakerComposed>
    {
        public SpeakerType()
        {
            Field(speaker => speaker.Id);
            Field(speaker => speaker.Name);
            Field(speaker => speaker.Description).Description("The speakers name");
            Field<ListGraphType<SessionType>>("sessions", resolve: context => context.Source.Sessions);
        }
    }
}