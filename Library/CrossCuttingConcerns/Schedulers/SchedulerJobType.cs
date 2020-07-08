namespace Library.CrossCuttingConcerns.Schedulers
{
    public enum SchedulerJobType
    {
        ResetPassword,
        NewPassword,
        PromotionStarted,
        CampaignStarted,
        PromotionEnded,
        CampaignEnded,
        PublishTemplate,
        PublishTemplateEnded,
        ProductCountByCategory,
        ProductFileUpload
    }
}