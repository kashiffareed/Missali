using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Hands.Service.Device;
 

namespace Hands.Service.Message_Post_FCM
{
   public class FCMService:IFCMService
    {
        private readonly IDeviceService _deviceService;
        public FCMService()
        {
            _deviceService = new DeviceService();

        }
        public void SendPushToUser(string title, string imagePath, string Desc, string Type, string URI)
        {
            var deviceFromDb = _deviceService.GetAllActive().ToList();
            List<string> listusers = _deviceService.GetAllActive().Where(x => x.DeviceToken != null).Select(x => x.DeviceToken).ToList();
            SendMultipleMsg(listusers, title, imagePath, Desc, Type, URI);

        }
        public void SendPushToUserVerification(int EmpId, string title, string imagePath, string Desc, string Type, string URI)
        {

            List<string> listusers = _deviceService.GetAllActive().Where(x => x.DeviceToken != null && x.UserId == EmpId).Select(x => x.DeviceToken).ToList();
            SendMultipleMsg(listusers, title, imagePath, Desc, Type, URI);

        }

        private Hands.ViewModels.Models.Massage.FCMMessage sendRequest(IList<string> deviceId, string title, string imagePath, string Desc, string Type, string URI)
        {
            try
            {
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var data = new
                {
                    registration_ids = deviceId,
                    badge = 12,
                    priority = "high",
                    content_available = true,
                    notification = new
                    {
                        body = Desc,
                        title = title,
                        badge = 0,
                        msg = title,
                        sound = "default"
                    },
                    data = new
                    {

                        title = title,
                        image = imagePath,
                        Desc = Desc,
                        Type = Type,
                        URI = URI
                    }
                };


                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                string FcmAuthId = ConfigurationSettings.AppSettings["GoogleApplicationId"];// WebConfigurationManager.AppSettings["FcmAuthId"];
                string FcmAppId = ConfigurationSettings.AppSettings["SENDER_ID"]; 

                tRequest.Headers.Add(string.Format("Authorization: key={0}", FcmAuthId));
                tRequest.Headers.Add(string.Format("Sender: id={0}", FcmAppId));
                tRequest.ContentLength = byteArray.Length;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                return new Hands.ViewModels.Models.Massage.FCMMessage { Data = sResponseFromServer, Success = true };
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return new Hands.ViewModels.Models.Massage.FCMMessage { Data = null, Success = false, Message = e.Message };
            }
        }

        public Hands.ViewModels.Models.Massage.FCMMessage SendMultipleMsg(IList<string> list, string title, string imagePath, string Desc, string Type, string URI)
        {
            return sendRequest(list, title, imagePath, Desc, Type, URI);
        }

    }
}
