using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet.graphql.usgs.Models
{
    [Table("SiteFlows")]
    public class FlowInfo
    {
        [Key]
        public string Id { get; set; }
        public string SiteId { get; set; }
        public string FlowValue { get; set; }
        public string FlowUnitCode { get; set; }
        public DateTime DateMeasured { get; set; }
    }
}