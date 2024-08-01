using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.Dynamic;
using System.Security.Cryptography;
using System.Xml.Schema;
using Hands.Data.HandsDB;

namespace Hands.Common.Common
{
    public sealed class HandSession
    {
        public const string Singletone = "HandsSession_be7112e1-051b-4b2b-b97b-d10d0f808f2a";
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public int RoleId { get; set; }
        public List<RoleMenuAccessLevelRight> AccessList { get; set; }

        public static HandSession Current
        {
            get
            {
                if (HttpContext.Current.Session[Singletone] == null)
                {
                    HttpContext.Current.Session[Singletone] = new HandSession();
                }

                return HttpContext.Current.Session[Singletone] as HandSession;
            }
        }

        public void ClearSession()
        {
            HttpContext.Current.Session[Singletone] = null;
        }
    }

}
