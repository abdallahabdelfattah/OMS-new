using OMS.BLL.Data;
using OMS.BLL.Repositories;
using OMS.DAL.DataAccess;

namespace OMS.BLL
{
    public sealed partial class OMSUoW : UnitOfWorkBase, IUnitOfWork
    {
        public OMSUoW()
        {
            this.context = new Entities();
            this.Cities = new CityRepository(this.context);
            this.Categories = new CategoryRepository(this.context);
            this.Countries = new CountryRepository(this.context);
            this.Currencies = new CurrencyRepository(this.context);
            this.Customers = new CustomerRepository(this.context);
            this.Groups = new GroupRepository(this.context);
            this.Notifications = new NotificationRepository(this.context);
            this.Products = new ProductRepository(this.context);
            this.Regions = new RegionRepository(this.context);
            this.Suppliers = new SupplierRepository(this.context);
            this.SystemPages = new SystemPageRepository(this.context);
            this.UserGroups = new UserGroupRepository(this.context);
            this.UserPageActions = new UserPageActionRepository(this.context);
            this.GroupPageActions = new GroupPageActionRepository(this.context);
            this.Users = new UserRepository(this.context);
            this.Warehouses = new WarehouseRepository(this.context);
            this.Branches = new BranchRepository(this.context);
            this.Doctors = new DoctorRepository(this.context);
            this.Diseases = new DiseaseRepository(this.context);
            this.Examinations = new ExaminationRepository(this.context);
            this.SystemConfigurations = new SystemConfigurationRepository(this.context);
            this.Uploads = new UploadRepository(this.context);
            this.CustomerTitles = new CustomerTitleRepository(this.context);
            this.Nationalities = new NationalityRepository(this.context);
            this.SalesTransactions = new SalesTransactionRepository(this.context);
            this.WarehouseTransactions = new WarehouseTransactionRepository(this.context);
            this.WarehouseProductsViews = new WarehouseProductsViewRepository(this.context);
            this.PurchaseTransactionMaster = new PurchaseTransactionMasterRepository(this.context);
            this.PurchaseTransactionDetails = new PurchaseTransactionDetailsRepository(this.context);
            this.Jobs = new JobRepository(this.context);
            this.WarehouseVerifications = new WarehouseVerificationRepository(this.context);
            this.Email = new EmailRepository(this.context);
            this.Sms = new SmsRepository(this.context);
            this.Brand = new BrandRepository(this.context);
            this.BrandModel = new ModelRepository(this.context);
            this.Color = new ColorRepository(this.context);
            this.ColorCategory = new ColorCategoryRepository(this.context);
            this.PurchaseReportView = new PurchaseReportViewRepository(this.context);
            this.Material = new MaterialRepository(this.context);
            this.CoatingDiagram = new CoatingDiagramRepository(this.context);
            this.LenseIndex = new LenseIndexRepository(this.context);
            this.MultifocalDesign = new MultifocalDesignRepository(this.context);
            this.VersionType = new VersionTypeRepository(this.context);
            this.SPH = new SPHRepository(this.context);
            this.CYL = new CYLRepository(this.context);
            this.MedicalLensMaster = new MedicalLensMasterRepository(this.context);
            this.MedicalLensDetails = new MedicalLensDetailsRepository(this.context);
            this.MedicalLensSalesDetails = new MedicalLensSalesDetailsRepository(this.context);
            this.SalesTrackingReportView = new SalesTrackingReportViewRepository(this.context);
            this.CLSPAndSolutionType = new CLSPAndSolutionTypeRepository(this.context);
            this.Power = new PowerRepository(this.context);
            this.UsePeriod = new UsePeriodRepository(this.context);
            this.Grade = new GradeRepository(this.context);
        }
        public CityRepository Cities { get; set; }
        public CategoryRepository Categories { get; set; }
        public CountryRepository Countries { get; set; }
        public CurrencyRepository Currencies { get; set; }
        public CustomerRepository Customers { get; set; }
        public GroupRepository Groups { get; set; }
        public NotificationRepository Notifications { get; set; }
        public MedicalLensMasterRepository MedicalLensMaster { get; set; }
        public MedicalLensDetailsRepository MedicalLensDetails { get; set; }
        public ProductRepository Products { get; set; }
        public RegionRepository Regions { get; set; }
        public SupplierRepository Suppliers { get; set; }
        public SystemPageRepository SystemPages { get; set; }
        public UserGroupRepository UserGroups { get; set; }
        public UserPageActionRepository UserPageActions { get; set; }
        public GroupPageActionRepository GroupPageActions { get; set; }
        public UserRepository Users { get; set; }
        public WarehouseRepository Warehouses { get; set; }
        public DoctorRepository Doctors { get; set; }
        public BranchRepository Branches { get; set; }
        public DiseaseRepository Diseases { get; set; }
        public ExaminationRepository Examinations { get; set; }
        public SystemConfigurationRepository SystemConfigurations { get; set; }
        public UploadRepository Uploads { get; set; }
        public CustomerTitleRepository CustomerTitles { get; set; }
        public NationalityRepository Nationalities { get; set; }
        public SalesTransactionRepository SalesTransactions { get; set; }
        public WarehouseTransactionRepository WarehouseTransactions { get; set; }
        public WarehouseProductsViewRepository WarehouseProductsViews { get; set; }
        public PurchaseTransactionMasterRepository PurchaseTransactionMaster { get; set; }
        public PurchaseTransactionDetailsRepository PurchaseTransactionDetails { get; set; }
        public PurchaseReportViewRepository PurchaseReportView { get; set; }
        public JobRepository Jobs { get; set; }
        public WarehouseVerificationRepository WarehouseVerifications { get; set; }
        public ColorRepository Color { get; set; }
        public ColorCategoryRepository ColorCategory { get; set; }
        public ModelRepository BrandModel { get; set; }
        public BrandRepository Brand { get; set; }
        public SmsRepository Sms { get; set; }
        public EmailRepository Email { get; set; }
        public MaterialRepository Material { get; set; }
        public CoatingDiagramRepository CoatingDiagram { get; set; }
        public LenseIndexRepository LenseIndex { get; set; }
        public MultifocalDesignRepository MultifocalDesign { get; set; }
        public VersionTypeRepository VersionType { get; set; }
        public SPHRepository SPH { get; set; }
        public CYLRepository CYL { get; set; }
        public MedicalLensDtailsTypeRepository MedicalLensDtailsType { get; set; }
        public SalesTrackingReportViewRepository SalesTrackingReportView { get; set; }
        public CLSPAndSolutionTypeRepository CLSPAndSolutionType { get; set; }
        public PowerRepository Power { get; set; }
        public UsePeriodRepository UsePeriod { get; set; }
        public GradeRepository Grade { get; set; }
        public MedicalLensSalesDetailsRepository MedicalLensSalesDetails { get; set; }
    }
}
