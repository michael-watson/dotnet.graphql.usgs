using dotnet.graphql.usgs.Models;
using dotnet.graphql.usgs.Types;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace dotnet.graphql.usgs
{
    public class UsgsQuery : ObjectGraphType<object>
    {
        public UsgsQuery()
        {
            Name = nameof(UsgsQuery);

            Field<ListGraphType<SiteType>>(
                name: "sites",
                description: "This returns all of the sites",
                resolve: context => GetSites(context)
            );
            FieldDelegate<SiteType>(
               name: "site",
               description: "This returns info on one specific site",
                arguments: new QueryArguments
               {
                   new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "site id"}
               },
               resolve: getSiteFunc
           );
        }

        Func<object,string, Site> getSiteFunc = (context, id) => GetSite(id);

        static List<Site> GetSites(ResolveFieldContext<object> context)
        {
            List<SiteMeta> siteMetas = new List<SiteMeta>();
            List<Site> sites = new List<Site>();

            using (var readContext = new SiteDbContext())
            {
                siteMetas = readContext.Sites.Take(50)
                   .OrderByDescending(site => site.Name)
                   .ToList();
            }

            foreach (var siteMeta in siteMetas)
                sites.Add(new Site { Info = siteMeta });

            return sites;
        }

        static Site GetSite(string id)
        {
            SiteMeta siteMeta = null;

            using (var readContext = new SiteDbContext())
            {
                siteMeta = readContext.Sites.FirstOrDefault(s => s.Id == id);
            }

            return new Site { Info = siteMeta };
        }
    }
}