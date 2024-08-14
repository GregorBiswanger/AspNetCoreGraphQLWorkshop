﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyConference.Data.Entities;

public class Speaker
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public List<string> SessionIds { get; set; } = new();
}