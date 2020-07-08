using System;
using System.Collections.Generic;
using Library.Models.Helpers;
using Library.Models.Lookups;
using Library.Utilities.Uploads;

namespace Library.ApacheKafka.Events.SupplierApplication
{
    public class SupplierApplicationEventModel
    {
        public string Id { get; set; }
        public List<StatusChangeEventModel> StatusHistory { get; set; } = new List<StatusChangeEventModel>();
        public ApplicationEventStatus CurrentStatus { get; set; } = ApplicationEventStatus.New;
        public SupplierStepEventModel Steps { get; set; }
    }

    public class SupplierStepEventModel
    {
        public StepIntroEventModel Intro { get; set; }
        public StepQuestionsEventModel Questions { get; set; }
        public StepCompanyInformationEventModel CompanyInformation { get; set; }
        public StepContactInformationEventModel ContactInformation { get; set; }
        public StepContractEventModel Contract { get; set; }
    }

    public abstract class StepBaseEventModel {
        public string Step { get; set; }
        public bool IsCompleted { get; set; } = false;
    }

    public class StepIntroEventModel : StepBaseEventModel
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CompanyName { get; set; }
    }

    public class StepQuestionsEventModel : StepBaseEventModel
    {
        public List<QuestionsEventModel> Questions { get; set; } = new List<QuestionsEventModel>();
    }

    public class QuestionsEventModel
    {
        public string Name { get; set; }
        public QuestionEventType Type { get; set; }
        public List<string> Answers { get; set; } = new List<string>();
    }

    public enum QuestionEventType
    {
        Multiple,
        Single,
        FreeText
    }

    public class StepCompanyInformationEventModel : StepBaseEventModel
    {
        public TaxInfo TaxInfo { get; set; }
        public string WebSiteUrl { get; set; }
        public List<Address> Addresses { get; set; } = new List<Address>();
    }

    public class StepContactInformationEventModel : StepBaseEventModel
    {
        public List<ContactInfo> Contacts { get; set; } = new List<ContactInfo>();

    }

    public class StepContractEventModel : StepBaseEventModel
    {
        public string ContractDocumentUrl { get; set; }
        public List<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();
        public List<UploadedFileInfo> ContractMedias { get; set; } = new List<UploadedFileInfo>();
        public List<UploadedFileInfo> TaxMedias { get; set; } = new List<UploadedFileInfo>();
        public List<UploadedFileInfo> CommercialRegistrationMedias { get; set; } = new List<UploadedFileInfo>();
        public List<UploadedFileInfo> ChamberRegistrationMedias { get; set; } = new List<UploadedFileInfo>();
        public List<UploadedFileInfo> SignatureCircularMedias { get; set; } = new List<UploadedFileInfo>();
    }

    public class StatusChangeEventModel
    {
        public UserSummary UpdatedBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public ApplicationEventStatus UpdatedStatus { get; set; }

        public string Message { get; set; }
    }

    public enum ApplicationEventStatus
    {
        New,
        Approved,
        Rejected,
        Hold
    }
}