using GraphQL.Types;
using MyConferece.Data.Entities;
using MyConferece.Repositories;

namespace MyConference.GraphQL.Types
{
    public class SessionType : ObjectGraphType<SessionComposed>
    {
        public SessionType(SpeakerRepository speakerRepository)
        {
            Field(session => session.Id);
            Field(session => session.Title);
            Field(session => session.Description);
            Field<CategoryTypeEnumType>("category");
            Field<LevelTypeEnumType>("level");
            Field(session => session.FromTime);
            Field(session => session.ToTime);
            Field(session => session.Date);
            Field(session => session.Duration);
            Field(session => session.Approved);
            FieldAsync<ListGraphType<SpeakerType>>("speakers",
                resolve: async context => await speakerRepository.SpeakersAsync(context.Source.SpeakerIds));
        }
    }
}
