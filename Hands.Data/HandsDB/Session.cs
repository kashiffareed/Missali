// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable EmptyNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 4.8
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Hands.Data.HandsDB
{

    // session
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.36.1.0")]
    public partial class Session : Entity
    {
        public int SessionId { get; set; } // session_id (Primary key)
        public System.DateTime NextSessionSchedule { get; set; } // next_session_schedule
        public short IsCompleted { get; set; } // is_completed
        public string Longitude { get; set; } // longitude (length: 20)
        public string Latitude { get; set; } // latitude (length: 20)
        public System.DateTime SessionStartDatetime { get; set; } // session_start_datetime
        public System.DateTime SessionEndDatetime { get; set; } // session_end_datetime
        public short IsGroup { get; set; } // is_group
        public int MarviId { get; set; } // marvi_id
        public int MobileSessionId { get; set; } // mobile_session_id
        public int LhvId { get; set; } // lhv_id
        public string UserType { get; set; } // user_type (length: 50)
        public System.DateTime CreatedAt { get; set; } // created_at
        public bool IsActive { get; set; } // IsActive
        public int? ProjectId { get; set; } // ProjectId

        // Reverse navigation

        /// <summary>
        /// Child SessionContents where [SessionContent].[sessionId] point to this entity (FK_SessionContent_session)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<SessionContent> SessionContents { get; set; } // SessionContent.FK_SessionContent_session

        public Session()
        {
            CreatedAt = System.DateTime.Now;
            IsActive = true;
            SessionContents = new System.Collections.Generic.List<SessionContent>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
