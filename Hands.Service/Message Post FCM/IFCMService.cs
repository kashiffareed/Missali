using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.Service.Message_Post_FCM
{
    public interface  IFCMService
    {
        void SendPushToUser(string title, string imagePath, string Desc, string Type, string URI);

        void SendPushToUserVerification(int EmpId, string title, string imagePath, string Desc, string Type,
            string URI);

        Hands.ViewModels.Models.Massage.FCMMessage SendMultipleMsg(IList<string> list, string title, string imagePath,
            string Desc, string Type, string URI);

    }
}
