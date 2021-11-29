using GraphQL.Types;
using MyConferece.Repositories;
using MyConference.GraphQL.Types;

namespace MyConference.GraphQL
{
    public class MyConferenceQuery : ObjectGraphType
    {
        public MyConferenceQuery(SpeakerRepository speakerRepository)
        {
            FieldAsync<ListGraphType<SpeakerType>>("allSpeaker",
                resolve: async context => await speakerRepository.AllSpeakerAsync());
        }
    }
}