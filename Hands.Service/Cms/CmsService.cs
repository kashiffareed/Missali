using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Hands.Common.Common;
using Hands.Data.HandsDB;
using Hands.Service.Cms;
namespace Hands.Service.Cms
{
    public class  CmsService : ServiceBase<Content>, ICmsService
    {
        public List<SpContentcmsReturnModel> GetAllcmsList(int projectId)
        {
            return _db.SpContentcms(projectId).OrderByDescending(x=>x.content_id).ToList();
        }

        public IEnumerable<GetAllContentReturnModel> GetcontentByType(int? projectId)
        {
            return _db.GetAllContent(projectId).OrderByDescending(x=>x.content_id);
        }
    }

     
    }

