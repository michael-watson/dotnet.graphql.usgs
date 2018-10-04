using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet.graphql.usgs.Models
{
    [Table("Sites")]
    public class SiteMeta
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string SiteCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ZoneOffset { get; set; }
    }
}