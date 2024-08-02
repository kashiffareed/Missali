using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hands.WebAPI.Models
{
    public class ClientChecklist
    {
        public int mwra_id { get; set; }
        public int ProjectId { get; set; } // app_user_id
        public int ClientChecklistId { get; set; } // client_checklist_id (Primary key)
        [Required]
        public int AppUserId { get; set; } // app_user_id
        [Required]
        public int VisitorId { get; set; } // visitor_id
        [Required]
        public string VisitorName { get; set; } // visitor_name (length: 255)
        //[Required]
        public System.DateTime CreatedAt { get; set; } // created_at
        //[Required]
        public string GetInformation { get; set; } // get_information (length: 10)
        //[Required]
        public string ReceivePrescribed { get; set; } // receive_prescribed (length: 10)
        //[Required]
        public string RestartContraceptiveMethod { get; set; } // restart_contraceptive_method (length: 10)
        //[Required]
        public string GetSupplier { get; set; } // get_supplier (length: 10)
        //[Required]
        public string SwitchContraceptiveMethod { get; set; } // switch_contraceptive_method (length: 10)
        //[Required]
        public string DiscussAProblem { get; set; } // discuss_a_problem (length: 10)
        //[Required]
        public string OtherNonFamily { get; set; } // other_non_family (length: 10)
        //[Required]
        public string NameOfTheMethod { get; set; } // name_of_the_method (length: 10)
        //[Required]
        public string DropDownMethodsName { get; set; } // drop_down_methods_name (length: 10)
        //[Required]
        public string ReceivedMethodOfChoice { get; set; } // received_method_of_choice (length: 10)
        //[Required]
        public string ForTheMethodYouJustAccept { get; set; } // for_the_method_you_just_accept (length: 10)
        //[Required]
        public string DidProviderDescribe { get; set; } // did_provider_describe (length: 10)
        //[Required]
        public string NameFewOfSideEffects { get; set; } // name_few_of_side_effects (length: 10)
        //[Required]
        public string WhatToDoIfProblem { get; set; } // what_to_do_if_problem (length: 10)
        //[Required]
        public string WhenFolloupVisit { get; set; } // when_folloup_visit (length: 10)
        //[Required]
        public string DidYouFeelComfortable { get; set; } // did_you_feel_comfortable (length: 10)
        //[Required]
        public string DidYouFeelInformation { get; set; } // did_you_feel_information (length: 10)
        //[Required]
        public string DidYouHaveEnoughPrivacy { get; set; } // did_you_have_enough_privacy (length: 10)
        //[Required]
        public string DuringVisitTreated { get; set; } // during_visit_treated (length: 10)
        //[Required]
        public string ProviderEncourage { get; set; } // provider_encourage (length: 10)
        //[Required]
        public string WhenReceivedMethod { get; set; } // when_received_method (length: 10)
        //[Required]
        public string ReceivedMethodOfChoiceSupport { get; set; } // received_method_of_choice_support (length: 10)
        //[Required]
        public string ForTheMethodYouJustAcceptSupport { get; set; } // for_the_method_you_just_accept_support (length: 10)
        //[Required]
        public string ProviderDescribeSideEffects { get; set; } // provider_describe_side_effects (length: 10)
        //[Required]
        public string NameSideEffects { get; set; } // name_side_effects (length: 10)
        //[Required]
        public string WhatToDoIfProblemSupport { get; set; } // what_to_do_if_problem_support (length: 10)
        //[Required]
        public string ToldWhenToVisit { get; set; } // told_when_to_visit (length: 10)
        //[Required]
        public string ExperienceAnySideEffects { get; set; } // experience_any_side_effects (length: 10)
        //[Required]
        public string SatisfyByProviderSupport { get; set; } // satisfy_by_provider_support (length: 10)
        //[Required]
        public string FeelComfortableToAskQuestions { get; set; } // feel_comfortable_to_ask_questions (length: 10)
        //[Required]
        public string EnoughPrivacyDuringExam { get; set; } // enough_privacy_during_exam (length: 10)
        //[Required]
        public string TreatedByProvider { get; set; } // treated_by_provider (length: 10)
        //[Required]
        public string EncourageAskQuestion { get; set; } // encourage_ask_question (length: 10)
        //[Required]
        public string FpNeedSpacing { get; set; } // fp_need_spacing (length: 10)

       // [Required]
        public string AdoptingFpMethod { get; set; } // adopting_fp_method (length: 10)
       // [Required]
        public string Date { get; set; } // date (length: 20)
        public bool IsActive { get; set; } // IsActive
        public Hands.Data.HandsDB.ClientChecklist GetMapclientChecklist()
        {
            Hands.Data.HandsDB.ClientChecklist clientChecklist = new Data.HandsDB.ClientChecklist();
            clientChecklist.MwraId = this.mwra_id;
            clientChecklist.ProjectId = this.ProjectId;
            clientChecklist.ClientChecklistId = this.ClientChecklistId;
            clientChecklist.AppUserId = this.AppUserId;
            clientChecklist.VisitorId = this.VisitorId;
            clientChecklist.VisitorName = this.VisitorName;
            clientChecklist.CreatedAt = this.CreatedAt;
            clientChecklist.GetInformation = this.GetInformation;
            clientChecklist.ReceivePrescribed = this.ReceivePrescribed;
            clientChecklist.RestartContraceptiveMethod = this.RestartContraceptiveMethod;
            clientChecklist.GetSupplier = this.GetSupplier;
            clientChecklist.SwitchContraceptiveMethod = this.SwitchContraceptiveMethod;
            clientChecklist.DiscussAProblem = this.DiscussAProblem;
            clientChecklist.OtherNonFamily = this.OtherNonFamily;
            clientChecklist.NameOfTheMethod = this.NameOfTheMethod;
            clientChecklist.DropDownMethodsName = this.DropDownMethodsName;
            clientChecklist.ReceivedMethodOfChoice = this.ReceivedMethodOfChoice;
            clientChecklist.ForTheMethodYouJustAccept = this.ForTheMethodYouJustAccept;
            clientChecklist.DidProviderDescribe = this.DidProviderDescribe;
            clientChecklist.NameFewOfSideEffects = this.NameFewOfSideEffects;
            clientChecklist.WhatToDoIfProblem = this.WhatToDoIfProblem;
            clientChecklist.WhenFolloupVisit = this.WhenFolloupVisit;
            clientChecklist.DidYouFeelComfortable = this.DidYouFeelComfortable;
            clientChecklist.DidYouFeelInformation = this.DidYouFeelInformation;
            clientChecklist.DidYouHaveEnoughPrivacy = this.DidYouHaveEnoughPrivacy;
            clientChecklist.DuringVisitTreated = this.DuringVisitTreated;
            clientChecklist.ProviderEncourage = this.ProviderEncourage;
            clientChecklist.WhenReceivedMethod = this.WhenReceivedMethod;
            clientChecklist.ReceivedMethodOfChoiceSupport = this.ReceivedMethodOfChoiceSupport;
            clientChecklist.ForTheMethodYouJustAcceptSupport = this.ForTheMethodYouJustAcceptSupport;
            clientChecklist.ProviderDescribeSideEffects = this.ProviderDescribeSideEffects;
            clientChecklist.NameSideEffects = this.NameSideEffects;
            clientChecklist.WhatToDoIfProblemSupport = this.WhatToDoIfProblemSupport;
            clientChecklist.ToldWhenToVisit = this.ToldWhenToVisit;
            clientChecklist.ExperienceAnySideEffects = this.ExperienceAnySideEffects;
            clientChecklist.SatisfyByProviderSupport = this.SatisfyByProviderSupport;
            clientChecklist.FeelComfortableToAskQuestions = this.FeelComfortableToAskQuestions;
            clientChecklist.EnoughPrivacyDuringExam = this.EnoughPrivacyDuringExam;
            clientChecklist.TreatedByProvider = this.TreatedByProvider;
            clientChecklist.EncourageAskQuestion = this.EncourageAskQuestion;
            clientChecklist.FpNeedSpacing = this.FpNeedSpacing;
            clientChecklist.AdoptingFpMethod = this.AdoptingFpMethod;
            clientChecklist.Date = this.Date;
            clientChecklist.IsActive= true;

            return clientChecklist;

        }
    }
}