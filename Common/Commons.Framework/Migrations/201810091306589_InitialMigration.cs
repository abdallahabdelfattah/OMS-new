namespace Commons.Framework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "common.ActivationCode",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        UserId = c.Guid(nullable: false),
                        AttemptCount = c.Int(nullable: false),
                        Code = c.Int(nullable: false),
                        SentDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false, maxLength: 256),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                        UpdatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "common.AttachmentType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AllowedFilesExtension = c.String(maxLength: 200),
                        Code = c.String(nullable: false, maxLength: 100),
                        CreatedBy = c.String(nullable: false, maxLength: 256),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ImageMaxHeight = c.Int(),
                        ImageMaxWidth = c.Int(),
                        IsImage = c.Boolean(nullable: false),
                        IsMandatory = c.Boolean(nullable: false),
                        MaxSizeInMegabytes = c.Int(nullable: false),
                        NameAr = c.String(nullable: false, maxLength: 200),
                        NameEn = c.String(nullable: false, maxLength: 100),
                        UpdatedBy = c.String(maxLength: 256),
                        UpdatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "common.Log",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Application = c.String(nullable: false, maxLength: 60),
                        Host = c.String(maxLength: 50),
                        Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Thread = c.String(nullable: false, maxLength: 255, unicode: false),
                        Level = c.String(nullable: false, maxLength: 50, unicode: false),
                        Logger = c.String(nullable: false, maxLength: 255, unicode: false),
                        Url = c.String(maxLength: 500, unicode: false),
                        StatusCode = c.Int(),
                        Browser = c.String(maxLength: 255, unicode: false),
                        User = c.String(maxLength: 255, unicode: false),
                        Message = c.String(nullable: false, maxLength: 4000, unicode: false),
                        Exception = c.String(maxLength: 6000, unicode: false),
                        ExceptionType = c.String(maxLength: 255, unicode: false),
                        ExceptionData = c.String(maxLength: 500, unicode: false),
                        AllXml = c.String(maxLength: 6000, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "common.Navigation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationId = c.String(nullable: false, maxLength: 100),
                        NameAr = c.String(nullable: false, maxLength: 100),
                        NameEn = c.String(nullable: false, maxLength: 100),
                        LinkUrl = c.String(maxLength: 500),
                        ParentId = c.Int(),
                        Roles = c.String(maxLength: 500),
                        ItemOrder = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false, maxLength: 256),
                        UpdatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("common.Navigation", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "common.NotificationQueue",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Subject = c.String(maxLength: 256),
                        Content = c.String(nullable: false),
                        NotificationTypeId = c.Byte(nullable: false),
                        NotificationSendStatusId = c.Byte(nullable: false),
                        SentOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Sender = c.String(maxLength: 256),
                        Recipients = c.String(maxLength: 500),
                        RetryCount = c.Byte(),
                        BaseRequestId = c.Guid(),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false, maxLength: 256),
                        UpdatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("common.NotificationSendStatus", t => t.NotificationSendStatusId)
                .ForeignKey("common.NotificationType", t => t.NotificationTypeId)
                .Index(t => t.NotificationTypeId)
                .Index(t => t.NotificationSendStatusId);
            
            CreateTable(
                "common.NotificationSendStatus",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        NameAr = c.String(nullable: false, maxLength: 256),
                        NameEn = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "common.NotificationType",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        NameAr = c.String(nullable: false, maxLength: 256),
                        NameEn = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "common.NotificationTemplate",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationId = c.String(nullable: false, maxLength: 50),
                        Key = c.String(nullable: false, maxLength: 100),
                        Value = c.String(nullable: false, maxLength: 4000),
                        NotificationTypeId = c.Byte(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 255),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 255),
                        UpdatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("common.NotificationType", t => t.NotificationTypeId)
                .Index(t => t.NotificationTypeId);
            
            CreateTable(
                "common.SystemSettings",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ApplicationId = c.String(maxLength: 100),
                        Secure = c.Boolean(nullable: false),
                        Key = c.String(nullable: false, maxLength: 100),
                        ValueType = c.String(nullable: false, maxLength: 30, unicode: false),
                        Value = c.String(nullable: false, maxLength: 255),
                        GroupName = c.String(nullable: false, maxLength: 50),
                        IsSticky = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 255),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 255),
                        UpdatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("common.NotificationTemplate", "NotificationTypeId", "common.NotificationType");
            DropForeignKey("common.NotificationQueue", "NotificationTypeId", "common.NotificationType");
            DropForeignKey("common.NotificationQueue", "NotificationSendStatusId", "common.NotificationSendStatus");
            DropForeignKey("common.Navigation", "ParentId", "common.Navigation");
            DropIndex("common.NotificationTemplate", new[] { "NotificationTypeId" });
            DropIndex("common.NotificationQueue", new[] { "NotificationSendStatusId" });
            DropIndex("common.NotificationQueue", new[] { "NotificationTypeId" });
            DropIndex("common.Navigation", new[] { "ParentId" });
            DropTable("common.SystemSettings");
            DropTable("common.NotificationTemplate");
            DropTable("common.NotificationType");
            DropTable("common.NotificationSendStatus");
            DropTable("common.NotificationQueue");
            DropTable("common.Navigation");
            DropTable("common.Log");
            DropTable("common.AttachmentType");
            DropTable("common.ActivationCode");
        }
    }
}
