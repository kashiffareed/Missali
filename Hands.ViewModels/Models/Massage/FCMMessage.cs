using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.ViewModels.Models.Massage
{
  public class FCMMessage
    {
        public int Status { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        // public Dictionary<string, object> ValidationMessages { get; set; }
        public object Data { get; set; }
    }
}
