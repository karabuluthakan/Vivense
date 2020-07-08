﻿namespace Library.ApacheKafka.Persistence
{
    public enum PubSubEventType
    {
        LogThresholdChanged,
        TemplatePublished,
        TemplatePublishedNew,
        TaxChanged,
        TaxReplicaInCatalogChanged,
        AttributeTitleChanged,
        CategoryCreated,
        CategoryUpdated,
        CategoryDeleted,
        CategoryTitleChanged,
        CategoryStatusChanged,
        CategoryProductCountChanged,
        CategoryProductCountShareRequestResulted,
        CollectionNameChanged,
        CollectionStatusChanged,
        CommissionRateChanged,
        SupplierNameChanged,
        SchedulerJobCreationRequested,
        SchedulerJobDeletionRequested,
        SchedulerJobUpdateRequested,
        EmailCreated,
        CampaignStarted,
        KafkaHealthCheck,
        PromotionStarted,
        PromotionEnded,
        CampaignEnded,
        PromotionCalculate,
        PromotionCalculateFinal,
        EntityListCrudChanged,
        EntityListChanged,
        EntityListShareRequested,
        EntityListShareRequestResulted,
        PageViewInteractEvent,
        TemplatePublishEnded,
        CollectionDeleted,
        MarketPlacePolicyPublishChanged,
        ActivePromotionListChanged,
        ActiveCampaignChanged,
        PromotionChanged,
        CampaignChanged,
        ProductToStockCard,
        StockCardWarehouses,
        ProductCreated,
        ProductUpdated,
        ProductDeleted,
        CreateNewVehicle,
        UpdateVehicle,
        DeleteVehicle,
        CreateNewWarehouse,
        UpdateWarehouse,
        DeleteWarehouse,
        CreateNewWarehouseDriver,
        UpdateWarehouseDriver,
        DeleteWarehouseDriver,
        CreateNewWarehousePersonnel,
        UpdateWarehousePersonnel,
        DeleteWarehousePersonnel,
        SupplierApplicationApproved,
        SupplierCreated,
        SupplierChanged,
        SupplierRemoved,
        UserCreated,
        UserUpdated,
        UserDeleted,
        SupplierApplicationCreated,
        AttributeCreated,
        AttributeUpdated,
        AttributeDeleted,
        CollectionCreated,
        CollectionUpdated,
        ProductFilesUploaded,
        ProductFilesResult,
        TransactionCanceled,
        EntityCreated,
        EntityUpdated,
        EntityDeleted,
        ProductFilesInProcess,
        EntityListWithMarketPlaceChanged,
        EntityListWithMarketPlaceShareRequested,
    }
}