using GraphQL;
using GraphQL.Types;

namespace MyConference.GraphQL
{
    public class MyConferenceSchema : Schema
    {
        public MyConferenceSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<MyConferenceQuery>();
        }
    }
}