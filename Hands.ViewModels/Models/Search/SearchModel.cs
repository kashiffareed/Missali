using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;

namespace Hands.ViewModels.Models.Search
{
    public class SearchModel
    {
        public int? RegionId { get; set; }
        public int? TaluqaId { get; set; } // taluqa_id
        public int? UnionCouncilId { get; set; } // union_council_id
        public IEnumerable<Region> Regions { get; set; }
        public string ControllerName { get; set; }

        public string ViewName { get; set; } = "Index";
        public string Action { get; set; } = "Import";
    }
}
