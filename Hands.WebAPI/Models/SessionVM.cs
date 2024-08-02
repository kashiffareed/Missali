using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Hands.Data.HandsDB;
using Hands.Service.AppUser;
using Hands.Service.Session;

namespace Hands.WebAPI.Models
{
    public class SessionVM
    {
        public int SessionId { get; set; } // session_id (Primary key)
        public System.DateTime NextSessionSchedule { get; set; } // next_session_schedule
        public short IsCompleted { get; set; } // is_completed
        [Required]
        [StringLength(50, ErrorMessage = "Longitude cannot be longer than 50 characters")]
        public string Longitude { get; set; } // longitude (length: 20)
        [Required]
        [StringLength(50, ErrorMessage = "Latitude cannot be longer than 50 characters")]
        public string Latitude { get; set; } // latitude (length: 20)

        public System.DateTime SessionStartDatetime { get; set; } // session_start_datetime
        public System.DateTime SessionEndDatetime { get; set; } // session_end_datetime
        public short IsGroup { get; set; } // is_group
        [Required]

        public int MarviId { get; set; } // marvi_id
        [Required]

        public int MobileSessionId { get; set; } // mobile_session_id
        [Required]
        public int LhvId { get; set; } // lhv_id
        [Required]
        [StringLength(50, ErrorMessage = "UserType cannot be longer than 50 characters")]
        public string UserType { get; set; } // user_type (length: 50)
        public System.DateTime CreatedAt { get; set; } // created_at
        public bool IsActive { get; set; } // IsActive


        public string MarviName { get; set; } // created_at
        public string LhvName { get; set; } // IsActive

        public int MwraCount { get; set; } //New

        public SessionVM SessionMapping(Session session, ISessionService sessionService, IAppUserService appUserService) //Add SessionMwra sessionMwra
        {
            SessionId = session.SessionId;
            NextSessionSchedule = session.NextSessionSchedule;
            IsCompleted = session.IsCompleted;
            Longitude = session.Longitude;
            Latitude = session.Latitude;
            SessionStartDatetime = session.SessionStartDatetime;
            SessionEndDatetime = session.SessionEndDatetime;
            IsGroup = session.IsGroup;
            MobileSessionId = session.MobileSessionId;
            LhvId = session.LhvId;
            UserType = session.UserType;
            MarviId = session.MarviId;
            CreatedAt = DateTime.Now;
            IsActive = session.IsActive;
            MarviName = appUserService.GetNameById(session.MarviId);
            LhvName = appUserService.GetNameById(session.LhvId);
            MwraCount = sessionService.GetMwraCountById(session.SessionId); // New
            MwraNames = sessionService.GetMwraNamesBySessionId(session.SessionId).FirstOrDefault()?.MWRANames;
            return this;
        }

        public string MwraNames { get; set; }

        public List<SessionVM> PrepareViewList(IEnumerable<Session> sessions, ISessionService sessionService,  IAppUserService appUserService)
        {
            var appUserList = appUserService.GetAllAppUser();
            var sessionIdList = sessions.Select(x => x.SessionId).ToArray();
            //var mwraSessionList = sessionService.GetSessionMwraBySessionIds(sessionIdList);
            var mwraList = sessionService.GetMwraBySessionIds(sessionIdList);
            var sessionResult = sessions.Select(session => new SessionVM()
            {
                SessionId = session.SessionId,
                NextSessionSchedule = session.NextSessionSchedule,
                IsCompleted = session.IsCompleted,
                Longitude = session.Longitude,
                Latitude = session.Latitude,
                SessionStartDatetime = session.SessionStartDatetime,
                SessionEndDatetime = session.SessionEndDatetime,
                IsGroup = session.IsGroup,
                MobileSessionId = session.MobileSessionId,
                LhvId = session.LhvId,
                UserType = session.UserType,
                MarviId = session.MarviId,
                CreatedAt = DateTime.Now,
                IsActive = session.IsActive,
                MarviName = appUserList.Where(x => x.AppUserId == session.MarviId).Select(y => y.FullName).FirstOrDefault(),
                LhvName = appUserList.Where(x => x.AppUserId == session.LhvId).Select(y => y.FullName).FirstOrDefault(),
                MwraCount = mwraList.Where(x => x.SessionId == session.SessionId).ToList().Count,
                MwraNames = mwraList.FirstOrDefault(x => x.SessionId == session.SessionId)?.MwraName
            }).ToList();
            return sessionResult;
            //return sessions.Select(x => new SessionVM().SessionMapping(x, sessionService, appUserService)).ToList();
        }
    }
}