using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hands.WebAPI.Models
{
    public class UnionCouncilApiModel
    {

        public int UnionCouncilId { get; set; }
        public string UnionCouncilName { get; set; }
        public int TaluqaId { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}