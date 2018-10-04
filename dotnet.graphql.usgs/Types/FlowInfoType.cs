using dotnet.graphql.usgs.Models;
using GraphQL.Types;

namespace dotnet.graphql.usgs.Types
{
    public class FlowInfoType : ObjectGraphType<FlowInfo>
    {
        public FlowInfoType()
        {
            Field(site => site.Id).Description("The id of the flow");
            Field(site => site.SiteId).Description("The id of the site for the registered flow");
            Field(site => site.FlowValue).Description("The value of the measured flow. This could also be a voltage for certain sites, check the unit code.");
            Field(site => site.FlowUnitCode).Description("The unit code for the measured flow");
            Field(site => site.DateMeasured).Description("The date and time when the flow was measured");
        }
    }
}