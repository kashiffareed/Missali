using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hands.WebAPI.Models
{
    public class NoorChecklist
    {
        public int? ProjectId { get; set; } // ProjectId
        public int NoorChecklistId { get; set; } // noor_checklist_id (Primary key)
        [Required]
        public int AppUserId { get; set; } // app_user_id
       // [Required]
        public System.DateTime CreatedAt { get; set; } // created_at
        //[Required]
        public string NoorMarkazRenovated { get; set; } // noor_markaz_renovated (length: 10)
        //[Required]
        public string SignBoards { get; set; } // sign_boards (length: 10)
        //[Required]
        public string ExaminationCouch { get; set; } // examination_couch (length: 10)
        //[Required]
        public string PlasticSheet { get; set; } // plastic_sheet (length: 10)
        //[Required]
        public string Light { get; set; } // light (length: 10)
        //[Required]
        public string PrivacyAssurance { get; set; } // privacy_assurance (length: 20)
        //[Required]
        public string Posters { get; set; } // posters (length: 10)
        //[Required]
        public string SacBanner { get; set; } // sac_banner (length: 10)
        //[Required]
        public string Remarks1 { get; set; } // remarks1 (length: 255)
        //[Required]
        public string WorkerBagAvailable { get; set; } // worker_bag_available (length: 10)
        //[Required]
        public string PhoneChargerAvailable { get; set; } // phone_charger_available (length: 10)
        //[Required]
        public string SpeakersAvailable { get; set; } // speakers_available (length: 10)
        //[Required]
        public string MwraBookletAvailable { get; set; } // mwra_booklet_available (length: 10)
        //[Required]
        public string TrackingCardsAvailable { get; set; } // tracking_cards_available (length: 10)
        //[Required]
        public string WorkerBagFunctional { get; set; } // WorkerBagFunctional (length: 10)
        //[Required]
        public string PhoneChargerFunc { get; set; } // phone_charger_func (length: 10)
        //[Required]
        public string SpeakersFunc { get; set; } // speakers_func (length: 10)
        //[Required]
        public string MwraBookletFunc { get; set; } // mwra_booklet_func (length: 10)
        //[Required]
        public string TrackingCardsFunc { get; set; } // tracking_cards_func (length: 10)
        //[Required]
        public string Remarks2 { get; set; } // remarks2 (length: 255)
        //[Required]
        public string Pills { get; set; } // pills (length: 10)
        //[Required]
        public string Condoms { get; set; } // condoms (length: 10)
        //[Required]
        public string Remarks3 { get; set; } // remarks3 (length: 255)
        //[Required]
        public string WashingSoap { get; set; } // washing_soap (length: 10)
        //[Required]
        public string Detergent { get; set; } // detergent (length: 10)
        //[Required]
        public string HandWash { get; set; } // hand_wash (length: 10)
        //[Required]
        public string BathSoap { get; set; } // bath_soap (length: 10)
        //[Required]
        public string SanitaryNapkins { get; set; } // sanitary_napkins (length: 10)

        //[Required]
        public string Toothpaste { get; set; } // toothpaste (length: 10)
        //[Required]
        public string ToothBrush { get; set; } // tooth_brush (length: 10)
        //[Required]
        public string Dettol { get; set; } // dettol (length: 10)
        //[Required]
        public string Slippers { get; set; } // slippers (length: 10)
        //[Required]
        public string SchoolShoesGirls { get; set; } // school_shoes_girls (length: 10)
        //[Required]
        public string SchoolShoesBoys { get; set; } // school_shoes_boys (length: 10)
        //[Required]
        public string NewBornKits { get; set; } // new_born_kits (length: 10)
        //[Required]
        public string Cloths { get; set; } // cloths (length: 10)
        //[Required]
        public string Pulses { get; set; } // pulses (length: 10)
        //[Required]
        public string Rice { get; set; } // rice (length: 10)
        //[Required]
        public string Sugar { get; set; } // sugar (length: 10)
        //[Required]
        public string VegetableEdibleOil { get; set; } // vegetable_edible_oil (length: 10)
        //[Required]
        public string Eggs { get; set; } // eggs (length: 10)
        //[Required]
        public string Flour { get; set; } // flour (length: 10)
        //[Required]
        public string LodizedSalt { get; set; } // lodized_salt (length: 10)
        //[Required]
        public string SypParacetamol { get; set; } // syp_paracetamol (length: 10)
        //[Required]
        public string TabParacetamol { get; set; } // tab_paracetamol (length: 10)
        //[Required]
        public string SypAmoxil { get; set; } // syp_amoxil (length: 10)
        //[Required]
        public string CapAmoxil { get; set; } // cap_amoxil (length: 10)
        //[Required]
        public string Ors { get; set; } // ors (length: 10)
        //[Required]
        public string TabFolicAcid { get; set; } // tab_folic_acid (length: 10)
        //[Required]
        public string IronTablets { get; set; } // iron_tablets (length: 10)
        //[Required]
        public string MultiVitamin { get; set; } // multi_vitamin (length: 10)
        //[Required]
        public string TabMetronidazole { get; set; } // tab_metronidazole (length: 10)
        //[Required]
        public string SypMetronidazole { get; set; } // syp_metronidazole (length: 10)
        //[Required]
        public string PolyfaxSkin { get; set; } // polyfax_skin (length: 10)
        //[Required]
        public string Saniplast { get; set; } // saniplast (length: 10)
        //[Required]
        public string Remarks4 { get; set; } // remarks4 (length: 255)
        //[Required]
        public string TotalPurchaseLastMonth { get; set; } // total_purchase_last_month (length: 10)
        //[Required]
        public string TotalSaleLastMonth { get; set; } // total_sale_last_month (length: 10)
        //[Required]
        public string Remarks5 { get; set; } // remarks5 (length: 255)
        //[Required]
        public string AmountReceivedLastStipend { get; set; } // amount_received_last_stipend (length: 10)
        //[Required]
        public string MonthReceivedLastStipend { get; set; } // month_received_last_stipend (length: 10)
        //[Required]
        public string ProblemReceivingStipend { get; set; } // problem_receiving_stipend (length: 10)
        //[Required]
        public string Remarks6 { get; set; } // remarks6 (length: 255)
        //[Required]
        public string NumberOfHh { get; set; } // number_of_hh (length: 10)
        //[Required]
        public string NumberOfVisitsLastMonth { get; set; } // number_of_visits_last_month (length: 10)
        //[Required]
        public string Remarks7 { get; set; } // remarks7 (length: 255)
        //[Required]
        public string HealthCommittee { get; set; } // health_committee (length: 10)
        //[Required]
        public string NumHcMembers { get; set; } // num_hc_members (length: 10)
        //[Required]
        public string MonthlyMeeting { get; set; } // monthly_meeting (length: 10)
        //[Required]
        public string Remarks8 { get; set; } // remarks8 (length: 255)
        //[Required]
        public string FpMethodPills { get; set; } // fp_method_pills (length: 10)
        //[Required]
        public string FpMethodCondoms { get; set; } // fp_method_condoms (length: 10)
        //[Required]
        public string FpMethodInjectables { get; set; } // fp_method_injectables (length: 10)
        //[Required]
        public string FpMethodIucd { get; set; } // fp_method_iucd (length: 10)
        //[Required]
        public string FpMethodTl { get; set; } // fp_method_tl (length: 10)
        //[Required]
        public string FpMethodImplant { get; set; } // fp_method_implant (length: 10)
        //[Required]
        public string KnowAppropriateFacility { get; set; } // know_appropriate_facility (length: 10)
        //[Required]
        public string KnowLocationUcHcp { get; set; } // know_location_uc_hcp (length: 10)
        //[Required]
        public string EnlistEnrollMwra { get; set; } // enlist_enroll_mwra (length: 10)
        //[Required]
        public string SpaceProvideNoorMarkaz { get; set; } // space_provide_noor_markaz (length: 10)
        //[Required]
        public string ConductCouncel { get; set; } // conduct_councel (length: 10)
        //[Required]
        public string ConductFamilySession { get; set; } // conduct_family_session (length: 10)
        //[Required]
        public string EnlistPotentialClients { get; set; } // enlist_potential_clients (length: 10)
        //[Required]
        public string FacilitateLhvArrangingClinic { get; set; } // facilitate_lhv_arranging_clinic (length: 10)
        //[Required]
        public string FpSuppliesCondoms { get; set; } // fp_supplies_condoms (length: 10)
        //[Required]
        public string FollowupClientsThroughCards { get; set; } // followup_clients_through_cards (length: 10)
        //[Required]
        public string InformLhvIfSideEffect { get; set; } // inform_lhv_if_side_effect (length: 10)
        //[Required]
        public string ReassureTillLhvArrives { get; set; } // reassure_till_lhv_arrives (length: 10)
        //[Required]
        public string FacilitateLhvMaintain { get; set; } // facilitate_lhv_maintain (length: 10)
        //[Required]
        public string ManageHygieneProducts { get; set; } // manage_hygiene_products (length: 10)
        //[Required]
        public string SocialMarketPromotions { get; set; } // social_market_promotions (length: 10)
        //[Required]
        public string KeepSaleRecord { get; set; } // keep_sale_record (length: 10)
        //[Required]
        public string WithLhvSupport { get; set; } // with_lhv_support (length: 10)
        //[Required]
        public string ConductMuacForPlw { get; set; } // conduct_muac_for_plw (length: 10)
        //[Required]
        public string ConductMuacForChildren { get; set; } // conduct_muac_for_children (length: 10)
        //[Required]
        public string SupportGroupFormation { get; set; } // SupportGroupFormation (length: 10)
        //[Required]
        public string Remarks9 { get; set; } // remarks9 (length: 255)
        //[Required]
        public string KnowSessionOnMobile { get; set; } // know_session_on_mobile (length: 10)
        //[Required]
        public string MazbotBandi { get; set; } // mazbot_bandi (length: 10)
        //[Required]
        public string SehatKhushi { get; set; } // sehat_khushi (length: 10)
        //[Required]
        public string SahiWaqtSahiFaisla { get; set; } // sahi_waqt_sahi_faisla (length: 10)
        //[Required]
        public string KhandaniMansobaBandi { get; set; } // khandani_mansoba_bandi (length: 10)
        //[Required]
        public string KhandaniMansobaKadeem { get; set; } // khandani_mansoba_kadeem (length: 10)
        //[Required]
        public string BachonKiPedaish { get; set; } // bachon_ki_pedaish (length: 10)
        //[Required]
        public string MustaqilTariqay { get; set; } // mustaqil_tariqay (length: 10)
        //[Required]
        public string KhawateenIucd { get; set; } // khawateen_iucd (length: 10)
        //[Required]
        public string KhandaniMansobaBandiTikay { get; set; } // khandani_mansoba_bandi_tikay (length: 10)
        //[Required]
        public string KhandaniMansobaBandiImplant { get; set; } // khandani_mansoba_bandi_implant (length: 10)
        //[Required]
        public string KhandaniMansobaBandiSawalat { get; set; } // khandani_mansoba_bandi_sawalat (length: 10)
        //[Required]
        public string Remarks10 { get; set; } // remarks10 (length: 255)
        //[Required]
        public int? LhvId { get; set; } // lhv_id
        [Required]
        public string VisitorName { get; set; } // visitor_name (length: 255)
        public string Date { get; set; } // date (length: 50)
        [Required]
        public int? VisitorId { get; set; } // visitor_id

        public bool IsActive { get; set; } // IsActive
        public Hands.Data.HandsDB.NoorChecklist GetMapclientNoorChecklist()
        {
            Hands.Data.HandsDB.NoorChecklist noorChecklist = new Data.HandsDB.NoorChecklist();
            noorChecklist.ProjectId = ProjectId;
            noorChecklist.NoorChecklistId = this.NoorChecklistId;
            noorChecklist.AppUserId = this.AppUserId;
            noorChecklist.CreatedAt = this.CreatedAt;
            noorChecklist.NoorMarkazRenovated = this.NoorMarkazRenovated;
            noorChecklist.SignBoards = this.SignBoards;
            noorChecklist.ExaminationCouch = this.ExaminationCouch;
            noorChecklist.PlasticSheet = this.PlasticSheet;
            noorChecklist.Light = this.Light;
            noorChecklist.PrivacyAssurance = this.PrivacyAssurance;
            noorChecklist.Posters = this.Posters;
            noorChecklist.SacBanner = this.SacBanner;
            noorChecklist.Remarks1 = this.Remarks1;
            noorChecklist.WorkerBagAvailable = this.WorkerBagAvailable;
            noorChecklist.PhoneChargerAvailable = this.PhoneChargerAvailable;
            noorChecklist.SpeakersAvailable = this.SpeakersAvailable;
            noorChecklist.MwraBookletAvailable = this.MwraBookletAvailable;
            noorChecklist.TrackingCardsAvailable = this.TrackingCardsAvailable;
            noorChecklist.WorkerBagFunctional = this.WorkerBagFunctional;
            noorChecklist.PhoneChargerFunc = this.PhoneChargerFunc;
            noorChecklist.SpeakersFunc = this.SpeakersFunc;
            noorChecklist.MwraBookletFunc = this.MwraBookletFunc;
            noorChecklist.TrackingCardsFunc = this.TrackingCardsFunc;
            noorChecklist.Remarks2 = this.Remarks2;
            noorChecklist.Pills = this.Pills;
            noorChecklist.Condoms = this.Condoms;
            noorChecklist.Remarks3 = this.Remarks3;
            noorChecklist.WashingSoap = this.WashingSoap;
            noorChecklist.Detergent = this.Detergent;
            noorChecklist.HandWash = this.HandWash;
            noorChecklist.BathSoap = this.BathSoap;
            noorChecklist.SanitaryNapkins = this.SanitaryNapkins;
            noorChecklist.Toothpaste = this.Toothpaste;
            noorChecklist.ToothBrush = this.ToothBrush;
            noorChecklist.Dettol = this.Dettol;
            noorChecklist.Slippers = this.Slippers;
            noorChecklist.SchoolShoesGirls = this.SchoolShoesGirls;
            noorChecklist.SchoolShoesBoys = this.SchoolShoesBoys;
            noorChecklist.NewBornKits = this.NewBornKits;
            noorChecklist.Cloths = this.Cloths;
            noorChecklist.Pulses = this.Pulses;
            noorChecklist.Rice = this.Rice;
            noorChecklist.Sugar = this.Sugar;
            noorChecklist.VegetableEdibleOil = this.VegetableEdibleOil;
            noorChecklist.Eggs = this.Eggs;
            noorChecklist.Flour = this.Flour;
            noorChecklist.LodizedSalt = this.LodizedSalt;
            noorChecklist.SypParacetamol = this.SypParacetamol;
            noorChecklist.TabParacetamol = this.TabParacetamol;
            noorChecklist.SypAmoxil = this.SypAmoxil;
            noorChecklist.CapAmoxil = this.CapAmoxil;
            noorChecklist.Ors = this.Ors;
            noorChecklist.TabFolicAcid = this.TabFolicAcid;
            noorChecklist.IronTablets = this.IronTablets;
            noorChecklist.MultiVitamin = this.MultiVitamin;
            noorChecklist.TabMetronidazole = this.TabMetronidazole;
            noorChecklist.SypMetronidazole = this.SypMetronidazole;
            noorChecklist.PolyfaxSkin = this.PolyfaxSkin;
            noorChecklist.Saniplast = this.Saniplast;
            noorChecklist.Remarks4 = this.Remarks4;
            noorChecklist.TotalPurchaseLastMonth = this.TotalPurchaseLastMonth;
            noorChecklist.TotalSaleLastMonth = this.TotalSaleLastMonth;
            noorChecklist.Remarks5 = this.Remarks5;
            noorChecklist.AmountReceivedLastStipend = this.AmountReceivedLastStipend;
            noorChecklist.MonthReceivedLastStipend = this.MonthReceivedLastStipend;
            noorChecklist.ProblemReceivingStipend = this.ProblemReceivingStipend;
            noorChecklist.Remarks6 = this.Remarks6;
            noorChecklist.NumberOfHh = this.NumberOfHh;
            noorChecklist.NumberOfVisitsLastMonth = this.NumberOfVisitsLastMonth;
            noorChecklist.Remarks7 = this.Remarks7;
            noorChecklist.HealthCommittee = this.HealthCommittee;
            noorChecklist.NumHcMembers = this.NumHcMembers;
            noorChecklist.MonthlyMeeting = this.MonthlyMeeting;
            noorChecklist.Remarks8 = this.Remarks8;
            noorChecklist.FpMethodPills = this.FpMethodPills;
            noorChecklist.FpMethodCondoms = this.FpMethodCondoms;
            noorChecklist.FpMethodInjectables = this.FpMethodInjectables;
            noorChecklist.FpMethodIucd = this.FpMethodIucd;
            noorChecklist.FpMethodTl = this.FpMethodTl;
            noorChecklist.FpMethodImplant = this.FpMethodImplant;
            noorChecklist.KnowAppropriateFacility = this.KnowAppropriateFacility;
            noorChecklist.KnowLocationUcHcp = this.KnowLocationUcHcp;
            noorChecklist.EnlistEnrollMwra = this.EnlistEnrollMwra;
            noorChecklist.SpaceProvideNoorMarkaz = this.SpaceProvideNoorMarkaz;
            noorChecklist.ConductCouncel = this.ConductCouncel;
            noorChecklist.ConductFamilySession = this.ConductFamilySession;
            noorChecklist.EnlistPotentialClients = this.EnlistPotentialClients;
            noorChecklist.FacilitateLhvArrangingClinic = this.FacilitateLhvArrangingClinic;
            noorChecklist.FpSuppliesCondoms = this.FpSuppliesCondoms;
            noorChecklist.FollowupClientsThroughCards = this.FollowupClientsThroughCards;
            noorChecklist.InformLhvIfSideEffect = this.InformLhvIfSideEffect;
            noorChecklist.ReassureTillLhvArrives = this.ReassureTillLhvArrives;
            noorChecklist.FacilitateLhvMaintain = this.FacilitateLhvMaintain;
            noorChecklist.ManageHygieneProducts = this.ManageHygieneProducts;
            noorChecklist.SocialMarketPromotions = this.SocialMarketPromotions;
            noorChecklist.KeepSaleRecord = this.KeepSaleRecord;
            noorChecklist.WithLhvSupport = this.WithLhvSupport;
            noorChecklist.ConductMuacForPlw = this.ConductMuacForPlw;
            noorChecklist.ConductMuacForChildren = this.ConductMuacForChildren;
            noorChecklist.SupportGroupFormation = this.SupportGroupFormation;
            noorChecklist.Remarks9 = this.Remarks9;
            noorChecklist.KnowSessionOnMobile = this.KnowSessionOnMobile;
            noorChecklist.MazbotBandi = this.MazbotBandi;
            noorChecklist.SehatKhushi = this.SehatKhushi;
            noorChecklist.SahiWaqtSahiFaisla = this.SahiWaqtSahiFaisla;
            noorChecklist.KhandaniMansobaBandi = this.KhandaniMansobaBandi;
            noorChecklist.KhandaniMansobaKadeem = this.KhandaniMansobaKadeem;
            noorChecklist.BachonKiPedaish = this.BachonKiPedaish;
            noorChecklist.MustaqilTariqay = this.MustaqilTariqay;
            noorChecklist.KhawateenIucd = this.KhawateenIucd;
            noorChecklist.KhandaniMansobaBandiTikay = this.KhandaniMansobaBandiTikay;
            noorChecklist.KhandaniMansobaBandiImplant = this.KhandaniMansobaBandiImplant;
            noorChecklist.KhandaniMansobaBandiSawalat = this.KhandaniMansobaBandiSawalat;
            noorChecklist.Remarks10 = this.Remarks10;
            noorChecklist.LhvId = this.LhvId;
            noorChecklist.VisitorName = this.VisitorName;
            noorChecklist.Date = this.Date;
            noorChecklist.VisitorId = this.VisitorId;
            noorChecklist.IsActive = true;

            return noorChecklist;

        }
    }
}