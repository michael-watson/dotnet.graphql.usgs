using dotnet.graphql.usgs.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace dotnet.graphql.usgs.Types
{
    public class SiteType : ObjectGraphType<Site>
    {
        public SiteType()
        {
            Name = "Site";

            Func<ResolveFieldContext<Site>, int, object> getSiteFlows = (context, days) => GetSiteFlows(context, days);

            Field<SiteMetaType>(
                name: "info",
                description: "This is the meta data information about the site",
                resolve: (context) =>
                {
                    siteCode = context.Source.Info.Id;
                    return context.Source.Info;
                }
            );
            FieldDelegate<ListGraphType<FlowInfoType>>(
               name: "flows",
               description: "This is the meta data information about the site",
               arguments: new QueryArguments
               {
                   new QueryArgument<IntGraphType> { Name = "days", Description = "defaults to 7 days of flow data", DefaultValue = 7 }
               },
               resolve: getSiteFlows
           );
        }

        string siteCode;

        public List<FlowInfo> GetSiteFlows(ResolveFieldContext<Site> context, int days)
        {
            if (days == 0) days = 7;

            List<FlowInfo> flows = new List<FlowInfo>();

            using (var readContext = new SiteDbContext())
            {
                var minDatTimee = DateTime.Now.Subtract(TimeSpan.FromDays(days));

                flows = readContext.SiteFlows
                   .Where(flow => flow.SiteId == siteCode
                       && flow.DateMeasured > minDatTimee)
                   .OrderByDescending(flow => flow.DateMeasured)
                   .ToList();
            }

            return flows;
        }
    }
}