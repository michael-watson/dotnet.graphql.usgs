using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet.graphql.usgs.Models
{
    public class Site
    {
        public SiteMeta Info { get; set; }
        public FlowInfo[] Flows { get; set; }
    }
}
