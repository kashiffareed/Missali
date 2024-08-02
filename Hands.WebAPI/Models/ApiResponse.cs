using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hands.WebAPI.Models
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
        public int TotalRecords { get; set; }
    }
}