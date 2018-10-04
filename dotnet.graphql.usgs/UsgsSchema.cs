using GraphQL.Types;

namespace dotnet.graphql.usgs
{
    public class UsgsSchema : Schema
    {
        public UsgsSchema()
        {
            Query = new UsgsQuery();
        }
    }
}
