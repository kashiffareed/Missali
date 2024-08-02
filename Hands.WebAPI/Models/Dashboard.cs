using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hands.WebAPI.Models
{
    public class Dashboard
    {
        public int MarviCount { get; set; }
        public int MwraCount { get; set; }
        public int MwraclientCount { get; set; }
        public int Lhvcount { get; set; }
        public int sessionCount { get; set; }
    }
}