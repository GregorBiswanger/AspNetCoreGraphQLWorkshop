using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyConference.Data.Entities;

public class Session
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public CategoryType Category { get; set; }

    public LevelType Level { get; set; }

    public string? FromTime { get; set; }
        
    public string? ToTime { get; set; }

    public DateTime Date { get; set; }

    public string? Duration { get; set; }

    public bool Approved { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public List<string> SpeakerIds { get; set; } = new();
}