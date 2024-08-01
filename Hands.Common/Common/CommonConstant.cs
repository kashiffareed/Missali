using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hands.Common.Common
{
    public static class CommonConstant
    {

        [Flags]
        public enum ResponseEnum
        {
            [Description("success")]
            Success = 200,

            [Description("Server Exception")]
            ServerException = 500,
            [Description("Invalid Credentials")]
            InvalidCredentials = 443,

        }

        [Flags]
        public enum UserTypeEnum
        {
            [Description("hcp")]
            hcp = 1,
            [Description("lhv")]
            lhv = 2,
            [Description("marvi")]
            marvi = 3,
            [Description("realtime")]
            realtime = 4,
            [Description("shopkeeper")]
            shopkeeper = 5

        }

        [Flags]
        public enum contentTypeEnum
        {
            [Description("image")]
            image = 1,
            [Description("video")]
            video = 2


        }
        [Flags]
        public enum ReturnTypeEnum
        {
            [Description("Return to hands")]
            Returntohands = 1,
            [Description("Miscellenous")]
            Miscellenous = 2


        }
        public enum RightLevelEnum
        {
            [Description("view")]
            view = 1,
            [Description("create")]
            create = 2,
            [Description("edit")]
            edit = 3,
            [Description("delete")]
            delete = 4

        }
        public enum MenuList
        {
            [Description("Dashboard")]
            Dashboard = 2,
            [Description("App User")]
            AppUser = 3,
            [Description("MWRAS Listing")]
            MWRASListing = 4,
            [Description("Real Time checklist")]
            RealTimechecklist = 5,
            [Description("LHV Listing")]
            LHVListing = 6,
            [Description("Marvi Listing")]
            MarviListing = 7,
            [Description("ShopKeeper Listing")]
            ShopKeeperListing = 8,
            [Description("HCP Listing")]
            HCPListing = 9,
            [Description("Real Time Monitoring")]
            RealTimeMonitoring = 10,
            [Description("MWRA")]
            MWRA = 11,
            [Description("Portal User")]
            PortalUser = 12,
            [Description("User")]
            User = 13,
            [Description("Role")]
            Role = 14,
            [Description("Client Lising")]
            ClientLising = 16,  //check!
            [Description("Content")]
            Content = 17,
            [Description("Location")]
            Location = 18,
            [Description("CLMIS Management")]
            CLMISManagement = 19,
            [Description("BLMIS Management")]
            BLMISManagement = 20,
            [Description("BLMIS")]
            BLMIS = 21,
            [Description("Messages")]
            Messages = 22,
            [Description("Events")]
            Events = 23,
            [Description("Session Calls")]
            SessionCalls = 24,
            [Description("Activity")]
            Activity = 25,
            [Description("Marvi Checklist")]
            MarviChecklist = 26,
            [Description("BLMIS Product")]
            BLMISProduct = 27,
            [Description("Category")]
            Category = 28,
            [Description("CMS")]
            CMS = 29,
            [Description("PMS")]
            PMS = 30,
            [Description("Union Councils")]
            UnionCouncils = 31,
            [Description("Taluqa/Tehsil")]
            TaluqaTehsil = 32, //check  
            [Description("Districs")]
            Districs = 33,
            [Description("CLMIS Product")]
            CLMISProduct = 34,
            [Description("CLMIS Hand Stock")]
            CLMISHandStock = 35,
            [Description("CLMIS Inventory")]
            CLMISInventory = 36,
            [Description("BLMIS Inventory")]
            BLMISInventory = 37,
            [Description("BLMIS Marvi")]
            BLMISMarvi = 38,
            [Description("BLMIS SHOPKEEPER")]
            BLMISSHOPKEEPER = 39,
            [Description("Massage")]
            Massage = 40,
            [Description("Push Event")]
            PushEvent = 41,  //check
            [Description("LHV Session Calls")]
            LHVSessionCalls = 42,
            [Description("Past shedule Activity")]
            PastsheduleActivity = 43,
            [Description("Shedule Activity")]
            SheduleActivity = 44,
            [Description("Activity Logs")]
            ActivityLogs = 45,
            [Description("Developer Forml")]
            DeveloperForm = 52,
            [Description("Assign Menu")]
            AssignMenu = 53,
            [Description("CLMIS Dashboard")]
            CLMISDashboard = 54,
            [Description("Geo Location")]
            GeoLocation = 58,
            [Description("Lhv")]
            Lhv = 59,
            [Description("Marvi")]
            Marvi = 60,
            [Description("Shop Keeper")]
            ShopKeeper = 61,
            [Description("Lhv checklist")]
            Lhvchecklist = 62,
            [Description("Client checklist")]
            Clientchecklist = 63,
            [Description("Missed Session Activity")]
            MissedSessionActivity = 64,
            [Description("Clmis Inventory Status")]
            ClmisInventoryStatus = 65,
            [Description("CLMIS Total Hand Stock")]
            CLMISTotalHandStock = 66,
            [Description("Hcp")]
            Hcp = 67,
            [Description("Follow Up")]
            FollowUp = 68,
            [Description("Clmis Inventory Log")]
            ClmisInventoryLog = 69,
            [Description("Clmis Return Management")]
            ClmisReturnManagement = 70,
            [Description("PMS Analytics")]
            PMSAnalytics = 71,
            [Description("Marvi Session Calls")]
            MarviSessionCalls = 72,
            [Description("Menu")]
            Menu = 73,
            [Description("Menu List")]
            MenuList = 74,
            [Description("Assign Menu To Role")]
            AssignMenuToRole = 75,
            [Description("Blmis Sells")]
            BlmisSells = 76,
            [Description("Project")]
            Project = 1076,
            [Description("Project Listing")]
            ProjectListing = 1077,
            [Description("Assign Role To Project")]
            AssignRoleToProject = 1078,
            [Description("Project Creation")]
            ProjectCreation = 1079,
            [Description("AssignMenuToProject")]
            AssignMenuToProject = 1080


        }
    }
}
