using dotnet.graphql.usgs.Models;
using GraphQL.Types;

namespace dotnet.graphql.usgs.Types
{
    public class SiteMetaType : ObjectGraphType<SiteMeta>
    {
        public SiteMetaType()
        {
            Name = nameof(SiteMeta);
            Description = "This is the metadata around the USGS Site that is being measured";

            Field(site => site.Id).Description("The id of the site");
            Field(site => site.Name).Description("The name of the site");
            Field(site => site.Longitude).Description("The longitude of the site");
            Field(site => site.Latitude).Description("The latitude of the site");
            Field(site => site.ZoneOffset).Description("The timezone offset from the location of the site");
        }
    }
}