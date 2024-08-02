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

    // noor_checklist
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.36.1.0")]
    public partial class NoorChecklistConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<NoorChecklist>
    {
        public NoorChecklistConfiguration()
            : this("dbo")
        {
        }

        public NoorChecklistConfiguration(string schema)
        {
            ToTable("noor_checklist", schema);
            HasKey(x => x.NoorChecklistId);

            Property(x => x.NoorChecklistId).HasColumnName(@"noor_checklist_id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.AppUserId).HasColumnName(@"app_user_id").HasColumnType("int").IsRequired();
            Property(x => x.CreatedAt).HasColumnName(@"created_at").HasColumnType("datetime2").IsRequired();
            Property(x => x.NoorMarkazRenovated).HasColumnName(@"noor_markaz_renovated").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.SignBoards).HasColumnName(@"sign_boards").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.ExaminationCouch).HasColumnName(@"examination_couch").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.PlasticSheet).HasColumnName(@"plastic_sheet").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.Light).HasColumnName(@"light").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.PrivacyAssurance).HasColumnName(@"privacy_assurance").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.Posters).HasColumnName(@"posters").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.SacBanner).HasColumnName(@"sac_banner").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.Remarks1).HasColumnName(@"remarks1").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.WorkerBagAvailable).HasColumnName(@"worker_bag_available").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.PhoneChargerAvailable).HasColumnName(@"phone_charger_available").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.SpeakersAvailable).HasColumnName(@"speakers_available").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.MwraBookletAvailable).HasColumnName(@"mwra_booklet_available").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.TrackingCardsAvailable).HasColumnName(@"tracking_cards_available").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.WorkerBagFunctional).HasColumnName(@"WorkerBagFunctional").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.PhoneChargerFunc).HasColumnName(@"phone_charger_func").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.SpeakersFunc).HasColumnName(@"speakers_func").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.MwraBookletFunc).HasColumnName(@"mwra_booklet_func").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.TrackingCardsFunc).HasColumnName(@"tracking_cards_func").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.Remarks2).HasColumnName(@"remarks2").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.Pills).HasColumnName(@"pills").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.Condoms).HasColumnName(@"condoms").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.Remarks3).HasColumnName(@"remarks3").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.WashingSoap).HasColumnName(@"washing_soap").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.Detergent).HasColumnName(@"detergent").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.HandWash).HasColumnName(@"hand_wash").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.BathSoap).HasColumnName(@"bath_soap").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.SanitaryNapkins).HasColumnName(@"sanitary_napkins").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.Toothpaste).HasColumnName(@"toothpaste").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.ToothBrush).HasColumnName(@"tooth_brush").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.Dettol).HasColumnName(@"dettol").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.Slippers).HasColumnName(@"slippers").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.SchoolShoesGirls).HasColumnName(@"school_shoes_girls").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.SchoolShoesBoys).HasColumnName(@"school_shoes_boys").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.NewBornKits).HasColumnName(@"new_born_kits").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.Cloths).HasColumnName(@"cloths").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.Pulses).HasColumnName(@"pulses").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.Rice).HasColumnName(@"rice").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.Sugar).HasColumnName(@"sugar").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.VegetableEdibleOil).HasColumnName(@"vegetable_edible_oil").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.Eggs).HasColumnName(@"eggs").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.Flour).HasColumnName(@"flour").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.LodizedSalt).HasColumnName(@"lodized_salt").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.SypParacetamol).HasColumnName(@"syp_paracetamol").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.TabParacetamol).HasColumnName(@"tab_paracetamol").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.SypAmoxil).HasColumnName(@"syp_amoxil").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.CapAmoxil).HasColumnName(@"cap_amoxil").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.Ors).HasColumnName(@"ors").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.TabFolicAcid).HasColumnName(@"tab_folic_acid").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.IronTablets).HasColumnName(@"iron_tablets").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.MultiVitamin).HasColumnName(@"multi_vitamin").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.TabMetronidazole).HasColumnName(@"tab_metronidazole").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.SypMetronidazole).HasColumnName(@"syp_metronidazole").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.PolyfaxSkin).HasColumnName(@"polyfax_skin").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.Saniplast).HasColumnName(@"saniplast").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.Remarks4).HasColumnName(@"remarks4").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.TotalPurchaseLastMonth).HasColumnName(@"total_purchase_last_month").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.TotalSaleLastMonth).HasColumnName(@"total_sale_last_month").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.Remarks5).HasColumnName(@"remarks5").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.AmountReceivedLastStipend).HasColumnName(@"amount_received_last_stipend").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.MonthReceivedLastStipend).HasColumnName(@"month_received_last_stipend").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.ProblemReceivingStipend).HasColumnName(@"problem_receiving_stipend").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.Remarks6).HasColumnName(@"remarks6").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.NumberOfHh).HasColumnName(@"number_of_hh").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.NumberOfVisitsLastMonth).HasColumnName(@"number_of_visits_last_month").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.Remarks7).HasColumnName(@"remarks7").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.HealthCommittee).HasColumnName(@"health_committee").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.NumHcMembers).HasColumnName(@"num_hc_members").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.MonthlyMeeting).HasColumnName(@"monthly_meeting").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.Remarks8).HasColumnName(@"remarks8").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.FpMethodPills).HasColumnName(@"fp_method_pills").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.FpMethodCondoms).HasColumnName(@"fp_method_condoms").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.FpMethodInjectables).HasColumnName(@"fp_method_injectables").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.FpMethodIucd).HasColumnName(@"fp_method_iucd").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.FpMethodTl).HasColumnName(@"fp_method_tl").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.FpMethodImplant).HasColumnName(@"fp_method_implant").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.KnowAppropriateFacility).HasColumnName(@"know_appropriate_facility").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.KnowLocationUcHcp).HasColumnName(@"know_location_uc_hcp").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.EnlistEnrollMwra).HasColumnName(@"enlist_enroll_mwra").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.SpaceProvideNoorMarkaz).HasColumnName(@"space_provide_noor_markaz").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.ConductCouncel).HasColumnName(@"conduct_councel").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.ConductFamilySession).HasColumnName(@"conduct_family_session").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.EnlistPotentialClients).HasColumnName(@"enlist_potential_clients").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.FacilitateLhvArrangingClinic).HasColumnName(@"facilitate_lhv_arranging_clinic").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.FpSuppliesCondoms).HasColumnName(@"fp_supplies_condoms").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.FollowupClientsThroughCards).HasColumnName(@"followup_clients_through_cards").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.InformLhvIfSideEffect).HasColumnName(@"inform_lhv_if_side_effect").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.ReassureTillLhvArrives).HasColumnName(@"reassure_till_lhv_arrives").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.FacilitateLhvMaintain).HasColumnName(@"facilitate_lhv_maintain").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.ManageHygieneProducts).HasColumnName(@"manage_hygiene_products").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.SocialMarketPromotions).HasColumnName(@"social_market_promotions").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.KeepSaleRecord).HasColumnName(@"keep_sale_record").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.WithLhvSupport).HasColumnName(@"with_lhv_support").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.ConductMuacForPlw).HasColumnName(@"conduct_muac_for_plw").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.ConductMuacForChildren).HasColumnName(@"conduct_muac_for_children").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.SupportGroupFormation).HasColumnName(@"SupportGroupFormation").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.Remarks9).HasColumnName(@"remarks9").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.KnowSessionOnMobile).HasColumnName(@"know_session_on_mobile").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.MazbotBandi).HasColumnName(@"mazbot_bandi").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.SehatKhushi).HasColumnName(@"sehat_khushi").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.SahiWaqtSahiFaisla).HasColumnName(@"sahi_waqt_sahi_faisla").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.KhandaniMansobaBandi).HasColumnName(@"khandani_mansoba_bandi").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.KhandaniMansobaKadeem).HasColumnName(@"khandani_mansoba_kadeem").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.BachonKiPedaish).HasColumnName(@"bachon_ki_pedaish").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.MustaqilTariqay).HasColumnName(@"mustaqil_tariqay").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.KhawateenIucd).HasColumnName(@"khawateen_iucd").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.KhandaniMansobaBandiTikay).HasColumnName(@"khandani_mansoba_bandi_tikay").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.KhandaniMansobaBandiImplant).HasColumnName(@"khandani_mansoba_bandi_implant").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.KhandaniMansobaBandiSawalat).HasColumnName(@"khandani_mansoba_bandi_sawalat").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.Remarks10).HasColumnName(@"remarks10").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.LhvId).HasColumnName(@"lhv_id").HasColumnType("int").IsOptional();
            Property(x => x.VisitorName).HasColumnName(@"visitor_name").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.Date).HasColumnName(@"date").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(50);
            Property(x => x.VisitorId).HasColumnName(@"visitor_id").HasColumnType("int").IsOptional();
            Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsRequired();
            Property(x => x.ProjectId).HasColumnName(@"ProjectId").HasColumnType("int").IsOptional();
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
