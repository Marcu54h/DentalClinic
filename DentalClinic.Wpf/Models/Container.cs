namespace DentalClinic.Wpf
{

    using DentalClinic.Data;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity.Core.EntityClient;
    using System.Data.SqlClient;
    using System.IO;

    /// <summary>
    /// 
    /// </summary>
    public class Container : ISmsServiceStatus
    {
        private readonly string configFile = "config.xml";

        private readonly string guiSettingFile = "guiset.xml";

        private readonly string smsNotifyAgreementFile = @"xml\smsagree.xml";

        public AppSettings AppSettings { get; set; } = new AppSettings();

        public GuiSettings GuiSettings { get; set; } = new GuiSettings();

        public string ConfigFile
        {
            get
            {
                if (new FileInfo(configFile).Exists)
                {
                    return new FileInfo(configFile).FullName;
                }
                return configFile;
            }
        }

        public string GuiSettingFile
        {
            get
            {
                if (new FileInfo(guiSettingFile).Exists)
                {
                    return new FileInfo(guiSettingFile).FullName;
                }
                return guiSettingFile;
            }
        }

        public string SmsNotifyAgreementFile
        {
            get
            {
                if (new FileInfo(smsNotifyAgreementFile).Exists)
                {
                    return new FileInfo(smsNotifyAgreementFile).FullName;
                }
                return smsNotifyAgreementFile;
            }
        }

        public readonly string XmlFileFolder = @"xml";

        public bool CommentPatient { get; set; }

        public bool DBConnectionStatus { get; set; }

        public bool SmsServiceStatus { get; set; }

        public Employee SelectedEmployee { get; set; }

        public EmployeeWithSelection SelectedEmployeeWithSelection { get; set; }

        public IScheduleDay SelectedScheduleDay { get; set; }

        public Office SelectedOffice { get; set; }

        public Patient SelectedPatient { get; set; }

        public Treatment SelectedTreatment { get; set; }
        
        public SubTreatment SelectedSubTreatment { get; set; }

        public Sub2Treatment SelectedSub2Treatment { get; set; }

        public WorkInterval SelectedWorkInterval { get; set; }

        public OfficeWithSelection SelectedOfficeWithSelection { get; set; }

        public Visit SelectedVisit { get; set; } = Visit.Empty;

        public ICollection<Employee> Employees { get; set; } = default;

        public ICollection<Office> Offices { get; set; } = default;

        public ICollection<Tooth> SelectedTeeth { get; set; } = new Collection<Tooth>();

        public ICollection<Treatment> Treatments { get; set; }

        public string ConnectionString
        {
            get
            {
                SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder()
                {
                    ApplicationName = "EntityFramework",
                    DataSource = "10.6.0.5",
                    MultipleActiveResultSets = true,
                    PersistSecurityInfo = true,
                    InitialCatalog = "DentalClinic",
                    UserID = "dental",
                    //IntegratedSecurity = true,
                    Password = "Sakerhet",
                };


                EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder()
                {
                    Provider = "System.Data.SqlClient",
                    ProviderConnectionString = sqlBuilder.ToString(),
                    Metadata = @"res://*/PersonalData.csdl|
                               res://*/PersonalData.ssdl|
                               res://*/PersonalData.msl",
                };
                return entityBuilder.ToString();
                
            }
        }

        public string DbTestConnectionString
        {
            get
            {
                SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder()
                {
                    DataSource = AppSettings.DataBaseIP + @"\SQLEXPRESS",
                    MultipleActiveResultSets = true,
                    PersistSecurityInfo = true,
                    InitialCatalog = AppSettings.DataBaseName,
                    UserID = AppSettings.DataBaseUser,
                    Password = "Sakerhet",
                };
                return sqlBuilder.ConnectionString;
            }
        }

        

        
    }

    

    

}
