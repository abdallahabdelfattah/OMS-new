
ALTER DATABASE [OMS-Dev] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OMS-Dev].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OMS-Dev] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OMS-Dev] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OMS-Dev] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OMS-Dev] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OMS-Dev] SET ARITHABORT OFF 
GO
ALTER DATABASE [OMS-Dev] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OMS-Dev] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OMS-Dev] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OMS-Dev] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OMS-Dev] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OMS-Dev] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OMS-Dev] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OMS-Dev] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OMS-Dev] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OMS-Dev] SET  DISABLE_BROKER 
GO
ALTER DATABASE [OMS-Dev] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OMS-Dev] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OMS-Dev] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OMS-Dev] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OMS-Dev] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OMS-Dev] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OMS-Dev] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OMS-Dev] SET RECOVERY FULL 
GO
ALTER DATABASE [OMS-Dev] SET  MULTI_USER 
GO
ALTER DATABASE [OMS-Dev] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OMS-Dev] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OMS-Dev] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OMS-Dev] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OMS-Dev] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [OMS-Dev] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'OMS-Dev', N'ON'
GO
ALTER DATABASE [OMS-Dev] SET QUERY_STORE = OFF
GO
USE [OMS-Dev]
GO
/****** Object:  Schema [common]    Script Date: 10/23/2021 8:32:33 PM ******/
CREATE SCHEMA [common]
GO
/****** Object:  Schema [lookup]    Script Date: 10/23/2021 8:32:33 PM ******/
CREATE SCHEMA [lookup]
GO
/****** Object:  Schema [MedicalLens]    Script Date: 10/23/2021 8:32:33 PM ******/
CREATE SCHEMA [MedicalLens]
GO
/****** Object:  Table [dbo].[Warehouse]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Warehouse](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[NameAr] [nvarchar](200) NULL,
	[NameEn] [nvarchar](200) NULL,
	[WarehouseType] [tinyint] NULL,
	[Address] [nvarchar](500) NULL,
	[EmployeeId] [bigint] NULL,
	[RegionId] [bigint] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_Warehouse] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NULL,
	[NameAr] [nvarchar](200) NULL,
	[NameEn] [nvarchar](200) NULL,
	[OfficialPrice] [decimal](18, 2) NULL,
	[MinQty] [int] NULL,
	[Qty] [int] NULL,
	[CategoryId] [bigint] NULL,
	[SupplierId] [bigint] NULL,
	[BrandId] [bigint] NULL,
	[ModelId] [bigint] NULL,
	[ColorId] [bigint] NULL,
	[MaterialId] [bigint] NULL,
	[Grade] [nvarchar](150) NULL,
	[Power] [nvarchar](150) NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WarehouseTransaction]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WarehouseTransaction](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[DocNo] [nvarchar](50) NULL,
	[EmployeeName] [nvarchar](350) NULL,
	[TransactionType] [tinyint] NULL,
	[Status] [tinyint] NOT NULL,
	[Notes] [nvarchar](500) NULL,
	[CustomerId] [bigint] NULL,
	[SupplierId] [bigint] NULL,
	[WarehouseId] [bigint] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_WarehouseTransaction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WarehouseTransactionDetail]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WarehouseTransactionDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Qty] [int] NULL,
	[ProductId] [bigint] NULL,
	[MedicalLensIMasterd] [bigint] NULL,
	[TransactionId] [bigint] NULL,
 CONSTRAINT [PK_WarehouseTransactionDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[WarehouseProductsView]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[WarehouseProductsView]
AS
SELECT w.Id AS WarehouseID, w.NameAr AS WarehouseAr, w.NameEn AS WarehouseEn, p.Id AS ProductID, p.NameAr AS ProductAr, p.NameEn AS ProductEn, 
                  ISNULL(SUM(CASE wo.[TransactionType] WHEN 1 THEN wt.qty WHEN 2 THEN - wt.qty ELSE 0 END), 0) AS total
FROM     dbo.Warehouse AS w INNER JOIN
                  dbo.WarehouseTransaction AS wo ON w.Id = wo.WarehouseId INNER JOIN
                  dbo.WarehouseTransactionDetail AS wt ON wo.Id = wt.TransactionId INNER JOIN
                  dbo.Product AS p ON p.Id = wt.ProductId
GROUP BY w.Id, w.NameAr, w.NameEn, p.NameAr, p.NameEn, p.Id
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NULL,
	[NameAr] [nvarchar](200) NULL,
	[NameEn] [nvarchar](200) NULL,
	[SalesManName] [nvarchar](250) NULL,
	[Phone] [nvarchar](20) NULL,
	[Mobile] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[FaxNo] [nvarchar](20) NULL,
	[Address] [nvarchar](400) NULL,
	[Balance] [decimal](18, 2) NULL,
	[BankName] [nvarchar](50) NULL,
	[BankAccount] [nvarchar](50) NULL,
	[RegionId] [bigint] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[IsExternal] [bit] NOT NULL,
 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseTransactionDetail]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseTransactionDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Qty] [int] NULL,
	[OfficialUnitPrice] [decimal](18, 2) NULL,
	[PurchasePrice] [decimal](18, 2) NULL,
	[ChangeRate] [decimal](18, 2) NULL,
	[PurchaseTransactionMasterID] [bigint] NULL,
	[ProductId] [bigint] NULL,
	[CurrencyId] [bigint] NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime2](7) NULL,
 CONSTRAINT [PK_PurchaseTransactionDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[PurchaseReportView]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[PurchaseReportView]
AS
SELECT 
 pd.Qty AS ProductQty
, pd.ChangeRate AS ProductChangeRate
, pd.CreatedOn
, pd.CreatedBy
, p.Code AS ProductCode
, p.NameAr AS productNameAr
, p.NameEn AS productNameEn
 ,pd.PurchasePrice AS ProductPurchasePrice 
,s.Id AS SupplierId
,s.NameAr AS SupplierNameAr
,s.NameEn AS SupplierNameEn
,dbo.Warehouse.Id AS WarehouseId
,dbo.Warehouse.NameAr AS WarehouseNameAr
,dbo.Warehouse.NameEn AS WarehouseNameEn
,dbo.Warehouse.Address AS WarehouseAddress 
FROM     dbo.PurchaseTransactionDetail AS pd INNER JOIN
                  dbo.Product AS p ON pd.ProductId = p.Id INNER JOIN
                  dbo.Supplier AS s ON s.Id = p.SupplierId INNER JOIN
                  dbo.Warehouse ON pd.Id = dbo.Warehouse.Id
GO
/****** Object:  Table [dbo].[SalesTransactionDetail]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesTransactionDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Qty] [int] NULL,
	[Discount] [decimal](18, 2) NULL,
	[VATValue] [decimal](18, 2) NULL,
	[SalesPrice] [decimal](18, 2) NULL,
	[TransactionId] [bigint] NULL,
	[MedicalLensIMasterd] [bigint] NULL,
	[ProductId] [bigint] NULL,
 CONSTRAINT [PK_SalesTransactionDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[SalesReportView]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[SalesReportView]
AS
SELECT sd.Qty, sd.Discount, p.Code, p.NameAr AS productNameAr, p.NameEn AS productNameEn, s.NameAr AS SupplierNameAr, s.NameEn AS SupplierNameEn
FROM     dbo.SalesTransactionDetail AS sd INNER JOIN
                  dbo.Product AS p ON sd.ProductId = p.Id INNER JOIN
                  dbo.Supplier AS s ON s.Id = p.SupplierId
GO
/****** Object:  Table [common].[SystemSettings]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [common].[SystemSettings](
	[Id] [int] NOT NULL,
	[ApplicationId] [nvarchar](100) NULL,
	[Secure] [bit] NOT NULL,
	[Key] [nvarchar](100) NOT NULL,
	[ValueType] [varchar](30) NOT NULL,
	[Value] [nvarchar](255) NOT NULL,
	[GroupName] [nvarchar](50) NOT NULL,
	[IsSticky] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](255) NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[UpdatedBy] [nvarchar](255) NULL,
	[UpdatedOn] [datetime2](7) NULL,
 CONSTRAINT [PK_common.SystemSettings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Attachment]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attachment](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](255) NOT NULL,
	[Extention] [nvarchar](500) NOT NULL,
	[File] [varbinary](max) NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[UpdatedBy] [nvarchar](256) NULL,
	[UpdatedOn] [datetime2](7) NULL,
 CONSTRAINT [PK_Attachment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Branch]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Branch](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[NameAr] [nvarchar](200) NULL,
	[NameEn] [nvarchar](200) NULL,
	[RegionId] [bigint] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_Branch] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[City]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[NameAr] [nvarchar](200) NULL,
	[NameEn] [nvarchar](200) NULL,
	[CountyId] [bigint] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Color]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Color](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ColorName] [nvarchar](250) NULL,
	[ModelId] [bigint] NULL,
 CONSTRAINT [PK_Color] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[NameAr] [nvarchar](200) NULL,
	[NameEn] [nvarchar](200) NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Currency]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Currency](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[NameAr] [nvarchar](200) NULL,
	[NameEn] [nvarchar](200) NULL,
	[ExchangeRate] [decimal](18, 2) NULL,
	[IsDefault] [bit] NOT NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NULL,
	[NameAr] [nvarchar](200) NULL,
	[NameEn] [nvarchar](200) NULL,
	[Phone] [nvarchar](20) NULL,
	[Mobile] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[Gender] [tinyint] NULL,
	[IdentityNo] [nvarchar](50) NULL,
	[DateOfBirth] [datetime] NULL,
	[CreationFileDate] [datetime] NULL,
	[InsuranceCardNo] [nvarchar](50) NULL,
	[Address] [nvarchar](400) NULL,
	[Notes] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[TitleId] [bigint] NULL,
	[NationalityId] [bigint] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerDisease]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerDisease](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerId] [bigint] NULL,
	[DiseaseId] [bigint] NULL,
 CONSTRAINT [PK_CustomerDisease] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerTitle]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerTitle](
	[Id] [bigint] NOT NULL,
	[NameAr] [nvarchar](200) NULL,
	[NameEn] [nvarchar](200) NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_CutomerTitle] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Disease]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Disease](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[NameAr] [nvarchar](200) NULL,
	[NameEn] [nvarchar](200) NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_Disease] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Doctor]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctor](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[NameAr] [nvarchar](250) NOT NULL,
	[NameEn] [nvarchar](250) NOT NULL,
	[Mobile] [nvarchar](15) NOT NULL,
	[Phone] [nvarchar](15) NULL,
	[Address] [nvarchar](250) NOT NULL,
	[CityId] [bigint] NULL,
	[RegionId] [bigint] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_Doctor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Emails]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Emails](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[EmailBody] [nvarchar](max) NOT NULL,
	[EmailAddress] [nvarchar](250) NULL,
	[IsEmailSent] [bit] NOT NULL,
	[ReceivingEmails] [nvarchar](500) NOT NULL,
	[AttachmentId] [bigint] NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](250) NULL,
 CONSTRAINT [PK_Emails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Examination]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Examination](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[RightSPH] [nvarchar](10) NULL,
	[RightCYL] [nvarchar](10) NULL,
	[RightAXIS] [nvarchar](10) NULL,
	[RightCL] [nvarchar](10) NULL,
	[LeftSPH] [nvarchar](10) NULL,
	[LeftCYL] [nvarchar](10) NULL,
	[LeftAXIS] [nvarchar](10) NULL,
	[LeftCL] [nvarchar](10) NULL,
	[IPD] [nvarchar](10) NULL,
	[ADDValue] [nvarchar](10) NULL,
	[CustomerId] [bigint] NULL,
	[DoctorId] [bigint] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_Examination] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[NameAr] [nvarchar](200) NULL,
	[NameEn] [nvarchar](200) NULL,
	[Description] [nvarchar](3000) NULL,
	[Status] [int] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupPageAction]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupPageAction](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[GroupId] [bigint] NOT NULL,
	[PageActionId] [bigint] NOT NULL,
 CONSTRAINT [PK_GroupPageAction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Job]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Job](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[NameAr] [nvarchar](200) NULL,
	[NameEn] [nvarchar](200) NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_Job] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Material]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Material](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](10) NULL,
	[CreatedOn] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](250) NULL,
 CONSTRAINT [PK_Material] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicalLensDetails]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicalLensDetails](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[MedicalLensMasterId] [bigint] NULL,
	[MedicalLensDetailsTypeId] [int] NULL,
	[SphId] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[CYL_0] [int] NULL,
	[CYL_0_25] [int] NULL,
	[CYL_0_5] [int] NULL,
	[CYL_0_75] [int] NULL,
	[CYL_1] [int] NULL,
	[CYL_1_25] [int] NULL,
	[CYL_1_5] [int] NULL,
	[CYL_1_75] [int] NULL,
	[CYL_2] [int] NULL,
	[CYL_2_25] [int] NULL,
	[CYL_2_5] [int] NULL,
	[CYL_2_75] [int] NULL,
	[CYL_3] [int] NULL,
	[CYL_3_25] [int] NULL,
	[CYL_3_5] [int] NULL,
	[CYL_3_75] [int] NULL,
	[CYL_4] [int] NULL,
	[CYL_4_25] [int] NULL,
	[CYL_4_5] [int] NULL,
	[CYL_4_75] [int] NULL,
	[CYL_5] [int] NULL,
	[CYL_5_25] [int] NULL,
	[CYL_5_5] [int] NULL,
	[CYL_5_75] [int] NULL,
	[CYL_6] [int] NULL,
 CONSTRAINT [PK__MedicalL__3214EC07AD8D3C3A] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicalLensDtailsType]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicalLensDtailsType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NameAr] [nvarchar](250) NULL,
	[NameEn] [nvarchar](250) NULL,
 CONSTRAINT [PK_MedicalLensDtailsType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicalLensMaster]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicalLensMaster](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[MedicalLensTypeId] [bigint] NULL,
	[Name] [nvarchar](250) NULL,
	[Code] [nvarchar](250) NULL,
	[AllowedDiscount] [float] NULL,
	[LensDiameter] [float] NULL,
	[LensThisckness] [float] NULL,
	[IsPositive] [bit] NULL,
	[CompanyName] [nvarchar](250) NULL,
	[Notes] [nvarchar](1000) NULL,
	[SupplierId] [bigint] NULL,
	[ColorId] [bigint] NULL,
	[CoatingDiagramId] [bigint] NULL,
	[LenseIndexId] [bigint] NULL,
	[MultifocalDesignId] [bigint] NULL,
	[VersionTypeId] [bigint] NULL,
	[BrandId] [bigint] NULL,
	[MaterialId] [bigint] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [nvarchar](250) NULL,
 CONSTRAINT [PK_MedicalLensMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicalLensPurchaseDetail]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicalLensPurchaseDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Qty] [nchar](10) NULL,
	[OfficialUnitPrice] [decimal](18, 2) NULL,
	[PurchasePrice] [decimal](18, 2) NULL,
	[ChangeRate] [decimal](18, 2) NULL,
	[PurchaseTransactionMasterID] [bigint] NULL,
	[MedicalLensMasterId] [bigint] NULL,
	[CurrencyId] [bigint] NULL,
	[SphId] [int] NULL,
	[CYL_0] [int] NULL,
	[CYL_0_25] [int] NULL,
	[CYL_0_5] [int] NULL,
	[CYL_0_75] [int] NULL,
	[CYL_1] [int] NULL,
	[CYL_1_25] [int] NULL,
	[CYL_1_5] [int] NULL,
	[CYL_1_75] [int] NULL,
	[CYL_2] [int] NULL,
	[CYL_2_25] [int] NULL,
	[CYL_2_5] [int] NULL,
	[CYL_2_75] [int] NULL,
	[CYL_3] [int] NULL,
	[CYL_3_25] [int] NULL,
	[CYL_3_5] [int] NULL,
	[CYL_3_75] [int] NULL,
	[CYL_4] [int] NULL,
	[CYL_4_25] [int] NULL,
	[CYL_4_5] [int] NULL,
	[CYL_4_75] [int] NULL,
	[CYL_5] [int] NULL,
	[CYL_5_25] [int] NULL,
	[CYL_5_5] [int] NULL,
	[CYL_5_75] [int] NULL,
	[CYL_6] [int] NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime2](7) NULL,
 CONSTRAINT [PK_MedicalLensPurchaseDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Model]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Model](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ModelName] [nvarchar](250) NULL,
	[BrandId] [bigint] NULL,
 CONSTRAINT [PK_Model] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Nationality]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nationality](
	[Id] [bigint] NOT NULL,
	[NameAr] [nvarchar](200) NULL,
	[NameEn] [nvarchar](200) NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_Nationality] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](250) NULL,
	[Description] [nvarchar](450) NULL,
	[ExpirDate] [datetime] NULL,
	[GroupId] [bigint] NULL,
	[Type] [varchar](1) NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NotificationResponse]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationResponse](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[ResponseById] [bigint] NULL,
	[NotificationId] [bigint] NULL,
	[Date] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_NotificationResponse] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NotificationSeen]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationSeen](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[SeenById] [bigint] NULL,
	[NotificationId] [bigint] NULL,
	[SeenDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_NotificationSeen] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PageAction]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageAction](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[NameAr] [nvarchar](200) NULL,
	[NameEn] [nvarchar](200) NULL,
	[PageId] [bigint] NULL,
 CONSTRAINT [PK_PageAction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductCategory]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategory](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[NameAr] [nvarchar](200) NULL,
	[NameEn] [nvarchar](200) NULL,
	[ParentId] [bigint] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseTransactionMaster]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseTransactionMaster](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[InvoiceDate] [datetime] NULL,
	[SupplierInvoiceDate] [datetime2](7) NULL,
	[InvoiceNo] [nvarchar](20) NULL,
	[SupplierInvoiceNo] [nvarchar](50) NULL,
	[PurchaseAgentName] [nvarchar](250) NULL,
	[TaxFees] [decimal](18, 4) NULL,
	[OtherFees] [decimal](18, 4) NULL,
	[InvoiceAmount] [decimal](18, 4) NULL,
	[FeesAmount] [decimal](18, 4) NULL,
	[Status] [bit] NULL,
	[Notes] [nvarchar](500) NULL,
	[WarehouseId] [bigint] NULL,
	[SupplierId] [bigint] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_PurchaseTransactionMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseTransactionPayment]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseTransactionPayment](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TransactionDate] [datetime] NULL,
	[PaidAmount] [decimal](18, 2) NULL,
	[CardNo] [nvarchar](50) NULL,
	[BankName] [nvarchar](50) NULL,
	[CardExpiryDate] [datetime] NULL,
	[ReciteNo] [nvarchar](50) NULL,
	[PaymentType] [tinyint] NULL,
	[TransactionId] [bigint] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_PurchaseTransactionPayment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Region]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Region](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[NameAr] [nvarchar](200) NULL,
	[NameEn] [nvarchar](200) NULL,
	[CityId] [bigint] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_Region] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesTransaction]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesTransaction](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[DocNo] [nvarchar](20) NULL,
	[SalesAgentName] [nvarchar](250) NULL,
	[WarehouserName] [nvarchar](250) NULL,
	[Discount] [decimal](18, 2) NULL,
	[InvoiceAmount] [decimal](18, 2) NULL,
	[DiscountAmount] [decimal](18, 2) NULL,
	[ReturnAmount] [decimal](18, 2) NULL,
	[VATValue] [decimal](18, 2) NULL,
	[CostAmount] [decimal](18, 2) NULL,
	[PaidAmount] [decimal](18, 2) NULL,
	[DeliveryDate] [datetime] NULL,
	[WarehouseId] [bigint] NULL,
	[CustomerId] [bigint] NULL,
	[BranchId] [bigint] NULL,
	[Type] [tinyint] NULL,
	[IsDelivered] [bit] NULL,
	[Notes] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_SalesTransactionMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesTransactionPayment]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesTransactionPayment](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[PaidAmount] [decimal](18, 2) NULL,
	[CardNo] [nvarchar](50) NULL,
	[BankName] [nvarchar](50) NULL,
	[CardExpiryDate] [datetime] NULL,
	[ReciteNo] [nvarchar](50) NULL,
	[PaymentType] [tinyint] NULL,
	[TransactionId] [bigint] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_SalesTransactionPayment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SMSs]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SMSs](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[IsMessageSent] [bit] NOT NULL,
	[ReceivingNumbers] [nvarchar](500) NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](250) NULL,
 CONSTRAINT [PK_SMS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SupplierContactPerson]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupplierContactPerson](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[NameAr] [nvarchar](200) NULL,
	[NameEn] [nvarchar](200) NULL,
	[Mobile1] [nvarchar](20) NULL,
	[Mobile2] [nvarchar](20) NULL,
	[Email] [nvarchar](200) NULL,
	[SupplierId] [bigint] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_SupplierContactPerson] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemConfiguration]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemConfiguration](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyNameAr] [nvarchar](250) NULL,
	[CompanyNameEn] [nvarchar](250) NULL,
	[DisableSMSNotifications] [bit] NULL,
	[DisableEmailNotifications] [bit] NULL,
	[IsSmtpAuthenticated] [bit] NULL,
	[SmtpPort] [nvarchar](5) NULL,
	[SmtpEnableSSL] [bit] NULL,
	[EmailFromName] [nvarchar](50) NULL,
	[SmtpServer] [nvarchar](50) NULL,
	[SmtpUserName] [nvarchar](50) NULL,
	[SmtpPassword] [nvarchar](50) NULL,
	[EmailFromAddress] [nvarchar](50) NULL,
	[ApplicationUrl] [nvarchar](250) NULL,
	[SMSSenderName] [nvarchar](100) NULL,
	[LogoId] [bigint] NULL,
 CONSTRAINT [PK__SystemCo__3214EC277CAB6DD7] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemErrorLog]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemErrorLog](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Source] [int] NULL,
	[Controller] [nvarchar](250) NULL,
	[Action] [nvarchar](250) NULL,
	[URL] [nvarchar](500) NULL,
	[ExceptionDetails] [nvarchar](max) NULL,
	[CreationDate] [datetime] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemPage]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemPage](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[NameAr] [nvarchar](200) NULL,
	[NameEn] [nvarchar](200) NULL,
	[URL] [nvarchar](200) NULL,
	[IconText] [nvarchar](20) NULL,
	[Sequence] [int] NULL,
	[Enabled] [bit] NULL,
	[ParentId] [bigint] NULL,
 CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Upload]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Upload](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](10) NULL,
	[NameAr] [nvarchar](200) NULL,
	[NameEn] [nvarchar](200) NULL,
	[IdentityNo] [nvarchar](20) NULL,
	[Email] [nvarchar](250) NULL,
	[WorkEmail] [nvarchar](250) NULL,
	[HomePhone] [nvarchar](25) NULL,
	[CurrentJob] [nvarchar](250) NULL,
	[Qualification] [nvarchar](150) NULL,
	[GraduationYear] [nvarchar](8) NULL,
	[Skills] [nvarchar](350) NULL,
	[UserName] [nvarchar](200) NULL,
	[Password] [nvarchar](100) NULL,
	[ActivationCode] [nvarchar](100) NULL,
	[Address] [nvarchar](500) NULL,
	[JoiningDate] [datetime] NULL,
	[MorningFrom] [time](7) NULL,
	[MorningTo] [time](7) NULL,
	[EveningFrom] [time](7) NULL,
	[EveningTo] [time](7) NULL,
	[Status] [int] NULL,
	[ImageId] [bigint] NULL,
	[NationalityId] [bigint] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[JobId] [bigint] NULL,
	[Mobile] [nvarchar](50) NULL,
	[DateOfBirth] [datetime] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserGroup]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGroup](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NULL,
	[GroupId] [bigint] NULL,
 CONSTRAINT [PK_UserGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPageAction]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPageAction](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[PageActionId] [bigint] NOT NULL,
	[Mode] [int] NOT NULL,
 CONSTRAINT [PK_UserPageAction_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserWarehouse]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserWarehouse](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NULL,
	[WarehouseId] [bigint] NULL,
 CONSTRAINT [PK_UserBranch] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WarehouseVerification]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WarehouseVerification](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DocumentNo] [nvarchar](20) NULL,
	[Date] [datetime] NULL,
	[WarehouserName] [nvarchar](250) NULL,
	[IsSettled] [bit] NOT NULL,
	[Notes] [nvarchar](max) NULL,
	[WarehouseId] [bigint] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_InventoryVerification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WarehouseVerificationDetail]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WarehouseVerificationDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TransQty] [int] NULL,
	[ActualQty] [int] NULL,
	[UnitOfficialPrice] [decimal](18, 4) NULL,
	[WarehouseVerificationId] [bigint] NULL,
	[MedicalLensIMasterd] [bigint] NULL,
	[ProductId] [bigint] NULL,
 CONSTRAINT [PK_InventoryVerificationDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [lookup].[Brand]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lookup].[Brand](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[BrandName] [nvarchar](250) NULL,
 CONSTRAINT [PK_Brand] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [lookup].[CoatingDiagram]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lookup].[CoatingDiagram](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[CreatedOn] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](250) NULL,
 CONSTRAINT [PK_CoatingDiagram] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [lookup].[CYLs]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lookup].[CYLs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](10) NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [nvarchar](250) NULL,
 CONSTRAINT [PK_CYLs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [lookup].[LenseIndex]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lookup].[LenseIndex](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Index] [float] NULL,
	[CreatedOn] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](250) NULL,
 CONSTRAINT [PK_LenseIndex] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [lookup].[MedicalLensType]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lookup].[MedicalLensType](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](250) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [nvarchar](250) NULL,
 CONSTRAINT [PK_MedicalLensType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [lookup].[MultifocalDesign]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lookup].[MultifocalDesign](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NULL,
	[CreatedOn] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](250) NULL,
 CONSTRAINT [PK_MultifocalDesign] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [lookup].[SPHs]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lookup].[SPHs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](10) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [nvarchar](250) NULL,
 CONSTRAINT [PK_SPHs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [lookup].[VersionType]    Script Date: 10/23/2021 8:32:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lookup].[VersionType](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NULL,
	[CreatedOn] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](250) NULL,
 CONSTRAINT [PK_VersionType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Branch] ON 
GO
INSERT [dbo].[Branch] ([Id], [NameAr], [NameEn], [RegionId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (1, N'فرع مدينة نصر', N'Nasr City', 1, NULL, NULL, CAST(N'2020-10-20T22:20:33.073' AS DateTime), CAST(N'2020-10-30T21:08:28.857' AS DateTime))
GO
INSERT [dbo].[Branch] ([Id], [NameAr], [NameEn], [RegionId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (2, N'فرع الاسكندرية', N'Alex', 2, NULL, NULL, CAST(N'2021-08-14T02:08:37.097' AS DateTime), NULL)
GO
INSERT [dbo].[Branch] ([Id], [NameAr], [NameEn], [RegionId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (3, N'الحى السابع', N'Seventh Avenue', 1, NULL, NULL, CAST(N'2021-08-30T21:30:42.227' AS DateTime), NULL)
GO
INSERT [dbo].[Branch] ([Id], [NameAr], [NameEn], [RegionId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (4, N'مركز ماوية للبصريات (الرس)', N'Maweya Optical Center', 3, NULL, NULL, CAST(N'2021-10-07T00:47:40.710' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[Branch] OFF
GO
SET IDENTITY_INSERT [dbo].[City] ON 
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (1, N'القاهرة', N'Cairo', 1, NULL, NULL, NULL, CAST(N'2020-10-18T02:23:59.223' AS DateTime))
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (2, N'الاسكندرية', N'Alexandria', 1, NULL, NULL, NULL, CAST(N'2020-10-19T17:12:14.587' AS DateTime))
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (3, N'ابو ظبي', N'Abu Dhabi', 5, NULL, NULL, CAST(N'2020-10-20T00:02:22.733' AS DateTime), CAST(N'2020-10-20T00:25:36.080' AS DateTime))
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (4, N'test', N'test', 1, NULL, NULL, CAST(N'2021-08-10T22:27:50.313' AS DateTime), NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (5, N'تبوك', N'Tabuk', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (6, N'الرياض', N'Riyadh', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (7, N'الطائف', N'At Taif', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (8, N'مكة المكرمة', N'Makkah Al Mukarramah', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (9, N'حائل', N'Hail', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (10, N'بريدة', N'Buraydah', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (11, N'الهفوف', N'Al Hufuf', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (12, N'الدمام', N'Ad Dammam', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (13, N'المدينة المنورة', N'Al Madinah Al Munawwarah', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (14, N'ابها', N'Abha', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (15, N'جازان', N'Jazan', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (16, N'جدة', N'Jeddah', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (17, N'المجمعة', N'Al Majmaah', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (18, N'الخبر', N'Al Khubar', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (19, N'حفر الباطن', N'Hafar Al Batin', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (20, N'خميس مشيط', N'Khamis Mushayt', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (21, N'احد رفيده', N'Ahad Rifaydah', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (22, N'القطيف', N'Al Qatif', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (23, N'عنيزة', N'Unayzah', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (24, N'قرية العليا', N'Qaryat Al Ulya', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (25, N'الجبيل', N'Al Jubail', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (26, N'النعيرية', N'An Nuayriyah', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (27, N'الظهران', N'Dhahran', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (28, N'الوجه', N'Al Wajh', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (29, N'بقيق', N'Buqayq', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (30, N'الزلفي', N'Az Zulfi', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (31, N'خيبر', N'Khaybar', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (32, N'الغاط', N'Al Ghat', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (33, N'املج', N'Umluj', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (34, N'رابغ', N'Rabigh', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (35, N'عفيف', N'Afif', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (36, N'ثادق', N'Thadiq', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (37, N'سيهات', N'Sayhat', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (38, N'تاروت', N'Tarut', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (39, N'ينبع', N'Yanbu', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (40, N'شقراء', N'Shaqra', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (41, N'الدوادمي', N'Ad Duwadimi', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (42, N'الدرعية', N'Ad Diriyah', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (43, N'القويعية', N'Quwayiyah', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (44, N'المزاحمية', N'Al Muzahimiyah', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (45, N'بدر', N'Badr', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (46, N'الخرج', N'Al Kharj', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (47, N'الدلم', N'Ad Dilam', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (48, N'الشنان', N'Ash Shinan', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (49, N'الخرمة', N'Al Khurmah', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (50, N'الجموم', N'Al Jumum', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (51, N'المجاردة', N'Al Majardah', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (52, N'السليل', N'As Sulayyil', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (53, N'تثليث', N'Tathilith', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (54, N'بيشة', N'Bishah', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (55, N'الباحة', N'Al Baha', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (56, N'القنفذة', N'Al Qunfidhah', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (57, N'محايل', N'Muhayil', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (58, N'ثول', N'Thuwal', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (59, N'ضبا', N'Duba', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (60, N'تربه', N'Turbah', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (61, N'صفوى', N'Safwa', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (62, N'عنك', N'Inak', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (63, N'طريف', N'Turaif', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (64, N'عرعر', N'Arar', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (65, N'القريات', N'Al Qurayyat', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (66, N'سكاكا', N'Sakaka', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (67, N'رفحاء', N'Rafha', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (68, N'دومة الجندل', N'Dawmat Al Jandal', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (69, N'الرس', N'Ar Rass', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (70, N'المذنب', N'Al Midhnab', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (71, N'الخفجي', N'Al Khafji', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (72, N'رياض الخبراء', N'Riyad Al Khabra', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (73, N'البدائع', N'Al Badai', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (74, N'رأس تنورة', N'Ras Tannurah', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (75, N'البكيرية', N'Al Bukayriyah', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (76, N'الشماسية', N'Ash Shimasiyah', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (77, N'الحريق', N'Al Hariq', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (78, N'حوطة بني تميم', N'Hawtat Bani Tamim', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (79, N'ليلى', N'Layla', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (80, N'بللسمر', N'Billasmar', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (81, N'شرورة', N'Sharurah', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (82, N'نجران', N'Najran', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (83, N'صبيا', N'Sabya', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (84, N'ابو عريش', N'Abu Arish', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (85, N'صامطة', N'Samtah', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (86, N'احد المسارحة', N'Ahad Al Musarihah', 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[City] ([Id], [NameAr], [NameEn], [CountyId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (87, N'مدينة الملك عبدالله الاقتصادية', N'King Abdullah Economic City', 2, NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[City] OFF
GO
SET IDENTITY_INSERT [dbo].[Color] ON 
GO
INSERT [dbo].[Color] ([Id], [ColorName], [ModelId]) VALUES (2, N'red', 4)
GO
SET IDENTITY_INSERT [dbo].[Color] OFF
GO
SET IDENTITY_INSERT [dbo].[Country] ON 
GO
INSERT [dbo].[Country] ([Id], [NameAr], [NameEn], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (1, N'مصر', N'Egypt', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Country] ([Id], [NameAr], [NameEn], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (2, N'المملكة العربية السعودية', N'KSA', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Country] ([Id], [NameAr], [NameEn], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (5, N'الامارات', N'UAE', NULL, NULL, CAST(N'2020-10-20T00:01:24.603' AS DateTime), NULL)
GO
INSERT [dbo].[Country] ([Id], [NameAr], [NameEn], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (6, N'مملكة البحرين', N'Bahrin', NULL, NULL, CAST(N'2021-10-07T00:48:16.367' AS DateTime), NULL)
GO
INSERT [dbo].[Country] ([Id], [NameAr], [NameEn], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (7, N'مملكة البحرين', N'Bahrain', NULL, NULL, CAST(N'2021-10-07T00:48:35.730' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[Country] OFF
GO
SET IDENTITY_INSERT [dbo].[Currency] ON 
GO
INSERT [dbo].[Currency] ([Id], [NameAr], [NameEn], [ExchangeRate], [IsDefault], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (1, N'ريال سعودي', N'SAR', CAST(4.32 AS Decimal(18, 2)), 0, NULL, NULL, CAST(N'2020-10-22T10:39:00.083' AS DateTime), NULL)
GO
INSERT [dbo].[Currency] ([Id], [NameAr], [NameEn], [ExchangeRate], [IsDefault], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (2, N'جنية مصري', N'EP', CAST(1.00 AS Decimal(18, 2)), 1, NULL, NULL, CAST(N'2020-10-22T10:39:32.570' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[Currency] OFF
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 
GO
INSERT [dbo].[Customer] ([Id], [Code], [NameAr], [NameEn], [Phone], [Mobile], [Email], [Gender], [IdentityNo], [DateOfBirth], [CreationFileDate], [InsuranceCardNo], [Address], [Notes], [IsActive], [TitleId], [NationalityId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (1, N'00001', N'اسامه كمال', N'Osama', N'257885555', N'01258748525', N'a@a.com', 1, N'123', CAST(N'1990-01-01T00:00:00.000' AS DateTime), CAST(N'2010-01-01T00:00:00.000' AS DateTime), N'123', N'giza', N'notes', 1, 2, 1, N'Osama', NULL, CAST(N'2020-01-01T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Customer] ([Id], [Code], [NameAr], [NameEn], [Phone], [Mobile], [Email], [Gender], [IdentityNo], [DateOfBirth], [CreationFileDate], [InsuranceCardNo], [Address], [Notes], [IsActive], [TitleId], [NationalityId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (2, N'00001', N'اسامه كمال', N'اسامه كمال', N'257885555', N'01258748525', N'a@a.com', NULL, NULL, NULL, NULL, NULL, N'giza', NULL, 0, NULL, NULL, NULL, NULL, CAST(N'2021-10-13T00:23:46.960' AS DateTime), NULL)
GO
INSERT [dbo].[Customer] ([Id], [Code], [NameAr], [NameEn], [Phone], [Mobile], [Email], [Gender], [IdentityNo], [DateOfBirth], [CreationFileDate], [InsuranceCardNo], [Address], [Notes], [IsActive], [TitleId], [NationalityId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (3, N'00001', N'اسامه كمال', N'اسامه كمال', N'257885555', N'01258748525', N'a@a.com', NULL, NULL, NULL, NULL, NULL, N'giza', N'test', 0, NULL, NULL, NULL, NULL, NULL, CAST(N'2021-10-18T22:52:41.210' AS DateTime))
GO
INSERT [dbo].[Customer] ([Id], [Code], [NameAr], [NameEn], [Phone], [Mobile], [Email], [Gender], [IdentityNo], [DateOfBirth], [CreationFileDate], [InsuranceCardNo], [Address], [Notes], [IsActive], [TitleId], [NationalityId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (4, N'000002', N'test', N'test', N'010022569885', N'01122544785', N'test@test.test', NULL, NULL, NULL, NULL, NULL, N'test', N'test', 0, NULL, NULL, NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[CustomerDisease] ON 
GO
INSERT [dbo].[CustomerDisease] ([Id], [CustomerId], [DiseaseId]) VALUES (1, 1, 1)
GO
INSERT [dbo].[CustomerDisease] ([Id], [CustomerId], [DiseaseId]) VALUES (2, 1, 5)
GO
SET IDENTITY_INSERT [dbo].[CustomerDisease] OFF
GO
INSERT [dbo].[CustomerTitle] ([Id], [NameAr], [NameEn], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (1, N'السيد', N'Mr', N'Osama', NULL, CAST(N'2020-01-01T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[CustomerTitle] ([Id], [NameAr], [NameEn], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (2, N'الدكتور', N'Prof', N'Osama', NULL, CAST(N'2020-01-01T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[CustomerTitle] ([Id], [NameAr], [NameEn], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (3, N'الطفل', N'Kid', N'Osama', NULL, CAST(N'2020-01-01T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[CustomerTitle] ([Id], [NameAr], [NameEn], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (4, N'السيدة', N'Miss', N'Osama', NULL, CAST(N'2020-01-01T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[CustomerTitle] ([Id], [NameAr], [NameEn], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (5, N'الدكتوره', N'Prof', N'Osama', NULL, CAST(N'2020-01-01T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[CustomerTitle] ([Id], [NameAr], [NameEn], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (6, N'الطفله', N'Kid', N'Osama', NULL, CAST(N'2020-01-01T00:00:00.000' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[Disease] ON 
GO
INSERT [dbo].[Disease] ([Id], [NameAr], [NameEn], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (1, N'ليزر تصحيحي', N'ليزر تصحيحي', NULL, NULL, CAST(N'2020-10-24T18:24:41.693' AS DateTime), NULL)
GO
INSERT [dbo].[Disease] ([Id], [NameAr], [NameEn], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (3, N'مرض 2', N'مرض 2', NULL, NULL, CAST(N'2020-10-24T18:24:41.693' AS DateTime), NULL)
GO
INSERT [dbo].[Disease] ([Id], [NameAr], [NameEn], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (4, N'مرض 3', N'مرض 3', NULL, NULL, CAST(N'2020-10-24T18:24:41.693' AS DateTime), NULL)
GO
INSERT [dbo].[Disease] ([Id], [NameAr], [NameEn], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (5, N'مرض 4', N'مرض 4', NULL, NULL, CAST(N'2020-10-24T18:24:41.693' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[Disease] OFF
GO
SET IDENTITY_INSERT [dbo].[Doctor] ON 
GO
INSERT [dbo].[Doctor] ([Id], [NameAr], [NameEn], [Mobile], [Phone], [Address], [CityId], [RegionId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (1, N'علاء محمد', N'Alaa', N'01258748566', N'257885554', N'Giza', 1, 1, N'Osama', N'Osama', CAST(N'2020-01-01T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Doctor] ([Id], [NameAr], [NameEn], [Mobile], [Phone], [Address], [CityId], [RegionId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (2, N'فثشسف', N'teast', N'022000', N'01002255', N'test', NULL, NULL, N'Current User', NULL, CAST(N'2021-02-27T01:29:23.210' AS DateTime), NULL)
GO
INSERT [dbo].[Doctor] ([Id], [NameAr], [NameEn], [Mobile], [Phone], [Address], [CityId], [RegionId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (3, N'محمد', N'Mohamed', N'01220014', N'01201055', N'test', NULL, NULL, N'Current User', NULL, CAST(N'2021-10-15T20:54:00.033' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[Doctor] OFF
GO
SET IDENTITY_INSERT [dbo].[Examination] ON 
GO
INSERT [dbo].[Examination] ([Id], [Date], [RightSPH], [RightCYL], [RightAXIS], [RightCL], [LeftSPH], [LeftCYL], [LeftAXIS], [LeftCL], [IPD], [ADDValue], [CustomerId], [DoctorId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (1, CAST(N'2020-01-01T00:00:00.000' AS DateTime), N'-05.25', N'-01.15', N'-02.25', N'02.25', N'02.25', N'02.25', N'02.25', N'02.25', N'02.25', N'02.25', 1, 1, N'Osama', NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Examination] OFF
GO
SET IDENTITY_INSERT [dbo].[Group] ON 
GO
INSERT [dbo].[Group] ([Id], [NameAr], [NameEn], [Description], [Status], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (1, N'Super Admins', N'This is a Group For super admin', NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Group] ([Id], [NameAr], [NameEn], [Description], [Status], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (2, N'Inventory', N'Inventory', NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Group] ([Id], [NameAr], [NameEn], [Description], [Status], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (3, N'Sales Agents', N'Sales Agents', NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Group] ([Id], [NameAr], [NameEn], [Description], [Status], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (4, N'Sales', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Group] ([Id], [NameAr], [NameEn], [Description], [Status], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (5, N'Finance', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Group] ([Id], [NameAr], [NameEn], [Description], [Status], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (6, N'Banking', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Group] ([Id], [NameAr], [NameEn], [Description], [Status], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (7, N'Sales & Finance', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Group] ([Id], [NameAr], [NameEn], [Description], [Status], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (8, N'Owner-General Manager', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Group] ([Id], [NameAr], [NameEn], [Description], [Status], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (9, N'Invoice', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Group] OFF
GO
SET IDENTITY_INSERT [dbo].[GroupPageAction] ON 
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (4, 4, 1)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (5, 4, 3)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (28, 3, 7)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (29, 1, 1)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (30, 1, 2)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (31, 1, 3)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (32, 1, 4)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (33, 1, 5)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (34, 1, 6)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (35, 1, 7)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (36, 1, 8)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (37, 1, 9)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (38, 1, 10)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (39, 1, 11)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (40, 1, 12)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (41, 1, 13)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (42, 1, 14)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (43, 1, 15)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (44, 1, 16)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (45, 1, 17)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (46, 1, 18)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (47, 1, 19)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (48, 1, 20)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (49, 1, 21)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (50, 1, 22)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (51, 1, 23)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (52, 1, 24)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (53, 1, 25)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (54, 1, 26)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (55, 1, 27)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (56, 1, 28)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (57, 1, 29)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (58, 1, 30)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (59, 1, 31)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (60, 1, 32)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (61, 1, 33)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (62, 1, 34)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (63, 1, 35)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (64, 1, 36)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (65, 1, 37)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (66, 1, 38)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (67, 1, 39)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (68, 1, 40)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (69, 1, 41)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (70, 1, 42)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (71, 1, 43)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (72, 1, 44)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (73, 1, 45)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (74, 1, 46)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (75, 1, 47)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (76, 1, 49)
GO
INSERT [dbo].[GroupPageAction] ([Id], [GroupId], [PageActionId]) VALUES (78, 1, 50)
GO
SET IDENTITY_INSERT [dbo].[GroupPageAction] OFF
GO
SET IDENTITY_INSERT [dbo].[Material] ON 
GO
INSERT [dbo].[Material] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (1, N'Glass     ', CAST(N'2020-04-11T00:00:00.0000000' AS DateTime2), N'Mohamed', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Material] OFF
GO
SET IDENTITY_INSERT [dbo].[MedicalLensDetails] ON 
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (261, 10, 1, 1, NULL, NULL, NULL, NULL, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (262, 10, 1, 2, NULL, NULL, NULL, NULL, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (263, 10, 1, 3, NULL, NULL, NULL, NULL, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (264, 10, 1, 4, NULL, NULL, NULL, NULL, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (265, 10, 1, 5, NULL, NULL, NULL, NULL, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (266, 10, 1, 6, NULL, NULL, NULL, NULL, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (267, 10, 1, 7, NULL, NULL, NULL, NULL, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (268, 10, 1, 8, NULL, NULL, NULL, NULL, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (269, 10, 1, 9, NULL, NULL, NULL, NULL, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (270, 10, 1, 10, NULL, NULL, NULL, NULL, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (271, 10, 1, 11, NULL, NULL, NULL, NULL, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (272, 10, 1, 12, NULL, NULL, NULL, NULL, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (273, 10, 1, 13, NULL, NULL, NULL, NULL, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (274, 10, 1, 14, NULL, NULL, NULL, NULL, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (275, 10, 1, 15, NULL, NULL, NULL, NULL, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (276, 10, 1, 16, NULL, NULL, NULL, NULL, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (277, 10, 1, 17, NULL, NULL, NULL, NULL, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (278, 10, 1, 18, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (279, 10, 1, 19, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (280, 10, 1, 20, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (281, 10, 1, 21, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (282, 10, 1, 22, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (283, 10, 1, 23, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (284, 10, 1, 24, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (285, 10, 1, 25, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (286, 10, 1, 26, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (287, 10, 1, 27, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (288, 10, 1, 28, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (289, 10, 1, 29, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (290, 10, 1, 30, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (291, 10, 1, 31, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (292, 10, 1, 32, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (293, 10, 1, 33, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (294, 10, 1, 34, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (295, 10, 1, 35, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (296, 10, 1, 36, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (297, 10, 1, 37, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (298, 10, 1, 38, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (299, 10, 1, 39, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (300, 10, 1, 40, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (301, 10, 1, 41, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (302, 10, 1, 42, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (303, 10, 1, 43, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (304, 10, 3, 1, NULL, NULL, NULL, NULL, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (305, 10, 3, 2, NULL, NULL, NULL, NULL, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (306, 10, 3, 3, NULL, NULL, NULL, NULL, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (307, 10, 3, 4, NULL, NULL, NULL, NULL, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (308, 10, 3, 5, NULL, NULL, NULL, NULL, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (309, 10, 3, 6, NULL, NULL, NULL, NULL, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (310, 10, 3, 7, NULL, NULL, NULL, NULL, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (311, 10, 3, 8, NULL, NULL, NULL, NULL, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (312, 10, 3, 9, NULL, NULL, NULL, NULL, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (313, 10, 3, 10, NULL, NULL, NULL, NULL, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (314, 10, 3, 11, NULL, NULL, NULL, NULL, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (315, 10, 3, 12, NULL, NULL, NULL, NULL, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (316, 10, 3, 13, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (317, 10, 3, 14, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (318, 10, 3, 15, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (319, 10, 3, 16, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (320, 10, 3, 17, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (321, 10, 3, 18, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (322, 10, 3, 19, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (323, 10, 3, 20, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (324, 10, 3, 21, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (325, 10, 3, 22, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (326, 10, 3, 23, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (327, 10, 3, 24, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (328, 10, 3, 25, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (329, 10, 3, 26, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (330, 10, 3, 27, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (331, 10, 3, 28, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (332, 10, 3, 29, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (333, 10, 3, 30, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (334, 10, 3, 31, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (335, 10, 3, 32, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (336, 10, 3, 33, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (337, 10, 3, 34, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (338, 10, 3, 35, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (339, 10, 3, 36, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (340, 10, 3, 37, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (341, 10, 3, 38, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (342, 10, 3, 39, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (343, 10, 3, 40, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (344, 10, 3, 41, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (345, 10, 3, 42, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (346, 10, 3, 43, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (347, 10, 4, 1, NULL, NULL, NULL, NULL, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (348, 10, 4, 2, NULL, NULL, NULL, NULL, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (349, 10, 4, 3, NULL, NULL, NULL, NULL, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (350, 10, 4, 4, NULL, NULL, NULL, NULL, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (351, 10, 4, 5, NULL, NULL, NULL, NULL, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (352, 10, 4, 6, NULL, NULL, NULL, NULL, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (353, 10, 4, 7, NULL, NULL, NULL, NULL, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (354, 10, 4, 8, NULL, NULL, NULL, NULL, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (355, 10, 4, 9, NULL, NULL, NULL, NULL, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (356, 10, 4, 10, NULL, NULL, NULL, NULL, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (357, 10, 4, 11, NULL, NULL, NULL, NULL, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (358, 10, 4, 12, NULL, NULL, NULL, NULL, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (359, 10, 4, 13, NULL, NULL, NULL, NULL, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (360, 10, 4, 14, NULL, NULL, NULL, NULL, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (361, 10, 4, 15, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (362, 10, 4, 16, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (363, 10, 4, 17, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (364, 10, 4, 18, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (365, 10, 4, 19, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (366, 10, 4, 20, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (367, 10, 4, 21, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (368, 10, 4, 22, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (369, 10, 4, 23, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (370, 10, 4, 24, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (371, 10, 4, 25, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (372, 10, 4, 26, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (373, 10, 4, 27, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (374, 10, 4, 28, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (375, 10, 4, 29, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (376, 10, 4, 30, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (377, 10, 4, 31, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (378, 10, 4, 32, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (379, 10, 4, 33, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (380, 10, 4, 34, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (381, 10, 4, 35, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (382, 10, 4, 36, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (383, 10, 4, 37, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (384, 10, 4, 38, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (385, 10, 4, 39, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (386, 10, 4, 40, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (387, 10, 4, 41, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (388, 10, 4, 42, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[MedicalLensDetails] ([Id], [MedicalLensMasterId], [MedicalLensDetailsTypeId], [SphId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [CYL_0], [CYL_0_25], [CYL_0_5], [CYL_0_75], [CYL_1], [CYL_1_25], [CYL_1_5], [CYL_1_75], [CYL_2], [CYL_2_25], [CYL_2_5], [CYL_2_75], [CYL_3], [CYL_3_25], [CYL_3_5], [CYL_3_75], [CYL_4], [CYL_4_25], [CYL_4_5], [CYL_4_75], [CYL_5], [CYL_5_25], [CYL_5_5], [CYL_5_75], [CYL_6]) VALUES (389, 10, 4, 43, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
SET IDENTITY_INSERT [dbo].[MedicalLensDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[MedicalLensDtailsType] ON 
GO
INSERT [dbo].[MedicalLensDtailsType] ([Id], [NameAr], [NameEn]) VALUES (1, N'الكمية', N'Quantity')
GO
INSERT [dbo].[MedicalLensDtailsType] ([Id], [NameAr], [NameEn]) VALUES (2, N'اقل كمية', N'Min Quantity')
GO
INSERT [dbo].[MedicalLensDtailsType] ([Id], [NameAr], [NameEn]) VALUES (3, N'سعر الشراء', N'Purchase Price')
GO
INSERT [dbo].[MedicalLensDtailsType] ([Id], [NameAr], [NameEn]) VALUES (4, N'سعر البيع', N'Selling Price')
GO
SET IDENTITY_INSERT [dbo].[MedicalLensDtailsType] OFF
GO
SET IDENTITY_INSERT [dbo].[MedicalLensMaster] ON 
GO
INSERT [dbo].[MedicalLensMaster] ([Id], [MedicalLensTypeId], [Name], [Code], [AllowedDiscount], [LensDiameter], [LensThisckness], [IsPositive], [CompanyName], [Notes], [SupplierId], [ColorId], [CoatingDiagramId], [LenseIndexId], [MultifocalDesignId], [VersionTypeId], [BrandId], [MaterialId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (3, NULL, N'test', N'000001', 33, 22, 55, 1, N'test', N'<b>test</b>', 1, 2, 2, 4, 1, 1, 1, 1, CAST(N'2021-09-30T17:34:09.623' AS DateTime), NULL, CAST(N'2021-10-02T03:11:01.210' AS DateTime), NULL)
GO
INSERT [dbo].[MedicalLensMaster] ([Id], [MedicalLensTypeId], [Name], [Code], [AllowedDiscount], [LensDiameter], [LensThisckness], [IsPositive], [CompanyName], [Notes], [SupplierId], [ColorId], [CoatingDiagramId], [LenseIndexId], [MultifocalDesignId], [VersionTypeId], [BrandId], [MaterialId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (4, NULL, N'testttt', N'000002', 22, 22, 22, 1, N'2test', NULL, 1, 2, 2, 1, 1, 1, 1, 1, CAST(N'2021-09-30T22:01:59.070' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[MedicalLensMaster] ([Id], [MedicalLensTypeId], [Name], [Code], [AllowedDiscount], [LensDiameter], [LensThisckness], [IsPositive], [CompanyName], [Notes], [SupplierId], [ColorId], [CoatingDiagramId], [LenseIndexId], [MultifocalDesignId], [VersionTypeId], [BrandId], [MaterialId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (5, NULL, N'crizal', N'000003', 22, 22, 22, NULL, N'test', N'test Notes', 1, 2, 2, 1, 1, 1, 1, 1, CAST(N'2021-10-03T23:21:08.583' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[MedicalLensMaster] ([Id], [MedicalLensTypeId], [Name], [Code], [AllowedDiscount], [LensDiameter], [LensThisckness], [IsPositive], [CompanyName], [Notes], [SupplierId], [ColorId], [CoatingDiagramId], [LenseIndexId], [MultifocalDesignId], [VersionTypeId], [BrandId], [MaterialId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (6, NULL, N'Crizal2', N'000004', 5, 2.5, 33, NULL, N'test compant name ', N'Test notes', 1, 2, 2, 2, 1, 1, 1, 1, CAST(N'2021-10-04T23:47:08.277' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[MedicalLensMaster] ([Id], [MedicalLensTypeId], [Name], [Code], [AllowedDiscount], [LensDiameter], [LensThisckness], [IsPositive], [CompanyName], [Notes], [SupplierId], [ColorId], [CoatingDiagramId], [LenseIndexId], [MultifocalDesignId], [VersionTypeId], [BrandId], [MaterialId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (7, NULL, N'testtt', N'000005', 55.25, 3.5, 3.5, NULL, N'test compant', N'testttttttttttttttttttttttttttt', 1, 2, 3, 1, 1, 3, 10004, 1, CAST(N'2021-10-06T22:58:07.767' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[MedicalLensMaster] ([Id], [MedicalLensTypeId], [Name], [Code], [AllowedDiscount], [LensDiameter], [LensThisckness], [IsPositive], [CompanyName], [Notes], [SupplierId], [ColorId], [CoatingDiagramId], [LenseIndexId], [MultifocalDesignId], [VersionTypeId], [BrandId], [MaterialId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (8, NULL, N'Crizal Test', N'000006', 33, 2.3, 2.5, NULL, N'النور للبصريات', N'Test Notes', 1, 2, 2, 1, 1, 2, 10003, 1, CAST(N'2021-10-07T02:16:29.193' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[MedicalLensMaster] ([Id], [MedicalLensTypeId], [Name], [Code], [AllowedDiscount], [LensDiameter], [LensThisckness], [IsPositive], [CompanyName], [Notes], [SupplierId], [ColorId], [CoatingDiagramId], [LenseIndexId], [MultifocalDesignId], [VersionTypeId], [BrandId], [MaterialId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (9, NULL, N'AIRWEAR', N'000007', 5.5, 3.3, 2.5, NULL, N'النور للبصريات', N'Test Notes', 1, 2, 2, 4, 1, 2, 1, 1, CAST(N'2021-10-07T02:51:44.590' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[MedicalLensMaster] ([Id], [MedicalLensTypeId], [Name], [Code], [AllowedDiscount], [LensDiameter], [LensThisckness], [IsPositive], [CompanyName], [Notes], [SupplierId], [ColorId], [CoatingDiagramId], [LenseIndexId], [MultifocalDesignId], [VersionTypeId], [BrandId], [MaterialId], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (10, NULL, N'test111', N'000008', 2, 2, 2, NULL, N'عدسة', N'test', 1, 2, 2, 1, 1, 1, 1, 1, CAST(N'2021-10-15T21:24:23.743' AS DateTime), NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[MedicalLensMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[Model] ON 
GO
INSERT [dbo].[Model] ([Id], [ModelName], [BrandId]) VALUES (2, N'kl252', 1)
GO
INSERT [dbo].[Model] ([Id], [ModelName], [BrandId]) VALUES (4, N'rb112', 10003)
GO
SET IDENTITY_INSERT [dbo].[Model] OFF
GO
INSERT [dbo].[Nationality] ([Id], [NameAr], [NameEn], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (1, N'مصري', N'Egyption', N'Osama', N'1-1-2020', NULL, NULL)
GO
INSERT [dbo].[Nationality] ([Id], [NameAr], [NameEn], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (2, N'سعودي', N'KSA', N'Osama', N'1-1-2020', NULL, NULL)
GO
INSERT [dbo].[Nationality] ([Id], [NameAr], [NameEn], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (3, N'إماراتي', N'UAE', N'Osama', N'1-1-2020', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[PageAction] ON 
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (1, N'عرض', N'View', 1)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (2, N'عرض', N'View', 2)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (3, N'عرض', N'View', 3)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (4, N'عرض', N'View', 4)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (5, N'عرض', N'View', 5)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (6, N'عرض', N'View', 6)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (7, N'عرض', N'View', 7)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (8, N'عرض', N'View', 8)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (9, N'عرض', N'View', 9)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (10, N'عرض', N'View', 10)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (11, N'عرض', N'View', 11)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (12, N'عرض', N'View', 12)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (13, N'عرض', N'View', 13)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (14, N'عرض', N'View', 14)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (15, N'عرض', N'View', 15)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (16, N'عرض', N'View', 16)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (17, N'عرض', N'View', 17)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (18, N'عرض', N'View', 18)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (19, N'عرض', N'View', 19)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (20, N'عرض', N'View', 20)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (21, N'عرض', N'View', 21)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (22, N'عرض', N'View', 22)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (23, N'عرض', N'View', 23)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (24, N'عرض', N'View', 24)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (25, N'عرض', N'View', 25)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (26, N'عرض', N'View', 26)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (27, N'عرض', N'View', 27)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (28, N'عرض', N'View', 28)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (29, N'عرض', N'View', 29)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (30, N'عرض', N'View', 30)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (31, N'عرض', N'View', 31)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (32, N'عرض', N'View', 32)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (33, N'عرض', N'View', 33)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (34, N'عرض', N'View', 34)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (35, N'عرض', N'View', 35)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (36, N'عرض', N'View', 36)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (37, N'عرض', N'View', 37)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (38, N'عرض', N'View', 38)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (39, N'عرض', N'View', 39)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (40, N'عرض', N'View', 40)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (41, N'عرض', N'View', 41)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (42, N'عرض', N'View', 42)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (43, N'عرض', N'View', 43)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (44, N'عرض', N'View', 44)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (45, N'عرض', N'View', 45)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (46, N'عرض', N'View', 46)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (47, N'عرض', N'View', 47)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (48, N'عرض', N'View', 48)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (49, N'عرض', N'View', 49)
GO
INSERT [dbo].[PageAction] ([Id], [NameAr], [NameEn], [PageId]) VALUES (50, N'عرض', N'View', 50)
GO
SET IDENTITY_INSERT [dbo].[PageAction] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 
GO
INSERT [dbo].[Product] ([Id], [Code], [NameAr], [NameEn], [OfficialPrice], [MinQty], [Qty], [CategoryId], [SupplierId], [BrandId], [ModelId], [ColorId], [MaterialId], [Grade], [Power], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (1, N'001', N'نظارة طبية 1', N'Medical lencesa 1', CAST(350.00 AS Decimal(18, 2)), 4, 542, 2, 1, NULL, NULL, NULL, NULL, NULL, NULL, N'Osama', CAST(N'2020-01-01T00:00:00.000' AS DateTime), NULL, CAST(N'2021-08-14T03:04:34.110' AS DateTime))
GO
INSERT [dbo].[Product] ([Id], [Code], [NameAr], [NameEn], [OfficialPrice], [MinQty], [Qty], [CategoryId], [SupplierId], [BrandId], [ModelId], [ColorId], [MaterialId], [Grade], [Power], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (2, N'002', N'نظارة طبية 2', N'Medical lences 2', CAST(550.00 AS Decimal(18, 2)), 4, 358, 2, 1, NULL, NULL, NULL, NULL, NULL, NULL, N'Osama', CAST(N'2020-01-01T00:00:00.000' AS DateTime), NULL, CAST(N'2021-08-14T03:04:34.113' AS DateTime))
GO
INSERT [dbo].[Product] ([Id], [Code], [NameAr], [NameEn], [OfficialPrice], [MinQty], [Qty], [CategoryId], [SupplierId], [BrandId], [ModelId], [ColorId], [MaterialId], [Grade], [Power], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (3, N'000003', N'Crizal', N'Crizal', CAST(100.00 AS Decimal(18, 2)), 5, NULL, 1, 1, 1, NULL, 2, NULL, N'20', N'20', NULL, CAST(N'2021-04-05T23:27:32.190' AS DateTime), NULL, CAST(N'2021-04-05T23:29:09.347' AS DateTime))
GO
INSERT [dbo].[Product] ([Id], [Code], [NameAr], [NameEn], [OfficialPrice], [MinQty], [Qty], [CategoryId], [SupplierId], [BrandId], [ModelId], [ColorId], [MaterialId], [Grade], [Power], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (4, N'000004', N'0.25', N'-0.5', CAST(50.00 AS Decimal(18, 2)), 5, NULL, 1, 1, 10004, NULL, 2, NULL, N'5', N'5', NULL, CAST(N'2021-04-09T00:02:21.497' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Product] ([Id], [Code], [NameAr], [NameEn], [OfficialPrice], [MinQty], [Qty], [CategoryId], [SupplierId], [BrandId], [ModelId], [ColorId], [MaterialId], [Grade], [Power], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (5, N'000005', N'lens', N'lens', CAST(111.00 AS Decimal(18, 2)), 4, NULL, 1, 1, 10004, NULL, 2, NULL, N'4', N'4', NULL, CAST(N'2021-04-09T00:28:47.007' AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductCategory] ON 
GO
INSERT [dbo].[ProductCategory] ([Id], [NameAr], [NameEn], [ParentId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (1, N'نظارات طبية', N'Medical Glasses', NULL, NULL, NULL, CAST(N'2020-10-23T12:32:42.417' AS DateTime), NULL)
GO
INSERT [dbo].[ProductCategory] ([Id], [NameAr], [NameEn], [ParentId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (2, N'نظارات طبية شمسية', N'Medical sun glasses', 1, NULL, NULL, CAST(N'2020-10-23T12:34:15.977' AS DateTime), NULL)
GO
INSERT [dbo].[ProductCategory] ([Id], [NameAr], [NameEn], [ParentId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (3, N'نظارات كليب', N'Clep On', NULL, N'Mohamed', NULL, CAST(N'2020-10-31T12:34:15.977' AS DateTime), NULL)
GO
INSERT [dbo].[ProductCategory] ([Id], [NameAr], [NameEn], [ParentId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (4, N'نظارات قراءة', N'Reading', NULL, N'Mohamed', NULL, CAST(N'2020-10-31T12:34:15.977' AS DateTime), NULL)
GO
INSERT [dbo].[ProductCategory] ([Id], [NameAr], [NameEn], [ParentId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (5, N'عدسات لاصقة', N'CL', NULL, N'Mohamed', NULL, CAST(N'2020-10-31T12:34:15.977' AS DateTime), NULL)
GO
INSERT [dbo].[ProductCategory] ([Id], [NameAr], [NameEn], [ParentId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (6, N'عدسات لاصقة طبية خاصة', N'CLSP', NULL, N'Mohamed', NULL, CAST(N'2020-10-31T12:34:15.977' AS DateTime), NULL)
GO
INSERT [dbo].[ProductCategory] ([Id], [NameAr], [NameEn], [ParentId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (7, N'محاليل العدسات لاصقة', N'CL Solution', NULL, N'Mohamed', NULL, CAST(N'2020-10-31T12:34:15.977' AS DateTime), NULL)
GO
INSERT [dbo].[ProductCategory] ([Id], [NameAr], [NameEn], [ParentId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (8, N'أخرى', N'Others', NULL, N'Mohamed', NULL, CAST(N'2020-10-31T12:34:15.977' AS DateTime), NULL)
GO
INSERT [dbo].[ProductCategory] ([Id], [NameAr], [NameEn], [ParentId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (9, N'العدسات الطبية', N'Optical Lenses', NULL, N'Mohamed', NULL, CAST(N'2020-10-31T12:34:15.977' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[ProductCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[PurchaseTransactionDetail] ON 
GO
INSERT [dbo].[PurchaseTransactionDetail] ([Id], [Qty], [OfficialUnitPrice], [PurchasePrice], [ChangeRate], [PurchaseTransactionMasterID], [ProductId], [CurrencyId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (1, 5, CAST(25.00 AS Decimal(18, 2)), CAST(30.00 AS Decimal(18, 2)), CAST(3.00 AS Decimal(18, 2)), 10003, 1, 1, NULL, CAST(N'2020-11-22T18:54:55.6966667' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[PurchaseTransactionDetail] ([Id], [Qty], [OfficialUnitPrice], [PurchasePrice], [ChangeRate], [PurchaseTransactionMasterID], [ProductId], [CurrencyId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (2, 5, CAST(55.00 AS Decimal(18, 2)), CAST(60.00 AS Decimal(18, 2)), CAST(3.00 AS Decimal(18, 2)), 10004, 2, 1, NULL, CAST(N'2020-11-22T18:54:55.6966667' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[PurchaseTransactionDetail] ([Id], [Qty], [OfficialUnitPrice], [PurchasePrice], [ChangeRate], [PurchaseTransactionMasterID], [ProductId], [CurrencyId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (3, 5, CAST(35.00 AS Decimal(18, 2)), CAST(40.00 AS Decimal(18, 2)), CAST(3.00 AS Decimal(18, 2)), 10004, 1, 1, NULL, CAST(N'2020-11-22T18:54:55.6966667' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[PurchaseTransactionDetail] ([Id], [Qty], [OfficialUnitPrice], [PurchasePrice], [ChangeRate], [PurchaseTransactionMasterID], [ProductId], [CurrencyId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (4, 2, CAST(20.00 AS Decimal(18, 2)), CAST(22.00 AS Decimal(18, 2)), CAST(3.00 AS Decimal(18, 2)), 10005, 1, 1, NULL, CAST(N'2020-11-22T18:54:55.6966667' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[PurchaseTransactionDetail] ([Id], [Qty], [OfficialUnitPrice], [PurchasePrice], [ChangeRate], [PurchaseTransactionMasterID], [ProductId], [CurrencyId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (5, 2, CAST(23.00 AS Decimal(18, 2)), CAST(25.00 AS Decimal(18, 2)), CAST(3.00 AS Decimal(18, 2)), 10005, 2, 1, NULL, CAST(N'2020-11-22T18:54:55.6966667' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[PurchaseTransactionDetail] ([Id], [Qty], [OfficialUnitPrice], [PurchasePrice], [ChangeRate], [PurchaseTransactionMasterID], [ProductId], [CurrencyId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (6, 11, CAST(23.00 AS Decimal(18, 2)), CAST(25.00 AS Decimal(18, 2)), CAST(3.50 AS Decimal(18, 2)), NULL, 1, 1, NULL, CAST(N'2020-11-22T23:37:52.2000000' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[PurchaseTransactionDetail] ([Id], [Qty], [OfficialUnitPrice], [PurchasePrice], [ChangeRate], [PurchaseTransactionMasterID], [ProductId], [CurrencyId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (7, 1, CAST(10.00 AS Decimal(18, 2)), CAST(11.00 AS Decimal(18, 2)), CAST(3.50 AS Decimal(18, 2)), NULL, 1, 1, NULL, CAST(N'2020-11-23T12:18:55.3500000' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[PurchaseTransactionDetail] ([Id], [Qty], [OfficialUnitPrice], [PurchasePrice], [ChangeRate], [PurchaseTransactionMasterID], [ProductId], [CurrencyId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (8, 2, CAST(22.00 AS Decimal(18, 2)), CAST(23.00 AS Decimal(18, 2)), CAST(3.00 AS Decimal(18, 2)), 10006, 1, 1, NULL, CAST(N'2020-11-24T03:41:11.2933333' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[PurchaseTransactionDetail] ([Id], [Qty], [OfficialUnitPrice], [PurchasePrice], [ChangeRate], [PurchaseTransactionMasterID], [ProductId], [CurrencyId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (9, 2, CAST(22.00 AS Decimal(18, 2)), CAST(23.00 AS Decimal(18, 2)), CAST(3.00 AS Decimal(18, 2)), 10006, 1, 1, NULL, CAST(N'2020-11-24T12:37:09.0433333' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[PurchaseTransactionDetail] ([Id], [Qty], [OfficialUnitPrice], [PurchasePrice], [ChangeRate], [PurchaseTransactionMasterID], [ProductId], [CurrencyId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (10, 5, CAST(55.00 AS Decimal(18, 2)), CAST(55.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), 10007, 1, 1, NULL, CAST(N'2020-12-19T22:30:11.9826236' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[PurchaseTransactionDetail] ([Id], [Qty], [OfficialUnitPrice], [PurchasePrice], [ChangeRate], [PurchaseTransactionMasterID], [ProductId], [CurrencyId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (13, 2, CAST(500.00 AS Decimal(18, 2)), CAST(500.00 AS Decimal(18, 2)), CAST(3.00 AS Decimal(18, 2)), NULL, 1, 1, NULL, CAST(N'2020-12-20T23:50:44.3562701' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[PurchaseTransactionDetail] ([Id], [Qty], [OfficialUnitPrice], [PurchasePrice], [ChangeRate], [PurchaseTransactionMasterID], [ProductId], [CurrencyId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (14, 3, CAST(750.00 AS Decimal(18, 2)), CAST(800.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), NULL, 2, 1, NULL, CAST(N'2020-12-20T23:50:44.3592626' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[PurchaseTransactionDetail] ([Id], [Qty], [OfficialUnitPrice], [PurchasePrice], [ChangeRate], [PurchaseTransactionMasterID], [ProductId], [CurrencyId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (17, 2, CAST(750.00 AS Decimal(18, 2)), CAST(750.00 AS Decimal(18, 2)), CAST(3.00 AS Decimal(18, 2)), NULL, 1, 1, NULL, CAST(N'2020-12-21T00:02:48.8267953' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[PurchaseTransactionDetail] ([Id], [Qty], [OfficialUnitPrice], [PurchasePrice], [ChangeRate], [PurchaseTransactionMasterID], [ProductId], [CurrencyId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (18, 3, CAST(850.00 AS Decimal(18, 2)), CAST(850.00 AS Decimal(18, 2)), CAST(3.00 AS Decimal(18, 2)), NULL, 2, 1, NULL, CAST(N'2020-12-21T00:02:48.8267953' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[PurchaseTransactionDetail] ([Id], [Qty], [OfficialUnitPrice], [PurchasePrice], [ChangeRate], [PurchaseTransactionMasterID], [ProductId], [CurrencyId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (27, 2, CAST(350.00 AS Decimal(18, 2)), CAST(350.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 10011, 1, 1, NULL, CAST(N'2020-12-26T14:24:10.2695496' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[PurchaseTransactionDetail] ([Id], [Qty], [OfficialUnitPrice], [PurchasePrice], [ChangeRate], [PurchaseTransactionMasterID], [ProductId], [CurrencyId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (28, 2, CAST(550.00 AS Decimal(18, 2)), CAST(550.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 10011, 2, 1, NULL, CAST(N'2020-12-26T14:24:10.2795227' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[PurchaseTransactionDetail] ([Id], [Qty], [OfficialUnitPrice], [PurchasePrice], [ChangeRate], [PurchaseTransactionMasterID], [ProductId], [CurrencyId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (10023, 3, CAST(350.00 AS Decimal(18, 2)), CAST(350.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 20009, 1, 1, NULL, CAST(N'2021-01-04T22:55:23.1068280' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[PurchaseTransactionDetail] ([Id], [Qty], [OfficialUnitPrice], [PurchasePrice], [ChangeRate], [PurchaseTransactionMasterID], [ProductId], [CurrencyId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (10024, 3, CAST(550.00 AS Decimal(18, 2)), CAST(550.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 20009, 2, 1, NULL, CAST(N'2021-01-04T22:55:23.1344500' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[PurchaseTransactionDetail] ([Id], [Qty], [OfficialUnitPrice], [PurchasePrice], [ChangeRate], [PurchaseTransactionMasterID], [ProductId], [CurrencyId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (10025, 1, CAST(100.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 20010, 3, 1, NULL, CAST(N'2021-04-05T23:37:43.8142595' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[PurchaseTransactionDetail] ([Id], [Qty], [OfficialUnitPrice], [PurchasePrice], [ChangeRate], [PurchaseTransactionMasterID], [ProductId], [CurrencyId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (10026, 1, CAST(350.00 AS Decimal(18, 2)), CAST(350.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 20011, 1, 1, NULL, CAST(N'2021-08-14T03:04:34.0930648' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[PurchaseTransactionDetail] ([Id], [Qty], [OfficialUnitPrice], [PurchasePrice], [ChangeRate], [PurchaseTransactionMasterID], [ProductId], [CurrencyId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (10027, 1, CAST(100.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 20011, 3, 1, NULL, CAST(N'2021-08-14T03:04:34.1010425' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[PurchaseTransactionDetail] ([Id], [Qty], [OfficialUnitPrice], [PurchasePrice], [ChangeRate], [PurchaseTransactionMasterID], [ProductId], [CurrencyId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (10028, 1, CAST(550.00 AS Decimal(18, 2)), CAST(550.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 20011, 2, 1, NULL, CAST(N'2021-08-14T03:04:34.1019997' AS DateTime2), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[PurchaseTransactionDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[PurchaseTransactionMaster] ON 
GO
INSERT [dbo].[PurchaseTransactionMaster] ([Id], [InvoiceDate], [SupplierInvoiceDate], [InvoiceNo], [SupplierInvoiceNo], [PurchaseAgentName], [TaxFees], [OtherFees], [InvoiceAmount], [FeesAmount], [Status], [Notes], [WarehouseId], [SupplierId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (2, CAST(N'2020-11-20T10:44:14.863' AS DateTime), CAST(N'2020-11-02T20:29:04.0200000' AS DateTime2), N'33625', N'22', N'Mohamed', NULL, CAST(5.0000 AS Decimal(18, 4)), CAST(2500.0000 AS Decimal(18, 4)), CAST(7.0000 AS Decimal(18, 4)), NULL, NULL, 1, 1, NULL, NULL, NULL, CAST(N'2020-11-23T12:18:55.007' AS DateTime))
GO
INSERT [dbo].[PurchaseTransactionMaster] ([Id], [InvoiceDate], [SupplierInvoiceDate], [InvoiceNo], [SupplierInvoiceNo], [PurchaseAgentName], [TaxFees], [OtherFees], [InvoiceAmount], [FeesAmount], [Status], [Notes], [WarehouseId], [SupplierId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (10003, CAST(N'2020-11-21T23:36:44.747' AS DateTime), CAST(N'2020-11-21T23:36:45.9690000' AS DateTime2), N'033626', N'235654', N'khaled', CAST(25.0000 AS Decimal(18, 4)), CAST(5.0000 AS Decimal(18, 4)), CAST(5000.0000 AS Decimal(18, 4)), CAST(30.0000 AS Decimal(18, 4)), 1, NULL, 1, 1, NULL, NULL, CAST(N'2020-11-21T23:37:50.387' AS DateTime), NULL)
GO
INSERT [dbo].[PurchaseTransactionMaster] ([Id], [InvoiceDate], [SupplierInvoiceDate], [InvoiceNo], [SupplierInvoiceNo], [PurchaseAgentName], [TaxFees], [OtherFees], [InvoiceAmount], [FeesAmount], [Status], [Notes], [WarehouseId], [SupplierId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (10004, CAST(N'2020-11-21T23:42:43.567' AS DateTime), CAST(N'2020-11-21T23:42:44.4410000' AS DateTime2), N'033627', N'233654', NULL, NULL, NULL, CAST(0.0000 AS Decimal(18, 4)), NULL, NULL, NULL, 1, 1, NULL, NULL, CAST(N'2020-11-21T23:42:56.437' AS DateTime), NULL)
GO
INSERT [dbo].[PurchaseTransactionMaster] ([Id], [InvoiceDate], [SupplierInvoiceDate], [InvoiceNo], [SupplierInvoiceNo], [PurchaseAgentName], [TaxFees], [OtherFees], [InvoiceAmount], [FeesAmount], [Status], [Notes], [WarehouseId], [SupplierId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (10005, CAST(N'2020-11-22T18:50:25.987' AS DateTime), CAST(N'2020-11-22T18:50:26.9960000' AS DateTime2), N'033628', N'22365', N'ahmed', CAST(12.0000 AS Decimal(18, 4)), CAST(5.0000 AS Decimal(18, 4)), CAST(5000.0000 AS Decimal(18, 4)), CAST(17.0000 AS Decimal(18, 4)), 1, NULL, 1, 1, NULL, NULL, CAST(N'2020-11-22T18:51:26.403' AS DateTime), NULL)
GO
INSERT [dbo].[PurchaseTransactionMaster] ([Id], [InvoiceDate], [SupplierInvoiceDate], [InvoiceNo], [SupplierInvoiceNo], [PurchaseAgentName], [TaxFees], [OtherFees], [InvoiceAmount], [FeesAmount], [Status], [Notes], [WarehouseId], [SupplierId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (10006, CAST(N'2020-11-24T03:37:50.000' AS DateTime), CAST(N'2020-11-24T03:37:50.0020000' AS DateTime2), N'033629', N'2236', N'ahmed ', NULL, CAST(2.0000 AS Decimal(18, 4)), CAST(5000.0000 AS Decimal(18, 4)), NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, CAST(N'2020-11-24T12:37:08.727' AS DateTime))
GO
INSERT [dbo].[PurchaseTransactionMaster] ([Id], [InvoiceDate], [SupplierInvoiceDate], [InvoiceNo], [SupplierInvoiceNo], [PurchaseAgentName], [TaxFees], [OtherFees], [InvoiceAmount], [FeesAmount], [Status], [Notes], [WarehouseId], [SupplierId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (10007, CAST(N'2020-12-19T22:28:13.663' AS DateTime), CAST(N'2020-12-01T22:28:16.7870000' AS DateTime2), N'033630', N'555', NULL, NULL, NULL, CAST(0.0000 AS Decimal(18, 4)), NULL, NULL, NULL, 1, 1, NULL, NULL, CAST(N'2020-12-19T22:30:12.000' AS DateTime), NULL)
GO
INSERT [dbo].[PurchaseTransactionMaster] ([Id], [InvoiceDate], [SupplierInvoiceDate], [InvoiceNo], [SupplierInvoiceNo], [PurchaseAgentName], [TaxFees], [OtherFees], [InvoiceAmount], [FeesAmount], [Status], [Notes], [WarehouseId], [SupplierId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (10011, CAST(N'2020-12-26T14:23:29.603' AS DateTime), CAST(N'2020-12-26T14:23:30.5550000' AS DateTime2), N'033631', N'2323', N'محمد محمود', CAST(5.0000 AS Decimal(18, 4)), CAST(5.0000 AS Decimal(18, 4)), CAST(1800.0000 AS Decimal(18, 4)), CAST(10.0000 AS Decimal(18, 4)), 1, NULL, 1, 1, NULL, NULL, CAST(N'2020-12-26T14:24:10.280' AS DateTime), NULL)
GO
INSERT [dbo].[PurchaseTransactionMaster] ([Id], [InvoiceDate], [SupplierInvoiceDate], [InvoiceNo], [SupplierInvoiceNo], [PurchaseAgentName], [TaxFees], [OtherFees], [InvoiceAmount], [FeesAmount], [Status], [Notes], [WarehouseId], [SupplierId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (20009, CAST(N'2021-01-04T22:54:21.237' AS DateTime), CAST(N'2021-01-04T22:54:22.9060000' AS DateTime2), N'033632', N'223', N'Mohamed', CAST(5.0000 AS Decimal(18, 4)), CAST(5.0000 AS Decimal(18, 4)), CAST(2700.0000 AS Decimal(18, 4)), CAST(10.0000 AS Decimal(18, 4)), 1, NULL, 1, 1, NULL, NULL, CAST(N'2021-01-04T22:55:23.133' AS DateTime), NULL)
GO
INSERT [dbo].[PurchaseTransactionMaster] ([Id], [InvoiceDate], [SupplierInvoiceDate], [InvoiceNo], [SupplierInvoiceNo], [PurchaseAgentName], [TaxFees], [OtherFees], [InvoiceAmount], [FeesAmount], [Status], [Notes], [WarehouseId], [SupplierId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (20010, CAST(N'2021-01-04T00:00:00.000' AS DateTime), CAST(N'2021-02-03T00:00:00.0000000' AS DateTime2), N'033633', N'22222', N'test', CAST(222.0000 AS Decimal(18, 4)), CAST(22.0000 AS Decimal(18, 4)), CAST(100.0000 AS Decimal(18, 4)), CAST(244.0000 AS Decimal(18, 4)), 1, N'testestestest', 1, 1, NULL, NULL, CAST(N'2021-04-05T23:37:43.817' AS DateTime), NULL)
GO
INSERT [dbo].[PurchaseTransactionMaster] ([Id], [InvoiceDate], [SupplierInvoiceDate], [InvoiceNo], [SupplierInvoiceNo], [PurchaseAgentName], [TaxFees], [OtherFees], [InvoiceAmount], [FeesAmount], [Status], [Notes], [WarehouseId], [SupplierId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (20011, NULL, CAST(N'2021-05-07T00:00:00.0000000' AS DateTime2), N'033634', N'2635', N'Mohamed', CAST(2.0000 AS Decimal(18, 4)), CAST(2.0000 AS Decimal(18, 4)), CAST(1000.0000 AS Decimal(18, 4)), CAST(4.0000 AS Decimal(18, 4)), 1, NULL, 1, 2, NULL, NULL, CAST(N'2021-08-14T03:04:34.103' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[PurchaseTransactionMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[Region] ON 
GO
INSERT [dbo].[Region] ([Id], [NameAr], [NameEn], [CityId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (1, N'مدينة نصر', N'Nasr City', 1, NULL, NULL, CAST(N'2020-10-20T00:40:29.033' AS DateTime), NULL)
GO
INSERT [dbo].[Region] ([Id], [NameAr], [NameEn], [CityId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (2, N'test', N'test', 2, NULL, NULL, CAST(N'2021-08-14T00:54:14.227' AS DateTime), NULL)
GO
INSERT [dbo].[Region] ([Id], [NameAr], [NameEn], [CityId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (3, N'المنطقة الوسطى', N'The Middle Region', 6, NULL, NULL, CAST(N'2021-10-07T00:46:25.810' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[Region] OFF
GO
SET IDENTITY_INSERT [dbo].[SalesTransaction] ON 
GO
INSERT [dbo].[SalesTransaction] ([Id], [Date], [DocNo], [SalesAgentName], [WarehouserName], [Discount], [InvoiceAmount], [DiscountAmount], [ReturnAmount], [VATValue], [CostAmount], [PaidAmount], [DeliveryDate], [WarehouseId], [CustomerId], [BranchId], [Type], [IsDelivered], [Notes], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (1, CAST(N'2019-01-01T00:00:00.000' AS DateTime), N'000001', N'خالد ابراهيم', N'عماد الدين حسن', CAST(20.00 AS Decimal(18, 2)), CAST(320.00 AS Decimal(18, 2)), CAST(20.00 AS Decimal(18, 2)), NULL, CAST(200.00 AS Decimal(18, 2)), CAST(300.00 AS Decimal(18, 2)), CAST(200.00 AS Decimal(18, 2)), CAST(N'2018-01-01T00:00:00.000' AS DateTime), 1, 1, NULL, 2, NULL, N'ملاحظات', N'Osama', NULL, CAST(N'2020-01-01T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[SalesTransaction] ([Id], [Date], [DocNo], [SalesAgentName], [WarehouserName], [Discount], [InvoiceAmount], [DiscountAmount], [ReturnAmount], [VATValue], [CostAmount], [PaidAmount], [DeliveryDate], [WarehouseId], [CustomerId], [BranchId], [Type], [IsDelivered], [Notes], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (2, NULL, N'000002', NULL, NULL, CAST(5.00 AS Decimal(18, 2)), CAST(495.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), NULL, CAST(5.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 1, 4, NULL, 2, NULL, N'test', NULL, NULL, CAST(N'2021-10-21T16:57:47.800' AS DateTime), CAST(N'2021-10-21T16:57:45.497' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[SalesTransaction] OFF
GO
SET IDENTITY_INSERT [dbo].[SalesTransactionDetail] ON 
GO
INSERT [dbo].[SalesTransactionDetail] ([Id], [Qty], [Discount], [VATValue], [SalesPrice], [TransactionId], [MedicalLensIMasterd], [ProductId]) VALUES (1, 2, CAST(120.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(125.00 AS Decimal(18, 2)), 1, NULL, 1)
GO
INSERT [dbo].[SalesTransactionDetail] ([Id], [Qty], [Discount], [VATValue], [SalesPrice], [TransactionId], [MedicalLensIMasterd], [ProductId]) VALUES (2, 5, CAST(65.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(80.00 AS Decimal(18, 2)), 1, NULL, 2)
GO
INSERT [dbo].[SalesTransactionDetail] ([Id], [Qty], [Discount], [VATValue], [SalesPrice], [TransactionId], [MedicalLensIMasterd], [ProductId]) VALUES (3, 10, CAST(55.00 AS Decimal(18, 2)), CAST(15.00 AS Decimal(18, 2)), CAST(50.00 AS Decimal(18, 2)), 2, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[SalesTransactionDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[Supplier] ON 
GO
INSERT [dbo].[Supplier] ([Id], [Code], [NameAr], [NameEn], [SalesManName], [Phone], [Mobile], [Email], [FaxNo], [Address], [Balance], [BankName], [BankAccount], [RegionId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn], [IsExternal]) VALUES (1, N'001', N'المتحدة للتوريد', N'Motaheda', N'Mohamed Hassa', N'123', N'123', N'a@a.com', N'123', N'giza', CAST(550.00 AS Decimal(18, 2)), N'QNB', N'123', 1, N'Osama', NULL, CAST(N'2020-01-01T00:00:00.000' AS DateTime), CAST(N'2020-12-19T23:02:44.787' AS DateTime), 0)
GO
INSERT [dbo].[Supplier] ([Id], [Code], [NameAr], [NameEn], [SalesManName], [Phone], [Mobile], [Email], [FaxNo], [Address], [Balance], [BankName], [BankAccount], [RegionId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn], [IsExternal]) VALUES (2, N'001', N'الأصدقاء جروب', N'Friends Group', N'Osama', N'22364', N'6335', N'a@a.com', N'123', N'giza', CAST(550.00 AS Decimal(18, 2)), N'QNB', N'123', 1, N'Osama', NULL, CAST(N'2020-01-01T00:00:00.000' AS DateTime), CAST(N'2020-12-19T23:02:44.787' AS DateTime), 1)
GO
INSERT [dbo].[Supplier] ([Id], [Code], [NameAr], [NameEn], [SalesManName], [Phone], [Mobile], [Email], [FaxNo], [Address], [Balance], [BankName], [BankAccount], [RegionId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn], [IsExternal]) VALUES (3, N'333', N'محمد', NULL, N'ahmed', N'0122655455', N'0102236555', N'mohamed@gmail.com', N'2235', N'test address', NULL, N'tet', N'0023', NULL, NULL, NULL, CAST(N'2021-09-11T22:12:02.990' AS DateTime), CAST(N'2021-09-11T22:11:59.837' AS DateTime), 0)
GO
SET IDENTITY_INSERT [dbo].[Supplier] OFF
GO
SET IDENTITY_INSERT [dbo].[SystemConfiguration] ON 
GO
INSERT [dbo].[SystemConfiguration] ([Id], [CompanyNameAr], [CompanyNameEn], [DisableSMSNotifications], [DisableEmailNotifications], [IsSmtpAuthenticated], [SmtpPort], [SmtpEnableSSL], [EmailFromName], [SmtpServer], [SmtpUserName], [SmtpPassword], [EmailFromAddress], [ApplicationUrl], [SMSSenderName], [LogoId]) VALUES (1, N'المغربي للبصريات2', N'Maghraby Optics2', 0, 0, NULL, NULL, NULL, N'المغربي للبصريات2', NULL, NULL, NULL, N'm2@maghraby.com', NULL, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[SystemConfiguration] OFF
GO
SET IDENTITY_INSERT [dbo].[SystemPage] ON 
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (1, N'الحسابات والمستخدمين', N'Users & Accounts', N'None', N'users', 12, 1, NULL)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (2, N'إدارة الوظائف', N'Manage Roles', N'Group/Index', NULL, 2, 1, 1)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (3, N'إدارة المستخدمين', N'Manage Users', N'User/Index', NULL, 1, 1, 1)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (4, N'بيانات النظام', N'Setup Data', N'None', N'exchange', 1, 1, NULL)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (5, N'إدارة الدول', N'Manage Countries', N'Country/Index', NULL, 3, 1, 4)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (6, N'إدارة المدن', N'Manage Cities', N'City/Index', NULL, 4, 1, 4)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (7, N'إدارة العملاء', N'Manage Customers', N'Customer/Index', NULL, 1, 1, 19)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (8, N'إدارة الفروع', N'Manage Branches', N'Branch/Index', NULL, 1, 1, 4)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (9, N'إدارة العملات', N'Manage Currencies', N'Currency/Index', NULL, 6, 1, 4)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (10, N'إدارة الموردين', N'Manage Suppliers', N'Supplier/Index', NULL, 2, 1, 4)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (11, N'إدارة المناطق', N'Manage Regions', N'Region/Index', NULL, 5, 1, 4)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (12, N'إدارة المخازن', N'Inventories', N'None', N'users', 2, 1, NULL)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (13, N'المخازن', N'Manage Warehouses', N'Warehouse/Index', NULL, 1, 1, 12)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (14, N'التصنيفات', N'Manage Categories', N'Category/Index', NULL, 2, 1, 12)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (15, N'إدارة المنتجات', N'Manage Products', N'None', N'eye', 3, 1, NULL)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (16, N'إذن إضافة مخزني', N'Add Inventory', N'WarehouseAdd/Index', NULL, 5, 1, 12)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (17, N'إذن صرف مخزني', N'Subtract Inventory', N'WarehouseSubtract/Index', NULL, 6, 1, 12)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (18, N'إذن تحويل مخزني', N'Inventory Transfer', N'WarehouseTransfer/Index', NULL, 7, 1, 12)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (19, N'إدارة المبيعات', N'Sales Management', N'None', N'shopping-cart', 3, 1, NULL)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (20, N'عمليات البيع', N'Sales Transactions', N'SalesTransaction/Index', NULL, 2, 1, 19)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (21, N'إدارة المشتريات', N'Purchase Management', N'None', N'users', 4, 1, NULL)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (22, N'عمليات الشراء', N'Purchase Transactions', N'PurchaseTransaction/Index', NULL, 1, 1, 21)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (23, N'إعدادات النظام', N'System Settings', N'None', N'users', 5, 1, NULL)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (24, N'الإعدادات', N'Settings', N'SystemConfiguration/Index', NULL, 1, 1, 23)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (25, N'إدارة التنبيهات', N'Notifications', N'Notification/Index', NULL, 2, 1, 23)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (26, N'إدارة الحالات المرضية', N'Disease', N'Disease/Index', NULL, 7, 1, 4)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (27, N'العمليات المخزنية', N'Inventory Transactions', N'WarehouseTransaction/Index', NULL, 4, 1, 12)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (28, N'إضافة مرتجع مبيعات', N'Sales Returns', N'SalesReturn/Index', NULL, 4, 1, 19)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (29, N'إضافة فاتورة بيع', N'Sales Invoices', N'SalesInvoice/Index', NULL, 3, 1, 19)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (30, N'إدارة الرسائل النصية', N'Manage SMS', N'sms/Index', NULL, 3, 1, 23)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (31, N'إدارة البريد الإلكترونى', N'Manage Emails', N'email/Index', NULL, 4, 1, 23)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (32, N'إدارة الماركات', N'Manage Brands', N'brand/Index', NULL, 8, 1, 4)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (33, N'إدارة الموديلات', N'Manage Models', N'BrandModel/Index', NULL, 9, 1, 4)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (34, N'إدارة ألوان الموديلات', N'Manage Colors', N'ModelColor/Index', NULL, 10, 1, 4)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (35, N'فاتورة مشتريات', N'Purchase Bill', N'PurchaseBill/Index', NULL, 2, 1, 21)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (36, N'الإهلاك المخزنى', N'Depreciation', N'Depreciation/Index', NULL, 8, 1, 12)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (37, N'التقارير', N'Reports', N'none', N'users', 6, 1, NULL)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (38, N'تقرير المشتريات', N'Purchase Report', N'PurchaseReportVeiw/Index', NULL, 1, 1, 37)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (39, N'تقرير المبيعات', N'Sales Report', N'SalesReport/Index', N'Null', 2, 1, 37)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (40, N'تقرير العدسات الطبية', N'Medical Lens Report', N'MedicalLensReport/Index', NULL, 3, 1, 37)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (41, N'جرد مخزني', N'Inventory Verification', N'WarehouseVerification/Index', NULL, 8, 1, 12)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (42, N'إدارة الأطباء', N'Manage Doctors', N'Doctor/Index', NULL, 11, 1, 4)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (43, N'إدارة المنتجات', N'Manage Products', N'Product/Index', NULL, 1, 1, 15)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (44, N'Coating Diagram', N'Coating Diagram', N'CoatingDiagram/Index', NULL, 2, 1, 15)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (45, N'Lens Index', N'Lens Index', N'LenseIndex/Index', NULL, 3, 1, 15)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (46, N'Multi-focal Design', N'Multi-focal Design', N'MultifocalDesign/Index', NULL, 4, 1, 15)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (47, N'Version Type', N'Manage Products', N'VersionType/Index', NULL, 5, 1, 15)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (48, N'العدسات الطبية', N'Medical Lens', N'None', NULL, 7, 1, NULL)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (49, N'إدارة العدسات الطبية', N'Manage Medical Lens', N'MedicalLensMaster/Index', NULL, 1, 1, 48)
GO
INSERT [dbo].[SystemPage] ([Id], [NameAr], [NameEn], [URL], [IconText], [Sequence], [Enabled], [ParentId]) VALUES (50, N'إدارة تفاصيل العدسات الطبية', N'Manage Medical Lens Details', N'MedicalLensDetails/Index', NULL, 2, 1, 48)
GO
SET IDENTITY_INSERT [dbo].[SystemPage] OFF
GO
SET IDENTITY_INSERT [dbo].[Upload] ON 
GO
INSERT [dbo].[Upload] ([Id], [FileName]) VALUES (1, N'logo.png')
GO
INSERT [dbo].[Upload] ([Id], [FileName]) VALUES (9, N'f1c39dcad8c94d9bab327d44f9985b00.jpg')
GO
SET IDENTITY_INSERT [dbo].[Upload] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([Id], [Code], [NameAr], [NameEn], [IdentityNo], [Email], [WorkEmail], [HomePhone], [CurrentJob], [Qualification], [GraduationYear], [Skills], [UserName], [Password], [ActivationCode], [Address], [JoiningDate], [MorningFrom], [MorningTo], [EveningFrom], [EveningTo], [Status], [ImageId], [NationalityId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn], [JobId], [Mobile], [DateOfBirth]) VALUES (1, NULL, N'مدير النظام2', N'System Admin2', NULL, N'a2@a.com', NULL, NULL, NULL, NULL, NULL, NULL, N'superadmin', N'123', N'123', NULL, NULL, NULL, NULL, NULL, NULL, 1, 9, NULL, NULL, NULL, NULL, CAST(N'2020-10-31T16:14:18.303' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[User] ([Id], [Code], [NameAr], [NameEn], [IdentityNo], [Email], [WorkEmail], [HomePhone], [CurrentJob], [Qualification], [GraduationYear], [Skills], [UserName], [Password], [ActivationCode], [Address], [JoiningDate], [MorningFrom], [MorningTo], [EveningFrom], [EveningTo], [Status], [ImageId], [NationalityId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn], [JobId], [Mobile], [DateOfBirth]) VALUES (2, NULL, N'مستخدم', N'user', NULL, N'a@a.com', NULL, NULL, NULL, NULL, NULL, NULL, N'user', N'123', N'123', NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[User] ([Id], [Code], [NameAr], [NameEn], [IdentityNo], [Email], [WorkEmail], [HomePhone], [CurrentJob], [Qualification], [GraduationYear], [Skills], [UserName], [Password], [ActivationCode], [Address], [JoiningDate], [MorningFrom], [MorningTo], [EveningFrom], [EveningTo], [Status], [ImageId], [NationalityId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn], [JobId], [Mobile], [DateOfBirth]) VALUES (3, NULL, N'مندوب مبيعات', N'Sales Agent', NULL, N'a@a.com', NULL, NULL, NULL, NULL, NULL, NULL, N'Osama', N'123', N'1f851a7f-6c54-4d99-8e79-cd651699652a', NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, CAST(N'2020-10-23T10:27:42.070' AS DateTime), CAST(N'2020-10-23T11:26:40.650' AS DateTime), NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UserGroup] ON 
GO
INSERT [dbo].[UserGroup] ([Id], [UserId], [GroupId]) VALUES (1, 1, 1)
GO
INSERT [dbo].[UserGroup] ([Id], [UserId], [GroupId]) VALUES (2, 2, 4)
GO
INSERT [dbo].[UserGroup] ([Id], [UserId], [GroupId]) VALUES (3, 3, 3)
GO
SET IDENTITY_INSERT [dbo].[UserGroup] OFF
GO
SET IDENTITY_INSERT [dbo].[UserPageAction] ON 
GO
INSERT [dbo].[UserPageAction] ([Id], [UserId], [PageActionId], [Mode]) VALUES (10, 3, 2, 2)
GO
INSERT [dbo].[UserPageAction] ([Id], [UserId], [PageActionId], [Mode]) VALUES (11, 3, 5, 2)
GO
INSERT [dbo].[UserPageAction] ([Id], [UserId], [PageActionId], [Mode]) VALUES (12, 3, 6, 2)
GO
SET IDENTITY_INSERT [dbo].[UserPageAction] OFF
GO
SET IDENTITY_INSERT [dbo].[Warehouse] ON 
GO
INSERT [dbo].[Warehouse] ([Id], [NameAr], [NameEn], [WarehouseType], [Address], [EmployeeId], [RegionId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (1, N'مخزن مدينة نصر', N'Nasr city warehouse', NULL, N'مدينة نصر', 2, 1, NULL, NULL, CAST(N'2020-10-23T12:28:00.030' AS DateTime), CAST(N'2021-10-14T23:34:27.577' AS DateTime))
GO
INSERT [dbo].[Warehouse] ([Id], [NameAr], [NameEn], [WarehouseType], [Address], [EmployeeId], [RegionId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (2, N'مخزن عين شمس', N'3en shams warehouse', NULL, N'عين شمس', 3, 2, N'Osama', NULL, CAST(N'2020-01-01T00:00:00.000' AS DateTime), CAST(N'2021-10-21T16:46:31.813' AS DateTime))
GO
INSERT [dbo].[Warehouse] ([Id], [NameAr], [NameEn], [WarehouseType], [Address], [EmployeeId], [RegionId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (3, N'ماوية الرس', N'Mawya AlRass', NULL, NULL, 1, 3, NULL, NULL, CAST(N'2021-10-14T23:32:31.417' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[Warehouse] OFF
GO
SET IDENTITY_INSERT [dbo].[WarehouseTransaction] ON 
GO
INSERT [dbo].[WarehouseTransaction] ([Id], [Date], [DocNo], [EmployeeName], [TransactionType], [Status], [Notes], [CustomerId], [SupplierId], [WarehouseId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (1, CAST(N'2020-01-01T00:00:00.000' AS DateTime), N'000123', N'محمد علي', 1, 1, N'ملاحظات', 1, 1, 1, N'Osama', NULL, CAST(N'2020-01-01T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[WarehouseTransaction] ([Id], [Date], [DocNo], [EmployeeName], [TransactionType], [Status], [Notes], [CustomerId], [SupplierId], [WarehouseId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (4, CAST(N'2020-11-24T00:35:02.250' AS DateTime), N'000124', N'علاء محمد', 1, 1, N'ملاحظات', NULL, NULL, 1, NULL, NULL, CAST(N'2020-11-21T00:35:47.480' AS DateTime), NULL)
GO
INSERT [dbo].[WarehouseTransaction] ([Id], [Date], [DocNo], [EmployeeName], [TransactionType], [Status], [Notes], [CustomerId], [SupplierId], [WarehouseId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (5, CAST(N'2020-11-24T13:16:54.270' AS DateTime), N'000125', N'علاء', 1, 1, N'ملاحظات', NULL, NULL, 2, NULL, NULL, CAST(N'2020-11-21T13:18:20.370' AS DateTime), NULL)
GO
INSERT [dbo].[WarehouseTransaction] ([Id], [Date], [DocNo], [EmployeeName], [TransactionType], [Status], [Notes], [CustomerId], [SupplierId], [WarehouseId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (6, CAST(N'2020-11-13T18:21:06.623' AS DateTime), N'000126', N'محمد الامين', 2, 1, N'ملاحظات', NULL, NULL, 2, NULL, NULL, CAST(N'2020-11-21T18:22:18.297' AS DateTime), NULL)
GO
INSERT [dbo].[WarehouseTransaction] ([Id], [Date], [DocNo], [EmployeeName], [TransactionType], [Status], [Notes], [CustomerId], [SupplierId], [WarehouseId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (7, CAST(N'2020-11-21T18:46:04.543' AS DateTime), N'000127', N'علي محمد', 1, 1, N'ملاحظات', NULL, NULL, 1, NULL, NULL, CAST(N'2020-11-21T18:46:58.690' AS DateTime), NULL)
GO
INSERT [dbo].[WarehouseTransaction] ([Id], [Date], [DocNo], [EmployeeName], [TransactionType], [Status], [Notes], [CustomerId], [SupplierId], [WarehouseId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (8, CAST(N'2020-11-21T18:51:08.037' AS DateTime), N'000128', N'أمين المخزن', 2, 1, N'ملاحظات', NULL, NULL, 2, NULL, NULL, CAST(N'2020-11-21T18:51:49.730' AS DateTime), NULL)
GO
INSERT [dbo].[WarehouseTransaction] ([Id], [Date], [DocNo], [EmployeeName], [TransactionType], [Status], [Notes], [CustomerId], [SupplierId], [WarehouseId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (9, CAST(N'2021-01-04T22:57:52.817' AS DateTime), N'000129', NULL, 2, 1, N'test note', NULL, NULL, 1, NULL, NULL, CAST(N'2021-01-04T22:58:25.147' AS DateTime), NULL)
GO
INSERT [dbo].[WarehouseTransaction] ([Id], [Date], [DocNo], [EmployeeName], [TransactionType], [Status], [Notes], [CustomerId], [SupplierId], [WarehouseId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (10, CAST(N'2021-01-04T23:20:05.500' AS DateTime), N'000130', NULL, 4, 1, NULL, NULL, NULL, 1, NULL, NULL, CAST(N'2021-01-04T23:20:27.797' AS DateTime), NULL)
GO
INSERT [dbo].[WarehouseTransaction] ([Id], [Date], [DocNo], [EmployeeName], [TransactionType], [Status], [Notes], [CustomerId], [SupplierId], [WarehouseId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (11, CAST(N'2021-01-04T23:20:55.517' AS DateTime), N'000131', NULL, 4, 1, NULL, NULL, NULL, 1, NULL, NULL, CAST(N'2021-01-04T23:21:02.223' AS DateTime), NULL)
GO
INSERT [dbo].[WarehouseTransaction] ([Id], [Date], [DocNo], [EmployeeName], [TransactionType], [Status], [Notes], [CustomerId], [SupplierId], [WarehouseId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (12, NULL, N'000002', NULL, 2, 0, N'sales invoice transaction no {invoice no}', 4, NULL, 1, NULL, NULL, CAST(N'2021-10-21T16:57:47.800' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[WarehouseTransaction] OFF
GO
SET IDENTITY_INSERT [dbo].[WarehouseTransactionDetail] ON 
GO
INSERT [dbo].[WarehouseTransactionDetail] ([Id], [Qty], [ProductId], [MedicalLensIMasterd], [TransactionId]) VALUES (1, 2, 1, NULL, 1)
GO
INSERT [dbo].[WarehouseTransactionDetail] ([Id], [Qty], [ProductId], [MedicalLensIMasterd], [TransactionId]) VALUES (2, 5, 2, NULL, 1)
GO
INSERT [dbo].[WarehouseTransactionDetail] ([Id], [Qty], [ProductId], [MedicalLensIMasterd], [TransactionId]) VALUES (3, 12, 1, NULL, 4)
GO
INSERT [dbo].[WarehouseTransactionDetail] ([Id], [Qty], [ProductId], [MedicalLensIMasterd], [TransactionId]) VALUES (4, 7, 2, NULL, 4)
GO
INSERT [dbo].[WarehouseTransactionDetail] ([Id], [Qty], [ProductId], [MedicalLensIMasterd], [TransactionId]) VALUES (5, 50, 2, NULL, 5)
GO
INSERT [dbo].[WarehouseTransactionDetail] ([Id], [Qty], [ProductId], [MedicalLensIMasterd], [TransactionId]) VALUES (6, 4, 2, NULL, 5)
GO
INSERT [dbo].[WarehouseTransactionDetail] ([Id], [Qty], [ProductId], [MedicalLensIMasterd], [TransactionId]) VALUES (7, 12, 2, NULL, 6)
GO
INSERT [dbo].[WarehouseTransactionDetail] ([Id], [Qty], [ProductId], [MedicalLensIMasterd], [TransactionId]) VALUES (8, 25, 1, NULL, 7)
GO
INSERT [dbo].[WarehouseTransactionDetail] ([Id], [Qty], [ProductId], [MedicalLensIMasterd], [TransactionId]) VALUES (9, 25, 2, NULL, 7)
GO
INSERT [dbo].[WarehouseTransactionDetail] ([Id], [Qty], [ProductId], [MedicalLensIMasterd], [TransactionId]) VALUES (10, 22, 2, NULL, 8)
GO
INSERT [dbo].[WarehouseTransactionDetail] ([Id], [Qty], [ProductId], [MedicalLensIMasterd], [TransactionId]) VALUES (11, 0, 1, NULL, 9)
GO
INSERT [dbo].[WarehouseTransactionDetail] ([Id], [Qty], [ProductId], [MedicalLensIMasterd], [TransactionId]) VALUES (12, 1, 1, NULL, 10)
GO
INSERT [dbo].[WarehouseTransactionDetail] ([Id], [Qty], [ProductId], [MedicalLensIMasterd], [TransactionId]) VALUES (13, 0, 1, NULL, 11)
GO
INSERT [dbo].[WarehouseTransactionDetail] ([Id], [Qty], [ProductId], [MedicalLensIMasterd], [TransactionId]) VALUES (14, 10, 1, NULL, 12)
GO
SET IDENTITY_INSERT [dbo].[WarehouseTransactionDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[WarehouseVerification] ON 
GO
INSERT [dbo].[WarehouseVerification] ([Id], [DocumentNo], [Date], [WarehouserName], [IsSettled], [Notes], [WarehouseId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (1, N'00001', CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Osama Wafi', 0, N'notessssssss', 1, N'Osama', NULL, CAST(N'2021-01-01T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[WarehouseVerification] ([Id], [DocumentNo], [Date], [WarehouserName], [IsSettled], [Notes], [WarehouseId], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (2, N'000002', CAST(N'2021-02-19T14:40:05.100' AS DateTime), N'test', 0, N'test', 1, NULL, NULL, CAST(N'2021-02-19T14:40:07.127' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[WarehouseVerification] OFF
GO
SET IDENTITY_INSERT [dbo].[WarehouseVerificationDetail] ON 
GO
INSERT [dbo].[WarehouseVerificationDetail] ([Id], [TransQty], [ActualQty], [UnitOfficialPrice], [WarehouseVerificationId], [MedicalLensIMasterd], [ProductId]) VALUES (1, 4, 2, CAST(20.0000 AS Decimal(18, 4)), 1, NULL, 1)
GO
INSERT [dbo].[WarehouseVerificationDetail] ([Id], [TransQty], [ActualQty], [UnitOfficialPrice], [WarehouseVerificationId], [MedicalLensIMasterd], [ProductId]) VALUES (2, 39, 38, CAST(350.0000 AS Decimal(18, 4)), 2, NULL, 1)
GO
INSERT [dbo].[WarehouseVerificationDetail] ([Id], [TransQty], [ActualQty], [UnitOfficialPrice], [WarehouseVerificationId], [MedicalLensIMasterd], [ProductId]) VALUES (3, 37, 38, CAST(550.0000 AS Decimal(18, 4)), 2, NULL, 2)
GO
SET IDENTITY_INSERT [dbo].[WarehouseVerificationDetail] OFF
GO
SET IDENTITY_INSERT [lookup].[Brand] ON 
GO
INSERT [lookup].[Brand] ([Id], [BrandName]) VALUES (1, N'Kelvin Kelin')
GO
INSERT [lookup].[Brand] ([Id], [BrandName]) VALUES (10003, N'Ray Ban')
GO
INSERT [lookup].[Brand] ([Id], [BrandName]) VALUES (10004, N'Crizal')
GO
SET IDENTITY_INSERT [lookup].[Brand] OFF
GO
SET IDENTITY_INSERT [lookup].[CoatingDiagram] ON 
GO
INSERT [lookup].[CoatingDiagram] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (2, N'TestCoati ', CAST(N'2021-03-15T00:00:00.0000000' AS DateTime2), N'Mohammed', NULL, NULL)
GO
INSERT [lookup].[CoatingDiagram] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (3, N'test      ', CAST(N'2021-03-15T23:49:20.6778827' AS DateTime2), NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [lookup].[CoatingDiagram] OFF
GO
SET IDENTITY_INSERT [lookup].[CYLs] ON 
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (1, N'-06.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (2, N'-05.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (3, N'-05.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (4, N'-05.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (5, N'-05.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (6, N'-04.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (7, N'-04.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (8, N'-04.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (9, N'-04.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (10, N'-03.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (11, N'-03.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (12, N'-03.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (13, N'-03.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (14, N'-02.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (15, N'-02.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (16, N'-02.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (17, N'-02.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (18, N'-01.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (19, N'-01.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (20, N'-01.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (21, N'-01.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (22, N'-00.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (23, N'-00.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (24, N'-00.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (25, N'00.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (26, N'+00.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (27, N'+00.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (28, N'+00.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (29, N'+01.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (30, N'+01.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (31, N'+01.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (32, N'+01.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (33, N'+02.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (34, N'+02.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (35, N'+02.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (36, N'+02.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (37, N'+03.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (38, N'+03.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (39, N'+03.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (40, N'+03.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (41, N'+04.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (42, N'+04.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (43, N'+04.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (44, N'+04.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (45, N'+05.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (46, N'+05.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (47, N'+05.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (48, N'+05.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[CYLs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (49, N'+06.00', NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [lookup].[CYLs] OFF
GO
SET IDENTITY_INSERT [lookup].[LenseIndex] ON 
GO
INSERT [lookup].[LenseIndex] ([Id], [Index], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (1, 1.5, CAST(N'2021-03-15T00:00:00.0000000' AS DateTime2), N'Mohammed', NULL, NULL)
GO
INSERT [lookup].[LenseIndex] ([Id], [Index], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (2, 1.53, CAST(N'2021-03-15T23:49:42.4095566' AS DateTime2), NULL, NULL, NULL)
GO
INSERT [lookup].[LenseIndex] ([Id], [Index], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (3, 1.56, CAST(N'2021-03-15T23:49:42.4095566' AS DateTime2), NULL, NULL, NULL)
GO
INSERT [lookup].[LenseIndex] ([Id], [Index], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (4, 1.59, CAST(N'2021-03-15T23:49:42.4095566' AS DateTime2), NULL, NULL, NULL)
GO
INSERT [lookup].[LenseIndex] ([Id], [Index], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (5, 1.61, CAST(N'2021-03-15T23:49:42.4095566' AS DateTime2), NULL, NULL, NULL)
GO
INSERT [lookup].[LenseIndex] ([Id], [Index], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (6, 1.67, CAST(N'2021-03-15T23:49:42.4095566' AS DateTime2), NULL, NULL, NULL)
GO
INSERT [lookup].[LenseIndex] ([Id], [Index], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (7, 1.7, CAST(N'2021-03-15T23:49:42.4095566' AS DateTime2), NULL, NULL, NULL)
GO
INSERT [lookup].[LenseIndex] ([Id], [Index], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (8, 1.74, CAST(N'2021-03-15T23:49:42.4095566' AS DateTime2), NULL, NULL, NULL)
GO
INSERT [lookup].[LenseIndex] ([Id], [Index], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (9, 1.8, CAST(N'2021-03-15T23:49:42.4095566' AS DateTime2), NULL, NULL, NULL)
GO
INSERT [lookup].[LenseIndex] ([Id], [Index], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (10, 1.9, CAST(N'2021-03-15T23:49:42.4095566' AS DateTime2), NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [lookup].[LenseIndex] OFF
GO
SET IDENTITY_INSERT [lookup].[MultifocalDesign] ON 
GO
INSERT [lookup].[MultifocalDesign] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (1, N'test multifocal', CAST(N'2021-03-15T00:00:00.0000000' AS DateTime2), N'Mohamed', NULL, NULL)
GO
SET IDENTITY_INSERT [lookup].[MultifocalDesign] OFF
GO
SET IDENTITY_INSERT [lookup].[SPHs] ON 
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (1, N'+20', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (2, N'+19.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (3, N'+19.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (4, N'+19.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (5, N'+19.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (6, N'+18.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (7, N'+18.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (8, N'+18.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (9, N'+18.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (10, N'+17.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (11, N'+17.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (12, N'+17.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (13, N'+17.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (14, N'+16.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (15, N'+16.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (16, N'+16.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (17, N'+16.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (18, N'+15.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (19, N'+15.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (20, N'+15.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (21, N'+15.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (22, N'+14.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (23, N'+14.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (24, N'+14.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (25, N'+14.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (26, N'+13.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (27, N'+13.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (28, N'+13.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (29, N'+13.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (30, N'+12.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (31, N'+12.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (32, N'+12.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (33, N'+12.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (34, N'+11.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (35, N'+11.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (36, N'+11.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (37, N'+11.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (38, N'+10.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (39, N'+10.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (40, N'+10.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (41, N'+10.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (42, N'+9.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (43, N'+9.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (44, N'+9.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (45, N'+9.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (46, N'+8.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (47, N'+8.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (48, N'+8.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (49, N'+8.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (50, N'+7.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (51, N'+7.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (52, N'+7.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (53, N'+7.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (54, N'+6.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (55, N'+6.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (56, N'+6.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (57, N'+6.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (58, N'+5.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (59, N'+5.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (60, N'+5.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (61, N'+5.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (62, N'+4.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (63, N'+4.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (64, N'+4.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (65, N'+4.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (66, N'+3.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (67, N'+3.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (68, N'+3.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (69, N'+3.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (70, N'+2.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (71, N'+2.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (72, N'+2.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (73, N'+2.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (74, N'+1.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (75, N'+1.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (76, N'+1.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (77, N'+1.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (78, N'+0.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (79, N'+0.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (80, N'+0.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (81, N'0', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (82, N'-0.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (83, N'-0.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (84, N'-0.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (85, N'-1.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (86, N'-1.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (87, N'-1.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (88, N'-1.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (89, N'-2.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (90, N'-2.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (91, N'-2.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (92, N'-2.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (93, N'-3.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (94, N'-3.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (95, N'-3.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (96, N'-3.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (97, N'-4.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (98, N'-4.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (99, N'-4.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (100, N'-4.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (101, N'-5.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (102, N'-5.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (103, N'-5.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (104, N'-5.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (105, N'-6.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (106, N'-6.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (107, N'-6.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (108, N'-6.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (109, N'-7.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (110, N'-7.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (111, N'-7.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (112, N'-7.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (113, N'-8.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (114, N'-8.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (115, N'-8.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (116, N'-8.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (117, N'-9.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (118, N'-9.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (119, N'-9.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (120, N'-9.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (121, N'-10.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (122, N'-10.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (123, N'-10.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (124, N'-10.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (125, N'-11.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (126, N'-11.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (127, N'-11.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (128, N'-11.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (129, N'-12.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (130, N'-12.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (131, N'-12.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (132, N'-12.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (133, N'-13.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (134, N'-13.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (135, N'-13.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (136, N'-13.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (137, N'-14.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (138, N'-14.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (139, N'-14.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (140, N'-14.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (141, N'-15.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (142, N'-15.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (143, N'-15.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (144, N'-15.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (145, N'-16.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (146, N'-16.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (147, N'-16.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (148, N'-16.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (149, N'-17.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (150, N'-17.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (151, N'-17.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (152, N'-17.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (153, N'-18.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (154, N'-18.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (155, N'-18.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (156, N'-18.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (157, N'-19.00', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (158, N'-19.25', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (159, N'-19.50', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (160, N'-19.75', NULL, NULL, NULL, NULL)
GO
INSERT [lookup].[SPHs] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (161, N'-20.00', NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [lookup].[SPHs] OFF
GO
SET IDENTITY_INSERT [lookup].[VersionType] ON 
GO
INSERT [lookup].[VersionType] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (1, N'Test Version type', CAST(N'2021-03-15T00:00:00.0000000' AS DateTime2), N'Mohamed', NULL, NULL)
GO
INSERT [lookup].[VersionType] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (2, N'tested', CAST(N'2021-03-15T23:50:19.7200153' AS DateTime2), NULL, NULL, NULL)
GO
INSERT [lookup].[VersionType] ([Id], [Name], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (3, N'test advise', CAST(N'2021-03-15T23:54:39.3746658' AS DateTime2), NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [lookup].[VersionType] OFF
GO
ALTER TABLE [dbo].[Currency] ADD  CONSTRAINT [DF_Currency_IsDefault]  DEFAULT ((0)) FOR [IsDefault]
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Emails] ADD  CONSTRAINT [DF_Emails_IsEmailSent]  DEFAULT ((0)) FOR [IsEmailSent]
GO
ALTER TABLE [dbo].[Emails] ADD  CONSTRAINT [DF_Emails_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[MedicalLensPurchaseDetail] ADD  CONSTRAINT [DF_MedicalLensPurchaseDetail_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[PurchaseTransactionDetail] ADD  CONSTRAINT [DF_PurchaseTransactionDetail_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[SMSs] ADD  CONSTRAINT [DF_Table_1_IsEmailSent]  DEFAULT ((0)) FOR [IsMessageSent]
GO
ALTER TABLE [dbo].[SMSs] ADD  CONSTRAINT [DF_SMS_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Supplier] ADD  CONSTRAINT [DF_Supplier_IsExternal]  DEFAULT ((0)) FOR [IsExternal]
GO
ALTER TABLE [dbo].[WarehouseTransaction] ADD  CONSTRAINT [DF_WarehouseTransaction_Status]  DEFAULT ((2)) FOR [Status]
GO
ALTER TABLE [dbo].[WarehouseVerification] ADD  CONSTRAINT [DF_WarehouseVerification_IsSettled]  DEFAULT ((0)) FOR [IsSettled]
GO
ALTER TABLE [dbo].[Branch]  WITH CHECK ADD  CONSTRAINT [FK_Branch_Region] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Region] ([Id])
GO
ALTER TABLE [dbo].[Branch] CHECK CONSTRAINT [FK_Branch_Region]
GO
ALTER TABLE [dbo].[City]  WITH CHECK ADD  CONSTRAINT [FK_City_Country] FOREIGN KEY([CountyId])
REFERENCES [dbo].[Country] ([Id])
GO
ALTER TABLE [dbo].[City] CHECK CONSTRAINT [FK_City_Country]
GO
ALTER TABLE [dbo].[Color]  WITH CHECK ADD  CONSTRAINT [FK_Color_Model] FOREIGN KEY([ModelId])
REFERENCES [dbo].[Model] ([Id])
GO
ALTER TABLE [dbo].[Color] CHECK CONSTRAINT [FK_Color_Model]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_CutomerTitle] FOREIGN KEY([TitleId])
REFERENCES [dbo].[CustomerTitle] ([Id])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_CutomerTitle]
GO
ALTER TABLE [dbo].[CustomerDisease]  WITH CHECK ADD  CONSTRAINT [FK_CustomerDisease_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[CustomerDisease] CHECK CONSTRAINT [FK_CustomerDisease_Customer]
GO
ALTER TABLE [dbo].[CustomerDisease]  WITH CHECK ADD  CONSTRAINT [FK_CustomerDisease_Disease] FOREIGN KEY([DiseaseId])
REFERENCES [dbo].[Disease] ([Id])
GO
ALTER TABLE [dbo].[CustomerDisease] CHECK CONSTRAINT [FK_CustomerDisease_Disease]
GO
ALTER TABLE [dbo].[Doctor]  WITH CHECK ADD  CONSTRAINT [FK_Doctor_Region] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Region] ([Id])
GO
ALTER TABLE [dbo].[Doctor] CHECK CONSTRAINT [FK_Doctor_Region]
GO
ALTER TABLE [dbo].[Emails]  WITH CHECK ADD  CONSTRAINT [FK_Emails_Attachment] FOREIGN KEY([AttachmentId])
REFERENCES [dbo].[Attachment] ([Id])
GO
ALTER TABLE [dbo].[Emails] CHECK CONSTRAINT [FK_Emails_Attachment]
GO
ALTER TABLE [dbo].[Examination]  WITH CHECK ADD  CONSTRAINT [FK_Examination_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[Examination] CHECK CONSTRAINT [FK_Examination_Customer]
GO
ALTER TABLE [dbo].[Examination]  WITH CHECK ADD  CONSTRAINT [FK_Examination_Doctor] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Doctor] ([Id])
GO
ALTER TABLE [dbo].[Examination] CHECK CONSTRAINT [FK_Examination_Doctor]
GO
ALTER TABLE [dbo].[GroupPageAction]  WITH CHECK ADD  CONSTRAINT [FK_GroupPageAction_Group] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Group] ([Id])
GO
ALTER TABLE [dbo].[GroupPageAction] CHECK CONSTRAINT [FK_GroupPageAction_Group]
GO
ALTER TABLE [dbo].[GroupPageAction]  WITH CHECK ADD  CONSTRAINT [FK_GroupPageAction_PageAction] FOREIGN KEY([PageActionId])
REFERENCES [dbo].[PageAction] ([Id])
GO
ALTER TABLE [dbo].[GroupPageAction] CHECK CONSTRAINT [FK_GroupPageAction_PageAction]
GO
ALTER TABLE [dbo].[MedicalLensDetails]  WITH CHECK ADD  CONSTRAINT [FK_MedicalLensDetails_MedicalLensDtailsType] FOREIGN KEY([MedicalLensDetailsTypeId])
REFERENCES [dbo].[MedicalLensDtailsType] ([Id])
GO
ALTER TABLE [dbo].[MedicalLensDetails] CHECK CONSTRAINT [FK_MedicalLensDetails_MedicalLensDtailsType]
GO
ALTER TABLE [dbo].[MedicalLensDetails]  WITH CHECK ADD  CONSTRAINT [FK_MedicalLensDetails_SPHs] FOREIGN KEY([SphId])
REFERENCES [lookup].[SPHs] ([Id])
GO
ALTER TABLE [dbo].[MedicalLensDetails] CHECK CONSTRAINT [FK_MedicalLensDetails_SPHs]
GO
ALTER TABLE [dbo].[MedicalLensMaster]  WITH CHECK ADD  CONSTRAINT [FK_MedicalLensMaster_Brand] FOREIGN KEY([BrandId])
REFERENCES [lookup].[Brand] ([Id])
GO
ALTER TABLE [dbo].[MedicalLensMaster] CHECK CONSTRAINT [FK_MedicalLensMaster_Brand]
GO
ALTER TABLE [dbo].[MedicalLensMaster]  WITH CHECK ADD  CONSTRAINT [FK_MedicalLensMaster_CoatingDiagram] FOREIGN KEY([CoatingDiagramId])
REFERENCES [lookup].[CoatingDiagram] ([Id])
GO
ALTER TABLE [dbo].[MedicalLensMaster] CHECK CONSTRAINT [FK_MedicalLensMaster_CoatingDiagram]
GO
ALTER TABLE [dbo].[MedicalLensMaster]  WITH CHECK ADD  CONSTRAINT [FK_MedicalLensMaster_Color] FOREIGN KEY([ColorId])
REFERENCES [dbo].[Color] ([Id])
GO
ALTER TABLE [dbo].[MedicalLensMaster] CHECK CONSTRAINT [FK_MedicalLensMaster_Color]
GO
ALTER TABLE [dbo].[MedicalLensMaster]  WITH CHECK ADD  CONSTRAINT [FK_MedicalLensMaster_LenseIndex] FOREIGN KEY([LenseIndexId])
REFERENCES [lookup].[LenseIndex] ([Id])
GO
ALTER TABLE [dbo].[MedicalLensMaster] CHECK CONSTRAINT [FK_MedicalLensMaster_LenseIndex]
GO
ALTER TABLE [dbo].[MedicalLensMaster]  WITH CHECK ADD  CONSTRAINT [FK_MedicalLensMaster_Material] FOREIGN KEY([MaterialId])
REFERENCES [dbo].[Material] ([Id])
GO
ALTER TABLE [dbo].[MedicalLensMaster] CHECK CONSTRAINT [FK_MedicalLensMaster_Material]
GO
ALTER TABLE [dbo].[MedicalLensMaster]  WITH CHECK ADD  CONSTRAINT [FK_MedicalLensMaster_MedicalLensType] FOREIGN KEY([MedicalLensTypeId])
REFERENCES [lookup].[MedicalLensType] ([Id])
GO
ALTER TABLE [dbo].[MedicalLensMaster] CHECK CONSTRAINT [FK_MedicalLensMaster_MedicalLensType]
GO
ALTER TABLE [dbo].[MedicalLensMaster]  WITH CHECK ADD  CONSTRAINT [FK_MedicalLensMaster_MultifocalDesign] FOREIGN KEY([MultifocalDesignId])
REFERENCES [lookup].[MultifocalDesign] ([Id])
GO
ALTER TABLE [dbo].[MedicalLensMaster] CHECK CONSTRAINT [FK_MedicalLensMaster_MultifocalDesign]
GO
ALTER TABLE [dbo].[MedicalLensMaster]  WITH CHECK ADD  CONSTRAINT [FK_MedicalLensMaster_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([Id])
GO
ALTER TABLE [dbo].[MedicalLensMaster] CHECK CONSTRAINT [FK_MedicalLensMaster_Supplier]
GO
ALTER TABLE [dbo].[MedicalLensMaster]  WITH CHECK ADD  CONSTRAINT [FK_MedicalLensMaster_VersionType] FOREIGN KEY([VersionTypeId])
REFERENCES [lookup].[VersionType] ([Id])
GO
ALTER TABLE [dbo].[MedicalLensMaster] CHECK CONSTRAINT [FK_MedicalLensMaster_VersionType]
GO
ALTER TABLE [dbo].[MedicalLensPurchaseDetail]  WITH CHECK ADD  CONSTRAINT [FK_MedicalLensPurchaseDetail_MedicalLensMaster] FOREIGN KEY([MedicalLensMasterId])
REFERENCES [dbo].[MedicalLensMaster] ([Id])
GO
ALTER TABLE [dbo].[MedicalLensPurchaseDetail] CHECK CONSTRAINT [FK_MedicalLensPurchaseDetail_MedicalLensMaster]
GO
ALTER TABLE [dbo].[MedicalLensPurchaseDetail]  WITH CHECK ADD  CONSTRAINT [FK_MedicalLensPurchaseDetail_PurchaseTransactionMaster] FOREIGN KEY([PurchaseTransactionMasterID])
REFERENCES [dbo].[PurchaseTransactionMaster] ([Id])
GO
ALTER TABLE [dbo].[MedicalLensPurchaseDetail] CHECK CONSTRAINT [FK_MedicalLensPurchaseDetail_PurchaseTransactionMaster]
GO
ALTER TABLE [dbo].[Model]  WITH CHECK ADD  CONSTRAINT [FK_Model_Brand] FOREIGN KEY([BrandId])
REFERENCES [lookup].[Brand] ([Id])
GO
ALTER TABLE [dbo].[Model] CHECK CONSTRAINT [FK_Model_Brand]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_Group] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Group] ([Id])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_Group]
GO
ALTER TABLE [dbo].[NotificationResponse]  WITH CHECK ADD  CONSTRAINT [FK_NotificationResponse_Notification] FOREIGN KEY([NotificationId])
REFERENCES [dbo].[Notification] ([Id])
GO
ALTER TABLE [dbo].[NotificationResponse] CHECK CONSTRAINT [FK_NotificationResponse_Notification]
GO
ALTER TABLE [dbo].[NotificationResponse]  WITH CHECK ADD  CONSTRAINT [FK_NotificationResponse_ResponseUser] FOREIGN KEY([ResponseById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[NotificationResponse] CHECK CONSTRAINT [FK_NotificationResponse_ResponseUser]
GO
ALTER TABLE [dbo].[NotificationSeen]  WITH CHECK ADD  CONSTRAINT [FK_NotificationSeen_Notification] FOREIGN KEY([NotificationId])
REFERENCES [dbo].[Notification] ([Id])
GO
ALTER TABLE [dbo].[NotificationSeen] CHECK CONSTRAINT [FK_NotificationSeen_Notification]
GO
ALTER TABLE [dbo].[NotificationSeen]  WITH CHECK ADD  CONSTRAINT [FK_NotificationSeen_SeenUser] FOREIGN KEY([SeenById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[NotificationSeen] CHECK CONSTRAINT [FK_NotificationSeen_SeenUser]
GO
ALTER TABLE [dbo].[PageAction]  WITH CHECK ADD  CONSTRAINT [FK_PageAction_SystemPage] FOREIGN KEY([PageId])
REFERENCES [dbo].[SystemPage] ([Id])
GO
ALTER TABLE [dbo].[PageAction] CHECK CONSTRAINT [FK_PageAction_SystemPage]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Brand] FOREIGN KEY([BrandId])
REFERENCES [lookup].[Brand] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Brand]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Color] FOREIGN KEY([ColorId])
REFERENCES [dbo].[Color] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Color]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Material] FOREIGN KEY([MaterialId])
REFERENCES [dbo].[Material] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Material]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Model] FOREIGN KEY([ModelId])
REFERENCES [dbo].[Model] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Model]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductCategory] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[ProductCategory] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_ProductCategory]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Supplier]
GO
ALTER TABLE [dbo].[ProductCategory]  WITH CHECK ADD  CONSTRAINT [FK_ProductCategory_ProductCategory1] FOREIGN KEY([ParentId])
REFERENCES [dbo].[ProductCategory] ([Id])
GO
ALTER TABLE [dbo].[ProductCategory] CHECK CONSTRAINT [FK_ProductCategory_ProductCategory1]
GO
ALTER TABLE [dbo].[PurchaseTransactionDetail]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseTransactionDetail_Currency] FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[Currency] ([Id])
GO
ALTER TABLE [dbo].[PurchaseTransactionDetail] CHECK CONSTRAINT [FK_PurchaseTransactionDetail_Currency]
GO
ALTER TABLE [dbo].[PurchaseTransactionDetail]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseTransactionDetail_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[PurchaseTransactionDetail] CHECK CONSTRAINT [FK_PurchaseTransactionDetail_Product]
GO
ALTER TABLE [dbo].[PurchaseTransactionDetail]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseTransactionDetail_PurchaseTransactionMaster] FOREIGN KEY([PurchaseTransactionMasterID])
REFERENCES [dbo].[PurchaseTransactionMaster] ([Id])
GO
ALTER TABLE [dbo].[PurchaseTransactionDetail] CHECK CONSTRAINT [FK_PurchaseTransactionDetail_PurchaseTransactionMaster]
GO
ALTER TABLE [dbo].[PurchaseTransactionMaster]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseTransactionMaster_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([Id])
GO
ALTER TABLE [dbo].[PurchaseTransactionMaster] CHECK CONSTRAINT [FK_PurchaseTransactionMaster_Supplier]
GO
ALTER TABLE [dbo].[PurchaseTransactionMaster]  WITH NOCHECK ADD  CONSTRAINT [FK_PurchaseTransactionMaster_Warehouse] FOREIGN KEY([WarehouseId])
REFERENCES [dbo].[Warehouse] ([Id])
GO
ALTER TABLE [dbo].[PurchaseTransactionMaster] CHECK CONSTRAINT [FK_PurchaseTransactionMaster_Warehouse]
GO
ALTER TABLE [dbo].[PurchaseTransactionPayment]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseTransactionPayment_PurchaseTransactionMaster] FOREIGN KEY([TransactionId])
REFERENCES [dbo].[SalesTransaction] ([Id])
GO
ALTER TABLE [dbo].[PurchaseTransactionPayment] CHECK CONSTRAINT [FK_PurchaseTransactionPayment_PurchaseTransactionMaster]
GO
ALTER TABLE [dbo].[Region]  WITH CHECK ADD  CONSTRAINT [FK_Region_City] FOREIGN KEY([CityId])
REFERENCES [dbo].[City] ([Id])
GO
ALTER TABLE [dbo].[Region] CHECK CONSTRAINT [FK_Region_City]
GO
ALTER TABLE [dbo].[SalesTransaction]  WITH CHECK ADD  CONSTRAINT [FK_SalesTransaction_Branch] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[SalesTransaction] CHECK CONSTRAINT [FK_SalesTransaction_Branch]
GO
ALTER TABLE [dbo].[SalesTransaction]  WITH CHECK ADD  CONSTRAINT [FK_SalesTransaction_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[SalesTransaction] CHECK CONSTRAINT [FK_SalesTransaction_Customer]
GO
ALTER TABLE [dbo].[SalesTransaction]  WITH NOCHECK ADD  CONSTRAINT [FK_SalesTransaction_Warehouse] FOREIGN KEY([WarehouseId])
REFERENCES [dbo].[Warehouse] ([Id])
GO
ALTER TABLE [dbo].[SalesTransaction] CHECK CONSTRAINT [FK_SalesTransaction_Warehouse]
GO
ALTER TABLE [dbo].[SalesTransactionDetail]  WITH CHECK ADD  CONSTRAINT [FK_SalesTransactionDetail_MedicalLensMaster] FOREIGN KEY([MedicalLensIMasterd])
REFERENCES [dbo].[MedicalLensMaster] ([Id])
GO
ALTER TABLE [dbo].[SalesTransactionDetail] CHECK CONSTRAINT [FK_SalesTransactionDetail_MedicalLensMaster]
GO
ALTER TABLE [dbo].[SalesTransactionDetail]  WITH CHECK ADD  CONSTRAINT [FK_SalesTransactionDetail_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[SalesTransactionDetail] CHECK CONSTRAINT [FK_SalesTransactionDetail_Product]
GO
ALTER TABLE [dbo].[SalesTransactionDetail]  WITH CHECK ADD  CONSTRAINT [FK_SalesTransactionDetail_SalesTransaction] FOREIGN KEY([TransactionId])
REFERENCES [dbo].[SalesTransaction] ([Id])
GO
ALTER TABLE [dbo].[SalesTransactionDetail] CHECK CONSTRAINT [FK_SalesTransactionDetail_SalesTransaction]
GO
ALTER TABLE [dbo].[SalesTransactionPayment]  WITH CHECK ADD  CONSTRAINT [FK_SalesTransactionPayment_SalesTransaction] FOREIGN KEY([TransactionId])
REFERENCES [dbo].[SalesTransaction] ([Id])
GO
ALTER TABLE [dbo].[SalesTransactionPayment] CHECK CONSTRAINT [FK_SalesTransactionPayment_SalesTransaction]
GO
ALTER TABLE [dbo].[Supplier]  WITH CHECK ADD  CONSTRAINT [FK_Supplier_Region] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Region] ([Id])
GO
ALTER TABLE [dbo].[Supplier] CHECK CONSTRAINT [FK_Supplier_Region]
GO
ALTER TABLE [dbo].[SupplierContactPerson]  WITH CHECK ADD  CONSTRAINT [FK_SupplierContactPerson_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([Id])
GO
ALTER TABLE [dbo].[SupplierContactPerson] CHECK CONSTRAINT [FK_SupplierContactPerson_Supplier]
GO
ALTER TABLE [dbo].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_Upload] FOREIGN KEY([LogoId])
REFERENCES [dbo].[Upload] ([Id])
GO
ALTER TABLE [dbo].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_Upload]
GO
ALTER TABLE [dbo].[SystemPage]  WITH CHECK ADD  CONSTRAINT [FK_Page_Page] FOREIGN KEY([ParentId])
REFERENCES [dbo].[SystemPage] ([Id])
GO
ALTER TABLE [dbo].[SystemPage] CHECK CONSTRAINT [FK_Page_Page]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Job] FOREIGN KEY([JobId])
REFERENCES [dbo].[Job] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Job]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Upload] FOREIGN KEY([ImageId])
REFERENCES [dbo].[Upload] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Upload]
GO
ALTER TABLE [dbo].[UserGroup]  WITH CHECK ADD  CONSTRAINT [FK_UserGroup_Group] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Group] ([Id])
GO
ALTER TABLE [dbo].[UserGroup] CHECK CONSTRAINT [FK_UserGroup_Group]
GO
ALTER TABLE [dbo].[UserGroup]  WITH CHECK ADD  CONSTRAINT [FK_UserGroup_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UserGroup] CHECK CONSTRAINT [FK_UserGroup_User]
GO
ALTER TABLE [dbo].[UserPageAction]  WITH CHECK ADD  CONSTRAINT [FK_UserPageAction_PageAction] FOREIGN KEY([PageActionId])
REFERENCES [dbo].[PageAction] ([Id])
GO
ALTER TABLE [dbo].[UserPageAction] CHECK CONSTRAINT [FK_UserPageAction_PageAction]
GO
ALTER TABLE [dbo].[UserPageAction]  WITH CHECK ADD  CONSTRAINT [FK_UserPageAction_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UserPageAction] CHECK CONSTRAINT [FK_UserPageAction_User]
GO
ALTER TABLE [dbo].[UserWarehouse]  WITH CHECK ADD  CONSTRAINT [FK_UserWarehouse_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UserWarehouse] CHECK CONSTRAINT [FK_UserWarehouse_User]
GO
ALTER TABLE [dbo].[UserWarehouse]  WITH CHECK ADD  CONSTRAINT [FK_UserWarehouse_Warehouse] FOREIGN KEY([WarehouseId])
REFERENCES [dbo].[Warehouse] ([Id])
GO
ALTER TABLE [dbo].[UserWarehouse] CHECK CONSTRAINT [FK_UserWarehouse_Warehouse]
GO
ALTER TABLE [dbo].[Warehouse]  WITH NOCHECK ADD  CONSTRAINT [FK_Warehouse_Region] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Region] ([Id])
GO
ALTER TABLE [dbo].[Warehouse] CHECK CONSTRAINT [FK_Warehouse_Region]
GO
ALTER TABLE [dbo].[Warehouse]  WITH CHECK ADD  CONSTRAINT [FK_Warehouse_User] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Warehouse] CHECK CONSTRAINT [FK_Warehouse_User]
GO
ALTER TABLE [dbo].[WarehouseTransaction]  WITH CHECK ADD  CONSTRAINT [FK_WarehouseTransaction_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[WarehouseTransaction] CHECK CONSTRAINT [FK_WarehouseTransaction_Customer]
GO
ALTER TABLE [dbo].[WarehouseTransaction]  WITH CHECK ADD  CONSTRAINT [FK_WarehouseTransaction_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([Id])
GO
ALTER TABLE [dbo].[WarehouseTransaction] CHECK CONSTRAINT [FK_WarehouseTransaction_Supplier]
GO
ALTER TABLE [dbo].[WarehouseTransaction]  WITH NOCHECK ADD  CONSTRAINT [FK_WarehouseTransaction_Warehouse] FOREIGN KEY([WarehouseId])
REFERENCES [dbo].[Warehouse] ([Id])
GO
ALTER TABLE [dbo].[WarehouseTransaction] CHECK CONSTRAINT [FK_WarehouseTransaction_Warehouse]
GO
ALTER TABLE [dbo].[WarehouseTransactionDetail]  WITH CHECK ADD  CONSTRAINT [FK_WarehouseTransactionDetail_MedicalLensMaster] FOREIGN KEY([MedicalLensIMasterd])
REFERENCES [dbo].[MedicalLensMaster] ([Id])
GO
ALTER TABLE [dbo].[WarehouseTransactionDetail] CHECK CONSTRAINT [FK_WarehouseTransactionDetail_MedicalLensMaster]
GO
ALTER TABLE [dbo].[WarehouseTransactionDetail]  WITH CHECK ADD  CONSTRAINT [FK_WarehouseTransactionDetail_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[WarehouseTransactionDetail] CHECK CONSTRAINT [FK_WarehouseTransactionDetail_Product]
GO
ALTER TABLE [dbo].[WarehouseTransactionDetail]  WITH CHECK ADD  CONSTRAINT [FK_WarehouseTransactionDetail_WarehouseTransaction] FOREIGN KEY([TransactionId])
REFERENCES [dbo].[WarehouseTransaction] ([Id])
GO
ALTER TABLE [dbo].[WarehouseTransactionDetail] CHECK CONSTRAINT [FK_WarehouseTransactionDetail_WarehouseTransaction]
GO
ALTER TABLE [dbo].[WarehouseVerification]  WITH CHECK ADD  CONSTRAINT [FK_InventoryVerification_Warehouse] FOREIGN KEY([WarehouseId])
REFERENCES [dbo].[Warehouse] ([Id])
GO
ALTER TABLE [dbo].[WarehouseVerification] CHECK CONSTRAINT [FK_InventoryVerification_Warehouse]
GO
ALTER TABLE [dbo].[WarehouseVerificationDetail]  WITH CHECK ADD  CONSTRAINT [FK_WarehouseVerificationDetail_MedicalLensMaster] FOREIGN KEY([MedicalLensIMasterd])
REFERENCES [dbo].[MedicalLensMaster] ([Id])
GO
ALTER TABLE [dbo].[WarehouseVerificationDetail] CHECK CONSTRAINT [FK_WarehouseVerificationDetail_MedicalLensMaster]
GO
ALTER TABLE [dbo].[WarehouseVerificationDetail]  WITH CHECK ADD  CONSTRAINT [FK_WarehouseVerificationDetail_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[WarehouseVerificationDetail] CHECK CONSTRAINT [FK_WarehouseVerificationDetail_Product]
GO
ALTER TABLE [dbo].[WarehouseVerificationDetail]  WITH CHECK ADD  CONSTRAINT [FK_WarehouseVerificationDetails_WarehouseVerification] FOREIGN KEY([WarehouseVerificationId])
REFERENCES [dbo].[WarehouseVerification] ([Id])
GO
ALTER TABLE [dbo].[WarehouseVerificationDetail] CHECK CONSTRAINT [FK_WarehouseVerificationDetails_WarehouseVerification]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'دمغة مهن طبية' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PurchaseTransactionMaster', @level2type=N'COLUMN',@level2name=N'SupplierInvoiceDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'رسوم جمركية' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PurchaseTransactionMaster', @level2type=N'COLUMN',@level2name=N'SupplierInvoiceNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1=Pending
2=Transfered to inventory add' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PurchaseTransactionMaster', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1=Sales Request,
2=Sales Invoice,3=Canceled Request,4=Sales Return' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SalesTransaction', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'امين المخزن' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Warehouse', @level2type=N'COLUMN',@level2name=N'EmployeeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'اسم امين المخزن' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WarehouseTransaction', @level2type=N'COLUMN',@level2name=N'EmployeeName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1=Pending/Hold,
2=Approved,
3=Canceled' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WarehouseTransaction', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "pd"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 285
               Right = 337
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 7
               Left = 385
               Bottom = 310
               Right = 579
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "s"
            Begin Extent = 
               Top = 7
               Left = 627
               Bottom = 170
               Right = 825
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Warehouse"
            Begin Extent = 
               Top = 7
               Left = 873
               Bottom = 310
               Right = 1074
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PurchaseReportView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PurchaseReportView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "sd"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 7
               Left = 290
               Bottom = 170
               Right = 484
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "s"
            Begin Extent = 
               Top = 7
               Left = 532
               Bottom = 170
               Right = 730
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'SalesReportView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'SalesReportView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "wo"
            Begin Extent = 
               Top = 7
               Left = 313
               Bottom = 170
               Right = 532
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "wt"
            Begin Extent = 
               Top = 7
               Left = 580
               Bottom = 170
               Right = 790
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 7
               Left = 838
               Bottom = 170
               Right = 1048
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "w"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 265
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'WarehouseProductsView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'WarehouseProductsView'
GO
USE [master]
GO
ALTER DATABASE [OMS-Dev] SET  READ_WRITE 
GO
