using GraphQL;
using GraphQL.Types;
using MyConferece.Repositories;
using MyConference.GraphQL.Types;

namespace MyConference.GraphQL
{
    public class MyConferenceQuery : ObjectGraphType
    {
        public MyConferenceQuery(SpeakerRepository speakerRepository, SessionsRepository sessionsRepository)
        {
            FieldAsync<ListGraphType<SpeakerType>>("allSpeaker",
                resolve: async context => await speakerRepository.AllSpeakerAsync());

            FieldAsync<SpeakerType>("speaker",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: async context =>
                {
                    string? id = context.GetArgument<string>("id");
                    return await speakerRepository.SpeakerAsync(id);
                });

            FieldAsync<ListGraphType<SessionType>>("allSessions",
                arguments: new QueryArguments(new QueryArgument<BooleanGraphType> { Name = "approvedOnly", DefaultValue = true }),
                resolve: async context =>
                {
                    bool approvedOnly = context.GetArgument<bool>("approvedOnly");
                    return await sessionsRepository.AllSessionsAsync(approvedOnly);
                });

            FieldAsync<SpeakerType>("session",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: async context =>
                {
                    string? id = context.GetArgument<string>("id");
                    return await sessionsRepository.SessionAsync(id);
                });
        }
    }
}