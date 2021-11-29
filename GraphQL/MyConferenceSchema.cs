using GraphQL.Types;

namespace MyConference.GraphQL
{
    public class MyConferenceSchema : Schema
    {
        public MyConferenceSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = (IObjectGraphType)serviceProvider.GetService(typeof(MyConferenceQuery));
        }
    }
}

















