using MongoDB.Driver;
using MyConference.Data;
using MyConference.Data.Entities;

namespace MyConference.Repositories;

public class SessionsRepository
{
    private readonly MyConferenceDataContext _dataContext;

    public SessionsRepository(MyConferenceDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<List<SessionComposed>> AllSessionsAsync(bool approvedOnly)
    {
        return await _dataContext.Sessions
            .Aggregate()
            .Match(session => session.Approved == approvedOnly)
            .Lookup<Session, Speaker, SessionComposed>(
                _dataContext.Speakers,
                session => session.SpeakerIds,
                speaker => speaker.Id,
                sessionComposed => sessionComposed.Speakers
            ).ToListAsync();
    }

    public async Task<SessionComposed> SessionAsync(string sessionId)
    {
        return await _dataContext.Sessions.Aggregate()
            .Match(session => session.Id == sessionId)
            .Lookup<Session, Speaker, SessionComposed>(
                _dataContext.Speakers,
                session => session.SpeakerIds,
                speaker => speaker.Id,
                speakerComposed => speakerComposed.Speakers
            ).SingleAsync();
    }
}