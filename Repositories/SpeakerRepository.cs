using MongoDB.Driver;
using MyConference.Data;
using MyConference.Data.Entities;

namespace MyConference.Repositories;

public class SpeakerRepository
{
    private readonly MyConferenceDataContext _dataContext;

    public SpeakerRepository(MyConferenceDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<SpeakerComposed> AddSpeakerAsync(Speaker speaker)
    {
        await _dataContext.Speakers.InsertOneAsync(speaker);

        return new SpeakerComposed 
        { 
            Id = speaker.Id, 
            Name = speaker.Name, 
            Description = speaker.Description 
        };
    }

    public async Task<List<SpeakerComposed>> AllSpeakerAsync()
    {
        return await _dataContext.Speakers
            .Aggregate().Lookup<Speaker, Session, SpeakerComposed>(
                _dataContext.Sessions,
                speaker => speaker.SessionIds,
                session => session.Id,
                speakerComposed => speakerComposed.Sessions
            ).ToListAsync();
    }

    public async Task<SpeakerComposed> SpeakerAsync(string id)
    {
        return await _dataContext.Speakers.Aggregate()
            .Match(speaker => speaker.Id == id)
            .Lookup<Speaker, Session, SpeakerComposed>(
                _dataContext.Sessions,
                speaker => speaker.SessionIds,
                session => session.Id,
                speakerComposed => speakerComposed.Sessions
            ).SingleAsync();
    }

    public async Task<IList<SpeakerComposed>> SpeakersAsync(IList<string> ids)
    {
        return await _dataContext.Speakers.Aggregate()
            .Match(speaker => ids.Contains(speaker.Id))
            .Lookup<Speaker, Session, SpeakerComposed>(
                _dataContext.Sessions,
                speaker => speaker.SessionIds,
                session => session.Id,
                speakerComposed => speakerComposed.Sessions
            ).ToListAsync();
    }
}