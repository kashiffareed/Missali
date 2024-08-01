using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.Common.Common
{
   public static class MyExtension
    {
        public static int ToInt(this object obj)
        {
            return Convert.ToInt32(obj);
        }
    }
}
