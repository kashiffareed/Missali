using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.ViewModels.Models
{
    public class DropOutClient
    {
        public int mwraId { get; set; }
        public DateTime dropOutDate { get; set; }
        public string dropOutReason { get; set; }
    }
}
