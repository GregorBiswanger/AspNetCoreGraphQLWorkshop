using GraphQL.Types;
using MyConferece.Data.Entities;

namespace MyConference.GraphQL.Types
{
    public class LevelTypeEnumType : EnumerationGraphType<LevelType>
    {
        public LevelTypeEnumType()
        {
            Name = "Level";
            Description = "The level of session";
        }
    }
}
