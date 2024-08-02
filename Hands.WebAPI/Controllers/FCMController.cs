using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Hands.Data.HandsDB;
using Hands.Service.Device;
using Hands.WebAPI.Models;

namespace Hands.WebAPI.Controllers
{
    public class FCMController : ApiController
    {
        private readonly IDeviceService _deviceService;
        private ApiResponse _response;
        public FCMController()
        {
            _deviceService = new DeviceService();
            _response = new ApiResponse();
        }


        // GET: FCM
        private string PushMessage(string postData)
        {
            //var ApplicationID = Properties.Settings.Default.GoogleApplicationId;
            //var SENDER_ID = ConfigurationSettings.AppSettings["SENDER_ID"].ToString();
            //var PushServerPath = ConfigurationSettings.AppSettings["PushServerPath"].ToString();
            //WebRequest tRequest;
            //tRequest = WebRequest.Create(PushServerPath);
            //tRequest.Method = "post";
            //tRequest.ContentType = "application/json";
            //tRequest.Headers.Add(string.Format("Authorization: key={0}", ApplicationID));
            //tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

            //Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            //tRequest.ContentLength = byteArray.Length;
            //Stream dataStream = tRequest.GetRequestStream();
            //dataStream.Write(byteArray, 0, byteArray.Length);
            //dataStream.Close();
            //WebResponse tResponse = tRequest.GetResponse(); dataStream = tResponse.GetResponseStream();
            //StreamReader tReader = new StreamReader(dataStream);
            //String sResponseFromServer = tReader.ReadToEnd();  //Get response from GCM server  
            //var response = sResponseFromServer; //Assigning GCM response to Label text
            //tReader.Close(); dataStream.Close();
            //tResponse.Close();


            return "";
        }

        public IHttpActionResult Register(List<Hands.Data.HandsDB.Device> register)
        {
            try
            {
                var deviceFromDb = _deviceService.GetAllActive()
                    .FirstOrDefault(x => x.MacAddress == register[0].MacAddress);

                if (deviceFromDb == null)
                {
                    register[0].IsActive = true;
                    _deviceService.Insert(register[0]);

                }
                else
                {
                    deviceFromDb.DeviceId = register[0].DeviceId;
                    deviceFromDb.UserId = register[0].UserId;
                    deviceFromDb.DeviceToken = register[0].DeviceToken;
                    deviceFromDb.DeviceType = register[0].DeviceType;
                    deviceFromDb.VersionName = register[0].VersionName;
                    deviceFromDb.VersionCode = register[0].VersionCode;
                    deviceFromDb.VersionCode = register[0].VersionCode;
                    _deviceService.Update(deviceFromDb);

                }

                _deviceService.SaveChanges();
                _response.Success = true;
                _response.Result = null;
                return Json(_response);
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Result = null;
                _response.Message = ex.Message;
                return Json(_response);
            }
        }

    }
}