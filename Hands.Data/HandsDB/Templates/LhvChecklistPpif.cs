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

    // The table 'lhv_checklist_ppif' is not usable by entity framework because it
    // does not have a primary key. It is listed here for completeness.
    // lhv_checklist_ppif
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.36.1.0")]
    public partial class LhvChecklistPpif : Entity
    {
        public double? LhvChecklistId { get; set; } // lhv_checklist_id
        public double? AppUserId { get; set; } // app_user_id
        public double? VisitorId { get; set; } // visitor_id
        public string VisitorName { get; set; } // visitor_name (length: 255)
        public System.DateTime? CreatedAt { get; set; } // created_at
        public string IucdInsertionKit { get; set; } // iucd_insertion_kit (length: 255)
        public string MuacTape { get; set; } // muac_tape (length: 255)
        public string MeasuringTape { get; set; } // measuring_tape (length: 255)
        public string Stethoscope { get; set; } // stethoscope (length: 255)
        public string BpApparatus { get; set; } // bp_apparatus (length: 255)
        public string WeighingMachcine { get; set; } // weighing_machcine (length: 255)
        public string Fetoscope { get; set; } // fetoscope (length: 255)
        public string Thermometer { get; set; } // thermometer (length: 255)
        public string InfectionPreventionKit { get; set; } // infection_prevention_kit (length: 255)
        public string TabletWithCharger { get; set; } // tablet_with_charger (length: 255)
        public string DangerBox { get; set; } // danger_box (length: 255)
        public string LhvApron { get; set; } // lhv_apron (length: 255)
        public string MecWheel { get; set; } // mec_wheel (length: 255)
        public string IucdInsertionKitFunc { get; set; } // iucd_insertion_kit_func (length: 255)
        public string BpApparatusFunc { get; set; } // bp_apparatus_func (length: 255)
        public string WeighingFunc { get; set; } // weighing_func (length: 255)
        public string ThermometerFunc { get; set; } // thermometer_func (length: 255)
        public string InfectionPreventionKitFunc { get; set; } // infection_prevention_kit_func (length: 255)
        public string TabletWithChargerFunc { get; set; } // tablet_with_charger_func (length: 255)
        public string DangerBoxFunc { get; set; } // danger_box_func (length: 255)
        public string LhvApronFunc { get; set; } // lhv_apron_func (length: 255)
        public string ProvideCounselingAboutFp { get; set; } // provide_counseling_about_fp (length: 255)
        public string OralPills { get; set; } // oral_pills (length: 255)
        public string Injections { get; set; } // injections (length: 255)
        public string Iud { get; set; } // iud (length: 255)
        public string Condoms { get; set; } // condoms (length: 255)
        public string Implant { get; set; } // implant (length: 255)
        public string AnyNaturalMethod { get; set; } // any_natural_method (length: 255)
        public string PermanentMethod { get; set; } // permanent_method (length: 255)
        public string AnyOther { get; set; } // any_other (length: 255)
        public string ProviderUseMecWheel { get; set; } // provider_use_mec_wheel (length: 255)
        public string CheckBp { get; set; } // check_bp (length: 255)
        public string CheckAnemiaAndJaundice { get; set; } // check_anemia_and_jaundice (length: 255)
        public string MeasurementOfWeight { get; set; } // measurement_of_weight (length: 255)
        public string PelvicExam { get; set; } // pelvic_exam (length: 255)
        public string ClientReceiveMethodChoice { get; set; } // client_receive_method_choice (length: 255)
        public string ExplainHowToUse { get; set; } // explain_how_to_use (length: 255)
        public string AboutPossibleSideEffects { get; set; } // about_possible_side_effects (length: 255)
        public string ExperienceSideEffect { get; set; } // experience_side_effect (length: 255)
        public string FollowUp { get; set; } // follow_up (length: 255)
        public string GiveTrackingCard { get; set; } // give_tracking_card (length: 255)
        public string TotalNumOfMwras { get; set; } // total_num_of_mwras (length: 255)
        public string HowManyVerifiedByTeam { get; set; } // how_many_verified_by_team (length: 255)
        public string Remarks1 { get; set; } // remarks1 (length: 255)
        public System.DateTime? Date { get; set; } // date

        public LhvChecklistPpif()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
