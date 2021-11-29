using GraphQL.Types;
using MyConferece.Data.Entities;

namespace MyConference.GraphQL.Types
{
    public class CategoryTypeEnumType : EnumerationGraphType<CategoryType>
    {
        public CategoryTypeEnumType()
        {
            Name = "Category";
            Description = "The category of session";
        }
    }
}
