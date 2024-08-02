using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hands.WebAPI.Models
{
    public class LhvChecklist
    {
        public int LhvChecklistId { get; set; } // lhv_checklist_id (Primary key)

        [Required]
        public int AppUserId { get; set; } // app_user_id
        [Required]

        public int VisitorId { get; set; } // visitor_id
        [Required]

        public string VisitorName { get; set; } // visitor_name (length: 255)
      

        public System.DateTime CreatedAt { get; set; } // created_at
       // [Required]

        public string IucdInsertionKit { get; set; } // iucd_insertion_kit (length: 10)
        //[Required]

        public string MuacTape { get; set; } // muac_tape (length: 10)
        //[Required]

        public string MeasuringTape { get; set; } // measuring_tape (length: 10)

        //[Required]
        public string Stethoscope { get; set; } // stethoscope (length: 10)
        //[Required]
        public string BpApparatus { get; set; } // bp_apparatus (length: 10)
        //[Required]
        public string WeighingMachcine { get; set; } // weighing_machcine (length: 10)
        //[Required]
        public string Fetoscope { get; set; } // fetoscope (length: 10)
        //[Required]
        public string Thermometer { get; set; } // thermometer (length: 10)
        //[Required]
        public string InfectionPreventionKit { get; set; } // infection_prevention_kit (length: 10)

        //[Required]
        public string TabletWithCharger { get; set; } // tablet_with_charger (length: 10)
        //[Required]
        public string DangerBox { get; set; } // danger_box (length: 10)
        //[Required]
        public string LhvApron { get; set; } // lhv_apron (length: 10)
        //[Required]
        public string MecWheel { get; set; } // mec_wheel (length: 10)
        //[Required]
        public string IucdInsertionKitFunc { get; set; } // iucd_insertion_kit_func (length: 10)
        //[Required]
        public string BpApparatusFunc { get; set; } // bp_apparatus_func (length: 10)
        //[Required]
        public string WeighingFunc { get; set; } // weighing_func (length: 10)
        //[Required]
        public string ThermometerFunc { get; set; } // thermometer_func (length: 10)
        //[Required]
        public string InfectionPreventionKitFunc { get; set; } // infection_prevention_kit_func (length: 10)
        //[Required]
        public string TabletWithChargerFunc { get; set; } // tablet_with_charger_func (length: 10)
        //[Required]
        public string DangerBoxFunc { get; set; } // danger_box_func (length: 10)
        //[Required]
        public string LhvApronFunc { get; set; } // lhv_apron_func (length: 10)
        //[Required]
        public string ProvideCounselingAboutFp { get; set; } // provide_counseling_about_fp (length: 10)
        //[Required]
        public string OralPills { get; set; } // oral_pills (length: 10)
        //[Required]
        public string Injections { get; set; } // injections (length: 10)
        //[Required]
        public string Iud { get; set; } // iud (length: 10)

        //[Required]
        public string Condoms { get; set; } // condoms (length: 10)

        //[Required]
        public string Implant { get; set; } // implant (length: 10)
        //[Required]
        public string AnyNaturalMethod { get; set; } // any_natural_method (length: 10)
        //[Required]
        public string PermanentMethod { get; set; } // permanent_method (length: 10)

        //[Required]
        public string AnyOther { get; set; } // any_other (length: 100)
        //[Required]
        public string ProviderUseMecWheel { get; set; } // provider_use_mec_wheel (length: 10)
        //[Required]
        public string CheckBp { get; set; } // check_bp (length: 10)
        //[Required]
        public string CheckAnemiaAndJaundice { get; set; } // check_anemia_and_jaundice (length: 10)
        //[Required]
        public string MeasurementOfWeight { get; set; } // measurement_of_weight (length: 10)
        //[Required]
        public string PelvicExam { get; set; } // pelvic_exam (length: 10)
        //[Required]
        public string ClientReceiveMethodChoice { get; set; } // client_receive_method_choice (length: 10)
        //[Required]
        public string ExplainHowToUse { get; set; } // explain_how_to_use (length: 10)
        //[Required]
        public string AboutPossibleSideEffects { get; set; } // about_possible_side_effects (length: 10)
        //[Required]
        public string ExperienceSideEffect { get; set; } // experience_side_effect (length: 10)
        //[Required]
        public string FollowUp { get; set; } // follow_up (length: 10)
        //[Required]
        public string GiveTrackingCard { get; set; } // give_tracking_card (length: 10)
        //[Required]
        public string TotalNumOfMwras { get; set; } // total_num_of_mwras (length: 10)
        //[Required]
        public string HowManyVerifiedByTeam { get; set; } // how_many_verified_by_team (length: 10)
        //[Required]
        public string Remarks1 { get; set; } // remarks1 (length: 255)
        public string Date { get; set; } // date (length: 20)
        public bool IsActive { get; set; } // IsActive

        public int? ProjectId { get; set; } // ProjectId

        public Hands.Data.HandsDB.LhvChecklist GetMapLhvChecklist()
        {
            Hands.Data.HandsDB.LhvChecklist lhvChecklist = new Data.HandsDB.LhvChecklist();
            lhvChecklist.ProjectId = this.ProjectId;
            lhvChecklist.LhvChecklistId = this.LhvChecklistId;
            lhvChecklist.AppUserId = this.AppUserId;
            lhvChecklist.VisitorId = this.VisitorId;
            lhvChecklist.VisitorName = this.VisitorName;
            lhvChecklist.IucdInsertionKit = this.IucdInsertionKit;
            lhvChecklist.MuacTape = this.MuacTape;
            lhvChecklist.MeasuringTape = this.MeasuringTape;
            lhvChecklist.Stethoscope = this.Stethoscope;
            lhvChecklist.BpApparatus = this.BpApparatus;
            lhvChecklist.WeighingMachcine = this.WeighingMachcine;
            lhvChecklist.Fetoscope = this.Fetoscope;
            lhvChecklist.Thermometer = this.Thermometer;
            lhvChecklist.InfectionPreventionKit = this.InfectionPreventionKit;
            lhvChecklist.TabletWithCharger = this.TabletWithCharger;
            lhvChecklist.DangerBox = this.DangerBox;
            lhvChecklist.LhvApron = this.LhvApron;
            lhvChecklist.MecWheel = this.MecWheel;
            lhvChecklist.IucdInsertionKitFunc = this.IucdInsertionKitFunc;
            lhvChecklist.BpApparatusFunc = this.BpApparatusFunc;
            lhvChecklist.WeighingFunc = this.WeighingFunc;
            lhvChecklist.ThermometerFunc = this.ThermometerFunc;
            lhvChecklist.InfectionPreventionKitFunc = this.InfectionPreventionKitFunc;
            lhvChecklist.TabletWithChargerFunc = this.TabletWithChargerFunc;
            lhvChecklist.DangerBoxFunc = this.DangerBoxFunc;
            lhvChecklist.LhvApronFunc = this.LhvApronFunc;
            lhvChecklist.ProvideCounselingAboutFp = this.ProvideCounselingAboutFp;
            lhvChecklist.OralPills = this.OralPills;
            lhvChecklist.Injections = this.Injections;
            lhvChecklist.Iud = this.Iud;
            lhvChecklist.Condoms = this.Condoms;
            lhvChecklist.Implant = this.Implant;
            lhvChecklist.AnyNaturalMethod = this.AnyNaturalMethod;
            lhvChecklist.PermanentMethod = this.PermanentMethod;
            lhvChecklist.AnyOther = this.AnyOther;
            lhvChecklist.ProviderUseMecWheel = this.ProviderUseMecWheel;
            lhvChecklist.CheckBp = this.CheckBp;
            lhvChecklist.CheckAnemiaAndJaundice = this.CheckAnemiaAndJaundice;
            lhvChecklist.MeasurementOfWeight = this.MeasurementOfWeight;
            lhvChecklist.PelvicExam = this.PelvicExam;
            lhvChecklist.ClientReceiveMethodChoice = this.ClientReceiveMethodChoice;
            lhvChecklist.ExplainHowToUse = this.ExplainHowToUse;
            lhvChecklist.AboutPossibleSideEffects = this.AboutPossibleSideEffects;
            lhvChecklist.ExperienceSideEffect = this.ExperienceSideEffect;
            lhvChecklist.FollowUp = this.FollowUp;
            lhvChecklist.GiveTrackingCard = this.GiveTrackingCard;
            lhvChecklist.TotalNumOfMwras = this.TotalNumOfMwras;
            lhvChecklist.HowManyVerifiedByTeam = this.HowManyVerifiedByTeam;
            lhvChecklist.ExperienceSideEffect = this.ExperienceSideEffect;
            lhvChecklist.Remarks1 = this.Remarks1;
            lhvChecklist.Date = this.Date;
            lhvChecklist.IsActive = true;

            return lhvChecklist;

        }

    }
}